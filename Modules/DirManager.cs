using System;
using System.Collections.Generic;
using System.IO;
using RipeLib.Modules.Other;
using RipeLib.Serializables.ArgumentsInfo.FileManager;
using RipeLib.Serializables.ArgumentsInfo.FileManager.Archive;
using RipeLib.Serializables.ArgumentsInfo.FileManager.Directory;

namespace RipeLib.Modules
{
/// <summary> Initializes Several Functions for Directories (such as Copying, Moving, Deleting and Others). </summary>

public static class DirManager
{
/** <summary> Changes a Path to a new one. </summary>

<param name = "oldPath"> The Path to be Changed. </param>
<param name = "newPath"> The New Path of the Folder. </param>

<returns> The Path Changed. </returns> */

public static string ChangePath(string oldPath, string newPath)
{
string absolutePath;

if(newPath == oldPath)
{
string rootPath = Path.GetDirectoryName(newPath);
char namePrefix = InputHelper.GenerateStringComplement();

string folderName = GetFolderName(newPath);
absolutePath = Path.Combine(rootPath, namePrefix + folderName);
}

else
absolutePath = newPath;

return absolutePath;
}

/** <summary> Checks if a Directory is Missing on User's device or not. </summary>
<remarks> In case the Folder does not Exists, it will be Created in order to avoid Issues. </remarks>

<param name = "targetPath"> The Path where the Directory to be Analized is Located. </param>
<returns> Info related to the Directory that was Analized. </returns> */

public static DirectoryInfo CheckMissingFolder(string targetPath)
{
DirectoryInfo folderInfo;

if(!Directory.Exists(targetPath) )
folderInfo = Directory.CreateDirectory(targetPath);

else
folderInfo = new(targetPath);

return folderInfo;
}

/** <summary> Copies a Directory to a New Location. </summary>

<param name = "sourcePath"> The Path where the Directory to be Copied is Located. </param>
<param name = "destPath"> The Location where the Folder will be Copied (this must be a Root Directory). </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CopyFolder(string sourcePath, string destPath,
ArchiveFilterCriteria filesFilter = null, DirectoryFilterCriteria dirsFilter = null)
{
filesFilter ??= new();
dirsFilter ??= new();

CheckMissingFolder(destPath);

string targetPath = Path.Combine(destPath, GetFolderName(sourcePath) );
string[] filesList = Directory.GetFiles(sourcePath);

PathHelper.FilterFiles(ref filesList, filesFilter.FileNamesToSearch, filesFilter.FileExtensionsToSearch);

foreach(string file in filesList)
FileManager.CopyFile(file, targetPath, false);

string[] dirsList = Directory.GetFiles(sourcePath);
PathHelper.FilterDirs(ref dirsList, dirsFilter.DirNamesToSearch, dirsFilter.MatchingContentLength);

foreach(string dir in dirsList)
{
string targetSubDir = Path.Combine(destPath, GetFolderName(dir) );

CopyFolder(dir, targetSubDir, filesFilter, dirsFilter);
}

}

/** <summary> Creates a Link that Serves as a Direct Access to a Specific Directory. </summary>

<param name = "sourcePath"> The Path of the Folder where the Direct Access will be Created from. </param>
<param name = "targetPath"> The Path where the Direct Access will be Created. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CreateDirectAccess(string sourcePath, string targetPath)
{
PathHelper.AddExtension(ref targetPath, ".lnk");

PathHelper.CheckDuplicatedPath(ref targetPath);

Directory.CreateSymbolicLink(targetPath, sourcePath);
}

/** <summary> Creates a New Directory on the specific Location. </summary>

<param name = "targetPath"> The Path where the Folder will be Created. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CreateFolder(string targetPath) => Directory.CreateDirectory(targetPath);

/** <summary> Deletes the Directory specific. </summary>

<param name = "targetPath"> The Path where the Folder to be Deleted is Located. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void DeleteFolder(string targetPath, bool? isRecursiveAction = null)
{
bool deleteAll = (isRecursiveAction == null) ? !FolderIsEmpty(targetPath) : (bool)isRecursiveAction;

Directory.Delete(targetPath, deleteAll);
}

/** <summary> Gets the Path of a Container. </summary>

<param name = "targetFilePath"> The Location of the File to add in the Container. </param>
<param name = "namePrefix"> The Prefix to Add to the Beginning of the Name (Optional). </param> */

public static string GetContainerPath(string targetPath, string namePrefix = "FilesContainer")
{
string rootPath = Path.GetDirectoryName(targetPath);
string fileName = Path.GetFileNameWithoutExtension(targetPath);

string containerPath = Path.Combine(rootPath, namePrefix, fileName);
CheckMissingFolder(containerPath);

return containerPath;
}

/** <summary> Gets a List of FileSystems. </summary> */

public static string[] GetFileSystems(string targetPath, bool isForFiles, FileSystemSearchParams searchParams = default)
{

if(!Directory.Exists(targetPath) )
return Array.Empty<string>();

searchParams ??= new(isForFiles);

if(isForFiles)
return Directory.GetFiles(targetPath, searchParams.SearchPattern, searchParams.EnumOptions);

return Directory.GetDirectories(targetPath, searchParams.SearchPattern, searchParams.EnumOptions);
}

/** <summary> Gets a List of File Entries. </summary>

<param name = "targetFilePath"> The Location of the File to add in the Container. </param>
<param name = "namePrefix"> The Prefix to Add to the Beginning of the Name (Optional). </param> */

public static string[] GetEntryNames(string targetPath, FileSystemSearchParams searchParams = default)
{
string[] filesList;

if(PathHelper.CheckPathType(targetPath) == FileAttributes.Directory)
filesList = GetFileSystems(targetPath, true, searchParams);

else
{
filesList = new string[1];
filesList[0] = targetPath;
}

return filesList;
}

/** <summary> Gets the Name of a Directory.  </summary>

<param name = "targetPath"> The Path where the Folder to be Analized is Located. </param>

<returns> The Name of the Folder. </returns> */

public static string GetFolderName(string targetPath) => new DirectoryInfo(targetPath).Name;

/** <summary> Gets the Size of a Sub-Folder expressed in Bytes. </summary>

<param name = "targetPath"> The Path where the Sub-Folder to be Analized is Located. </param>

<returns> The Size of the Sub-Folder. </returns> */

private static long GetSubfolderSize(string targetPath)
{
var filesList = GetFileSystems(targetPath, true);
long subfolderSize = 0;

foreach(string filePath in filesList)
{
long fileSize = FileManager.GetFileSize(filePath);
subfolderSize += fileSize;
}

return subfolderSize;
}

/** <summary> Gets the Size of a Directory expressed in Bytes. </summary>

<param name = "targetPath"> The Path where the Directory to be Analized is Located. </param>

<returns> The Size of the Folder. </returns> */

public static long GetFolderSize(string targetPath)
{
IEnumerable<string> filesList = GetFileSystems(targetPath, true);
long folderSize = 0;

foreach(string filePath in filesList)
{
long fileSize = FileManager.GetFileSize(filePath);
folderSize += fileSize;
}

IEnumerable<string> subfoldersList = GetFileSystems(targetPath, false);

foreach(string subfolderPath in subfoldersList)
{
long subfolderSize = GetSubfolderSize(subfolderPath);
folderSize += subfolderSize;
}

return folderSize;
}

/** <summary> Checks if a Folder is Empty or not by Analizing its Content. </summary>

<param name = "targetPath"> The Path where the Directory to be Checked is Located. </param>

<returns> <b>true</b> if the Folder is Empty; otherwise, <b>false</b>. </returns> */

public static bool FolderIsEmpty(string targetPath)
{
string[] filesList = Directory.GetFiles(targetPath);

if(filesList == null || filesList.Length == 0)
return true;

else
return false;

}

/** <summary> Moves a Directory to a New Location. </summary>

<param name = "sourcePath"> The Path where the Directory to be Moved is Located. </param>
<param name = "destPath"> The Location where the Directory will be Moved (this must be a Root Directory Path). </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void MoveFolder(string sourcePath, string destPath)
{
string sourceFolderName = GetFolderName(sourcePath);
string targetPath = Path.Combine(destPath, sourceFolderName);

PathHelper.CheckDuplicatedPath(ref targetPath);
Directory.Move(sourcePath, targetPath);
}

/** <summary> Rename a Directory with the specific Name. </summary>

<param name = "sourcePath"> The Path where the Directory to be Renamed is Located. </param>
<param name = "newName"> The New Name of the Folder. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "DirectoryNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void RenameFolder(string sourcePath, string newName)
{
string rootPath = Path.GetDirectoryName(sourcePath);
string targetPath = Path.Combine(rootPath, newName);

PathHelper.CheckDuplicatedPath(ref targetPath);
Directory.Move(sourcePath, targetPath);
}

}

}