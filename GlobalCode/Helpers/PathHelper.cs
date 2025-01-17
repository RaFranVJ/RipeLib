using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary> Initializes some Functions for Building or Editing access Paths. </summary>

public static partial class PathHelper
{
/** <summary> Adds an Extension to the End of a Path. </summary>

<param name = "sourcePath"> The Path to be Modified. </param>
<param name = "targetExtension"> The Extesion to be Added. </param>

<returns> A Path with the new Extension. </returns> */

public static void AddExtension(ref string sourcePath, string targetExtension)
{

if(string.IsNullOrEmpty(sourcePath) || Path.GetExtension(sourcePath) == targetExtension)
return;

sourcePath += targetExtension;

CheckDuplicatedPath(ref sourcePath);
}

/** <summary> Checks if the Path is a Relative Path or not. </summary>

<param name = "targetPath"> The Path Defined by User. </param>

<returns> The New Path. </returns> */

public static string AlignPathWithAppDir(string targetPath)
{

if(string.IsNullOrEmpty(targetPath) || Path.IsPathRooted(targetPath) )
return targetPath;

return Path.Combine(AppContext.BaseDirectory, targetPath);
}

/** <summary> Builds a new Path from a Directory with the Specified Params. </summary>

<param name = "parentPath"> The Parent Path (must be a Directory name). </param>
<param name = "filePath"> The File Path to Use as a Reference Name. </param>
<param name = "fileExtension"> The Extesion to Add at the End of the Path. </param>
<param name = "pathSuffix"> A Suffix to Add to the File Name (this is Optional). </param>

<returns> The new Path Built. </returns> */

public static string BuildPathFromDir(string parentPath, string filePath, string fileExtension, string pathSuffix = null)
{
CheckExistingPath(parentPath, true);

string fileName = Path.GetFileNameWithoutExtension(filePath);
string basePath = Path.Combine(parentPath, fileName);

if(string.IsNullOrEmpty(pathSuffix) )
return basePath + fileExtension;

return $"{basePath}_{pathSuffix}{fileExtension}";
}

/** <summary> Changes the Extension from a given Path. </summary>

<param name = "sourcePath"> The Path to be Modified. </param>
<param name = "targetExtension"> The Extesion to be Changed. </param>

<returns> A Path with the new Extension. </returns> */

public static void ChangeExtension(ref string sourcePath, string targetExtension)
{

if(string.IsNullOrEmpty(sourcePath) || Path.GetExtension(sourcePath) == targetExtension)
return;

sourcePath = Path.ChangeExtension(sourcePath, targetExtension);

CheckDuplicatedPath(ref sourcePath);
}

/** <summary> Checks if a Path is already been used. </summary>

<param name = "targetPath"> The Path to be Analized. </param>

<returns> The Path Validated. </returns> */

public static void CheckDuplicatedPath(ref string targetPath)
{

if(!Directory.Exists(targetPath) && !File.Exists(targetPath) )
return;

string rootPath = Path.GetDirectoryName(targetPath);
string name = Path.GetFileName(targetPath);

string extension = string.Empty;

if(File.Exists(targetPath) )
{
extension = Path.GetExtension(targetPath);
name = Path.GetFileNameWithoutExtension(targetPath);
}

int copyIndex = 1;
var match = DuplicatedPathRegex().Match(name);

if(match.Success)
{
name = match.Groups[1].Value.Trim();
copyIndex = int.Parse(match.Groups[2].Value) + 1;
}

string newPath = targetPath;

while(Directory.Exists(newPath) || File.Exists(newPath))
{
newPath = Path.Combine(rootPath, $"{name} ({copyIndex}){extension}");
copyIndex++;
}

targetPath = newPath;
}

/** <summary> Checks if the Path provided refers to an Existing FileSystem or not. </summary>

<param name = "sourcePath"> The Path to be Analized. </param>

<param name = "createFileSystem"> A boolean that Determines if a FileSystem should be Created 
in case the Path does not exists. </param> */

public static void CheckExistingPath(string sourcePath, bool createFileSystem)
{

if(!Path.Exists(sourcePath) )
{

if(createFileSystem)
CreateFileSystem(sourcePath);

}

else
CheckDuplicatedPath(ref sourcePath);

}

/** <summary> Checks if a Path is Actually an Existing File or a Folder. </summary>

<param name = "sourcePath"> The Path to be Analized. </param>

<returns> The Attributes that Specificates the Path Type. </returns> */

public static FileAttributes CheckPathType(string sourcePath)
{

if(!Path.Exists(sourcePath) )
CreateFileSystem(sourcePath);

return File.GetAttributes(sourcePath);
}

/** <summary> Creates a FileSystem (a File or a Folder) according to the given Path Type. </summary>
<param name = "targetPath"> The Path where the FileSystem will be Created. </param> */

public static void CreateFileSystem(string targetPath)
{

if(Path.HasExtension(targetPath) )
File.Create(targetPath);

else
Directory.CreateDirectory(targetPath);

}

/** <summary> Creates a Extensions Filter from a Specific Extensions List. </summary>

<param name = "specificExtensions"> The Extensions used for Creating the Filter. </param>
<param name = "useUppercase"> A boolean that Determines if extensions should be Filtered in UpperCase or not. </param>

<returns> The Extensions Filter. </returns> */

private static Func<string, bool> CreateExtensionsFilter(List<string> includeList, List<string> excludeList)
{

return extFilter => 
{
string fileExt = Path.GetExtension(extFilter);

if(includeList.Contains(".*") )       
return excludeList.Count == 0 || !excludeList.Contains(fileExt, StringComparer.OrdinalIgnoreCase);

else if(includeList.Count == 0 && excludeList.Count > 0)
return !excludeList.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
    
return includeList.Contains(fileExt, StringComparer.OrdinalIgnoreCase) &&
!excludeList.Contains(fileExt, StringComparer.OrdinalIgnoreCase);

};

}

/** <summary> Creates a Filter from a Specific Names List. </summary>

<param name = "specificNames"> The Names used for Creating the Filter. </param>

<returns> The Names Filter. </returns> */

private static Func<string, bool> CreateNamesFilter(List<string> includeList, List<string> excludeList)
{

return namesFilter => 
{
string fileName = Path.GetFileNameWithoutExtension(namesFilter);

if(includeList.Contains("*") )
return excludeList.Count == 0 || !excludeList.Contains(fileName, StringComparer.OrdinalIgnoreCase);

else if(includeList.Count == 0 && excludeList.Count > 0)
return !excludeList.Contains(fileName, StringComparer.OrdinalIgnoreCase);
    
return includeList.Contains(fileName, StringComparer.OrdinalIgnoreCase) &&
!excludeList.Contains(fileName, StringComparer.OrdinalIgnoreCase);     
};

}

/** <summary> Creates a Extensions Filter from a Dir Length. </summary>

<param name = "maxDirLength"> A Value that Determines the Directories to Search
according to their amount of Paths inside. </param>

<returns> The Extensions Filter. </returns> */

private static Func<string, bool> CreateDirLengthFilter(int dirLength)
{
return dirFilter => dirLength < 0 || Directory.GetFileSystemEntries(dirFilter).Length == dirLength;
}

// Delete End Separator

public static void DeleteEndPathSeparator(ref string str)
{
char t = str[^1];

if (t == '/' || t == '\\')
str = str[0..^1];

}

/** <summary> Filters a List of Files by a Specific Name and a Specific Extension,
which can be in Lowercase or in Uppercase. </summary>

<param name = "sourceList"> The Files List to be Filtered. </param>
<param name = "specificNames"> A List of Specific Names used for Filtering the Files. </param>
<param name = "specificExtensions"> A List of Specific Extensions used for Filtering the Files. </param>

<returns> The Filtered Files List. </returns> */

public static void FilterFiles(ref string[] sourceList, List<string> names, List<string> extensions,
List<string> namesToExclude = null, List<string> extensionsToExclude = null)
{

if(sourceList == null || sourceList.Length == 0 || (names == null || names.Count == 0) && 
(extensions == null || extensions.Count == 0) )
return;

namesToExclude ??= new();
extensionsToExclude ??= new();

List<string> filteredList = new();

var namesFilter = CreateNamesFilter(names, namesToExclude);
var extFilter = CreateExtensionsFilter(extensions, extensionsToExclude);

HashSet<string> addedFiles = new(); // Avoid duplicates

if(extensions != null && extensions.Count > 0)
{
var filteredByExt = sourceList.Where(extFilter);

foreach(var file in filteredByExt)
{
string fileName = Path.GetFileNameWithoutExtension(file);

if(!namesToExclude.Contains(fileName) && addedFiles.Add(file) )
filteredList.Add(file);

}

}

if(names != null && names.Count > 0)
{
var filteredByName = sourceList.Where(namesFilter);
            
foreach(var file in filteredByName)
{

if(addedFiles.Add(file) )
filteredList.Add(file);

}

}

sourceList = [.. filteredList];
}

/** <summary> Filters a List of Folders by a Specific Name and by Content Length. </summary>

<param name = "sourceList"> The Folders List to be Filtered. </param>
<param name = "specificNames"> A List of Specific Names used for Filtering the Fodlers. </param>

<param name = "maxDirLength"> A Value that Determines the Directories to Search
according to their amount of Paths inside. </param>

<returns> The Filtered Dirs. </returns> */

public static void FilterDirs(ref string[] sourceList, List<string> names, int maxLength,
List<string> namesToExclude = null)
{

if(sourceList == null || sourceList.Length == 0 || (names == null || names.Count == 0) && maxLength < 0)
return;

namesToExclude ??= new();

List<string> filteredList = new();

var namesFilter = CreateNamesFilter(names, namesToExclude);
var lengthFilter = CreateDirLengthFilter(maxLength);

HashSet<string> addedDirs = new(); // Avoid duplicates

if(names != null && names.Count > 0)
{
var filteredByName = sourceList.Where(namesFilter);
            
foreach(var dir in filteredByName)
{

if(addedDirs.Add(dir) )
filteredList.Add(dir);

}

}

if(maxLength > 0)
{
var filteredByLength = sourceList.Where(lengthFilter);
            
foreach(var dir in filteredByLength)
{

if(addedDirs.Add(dir) )
filteredList.Add(dir);

}

}

sourceList = [.. filteredList];
}

/** <summary> Filters a Path from User's Input. </summary>

<param name = "targetPath"> The Path to be Filtered. </param>

<returns> The Filtered Path. </returns> */

public static string FilterPath(string targetPath)
{

if(string.IsNullOrEmpty(targetPath) )
return targetPath;

string validStr = targetPath;
string filteredPath = string.Empty;

char[] invalidPathChars = InputHelper.GetInvalidChars(false);

for(int i = 0; i < invalidPathChars.Length; i++)
{

if(validStr.Contains(invalidPathChars[i] ) )
{
filteredPath = validStr.Replace(invalidPathChars[i].ToString(), string.Empty);
validStr = filteredPath;
}

filteredPath = validStr;
}

return filteredPath.Replace("\"", string.Empty);
}

// Use Windows Format for Paths

public static string FormatWindowsPath(string filePath) => filePath.Replace('/', '\\').Replace(@"\\", @"\").Replace(@" \", @"\");

// Use Linux Format for Paths

public static string FormatLinuxPath(string filePath) => filePath.Replace('\\', '/').Replace("//", "/").Replace(" /", "/");

// Get Path to Downloads Folder

public static string GetDownloadsFolder()
{
string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

return Path.Combine(userProfile, "Downloads");
}

/** <summary> Removes the Extension from a given Path. </summary>

<param name = "sourcePath"> The Path to be Modified. </param>
<param name = "targetExtension"> The Extesion to be Removed. </param>

<returns> A Path with the Extension removed. </returns> */

public static void RemoveExtension(ref string sourcePath, string targetExtension = "")
{

if(string.IsNullOrEmpty(sourcePath) )
return;

string sourceExtension = Path.GetExtension(sourcePath);

if(string.IsNullOrEmpty(sourceExtension) )
return;

targetExtension = string.IsNullOrEmpty(targetExtension) ? sourceExtension : targetExtension;

if(sourceExtension != targetExtension)
return;

else
{
int lengthDiff = sourcePath.Length - targetExtension.Length;

sourcePath = sourcePath[..lengthDiff];

CheckDuplicatedPath(ref sourcePath);
}

}

[GeneratedRegex(@"^(.*)\((\d+)\)$")]

private static partial Regex DuplicatedPathRegex();
}