using System;
using System.Buffers;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;

namespace RipeLib.Modules
{
/// <summary> Initializes Several Functions for Archives (such as File Opening, Writing, Closing and Others). </summary> 

public static class FileManager
{
/** <summary> Avoids the Replacement of a File by Changing its Path. </summary>

<param name = "targetPath"> The Path to be Analized. </param> */

public static void AvoidFileReplacement(string targetPath, bool doReplaceFile)
{

if(!doReplaceFile)
PathHelper.CheckDuplicatedPath(ref targetPath);

}

/** <summary> Changes a Path to a new one. </summary>

<param name = "oldPath"> The Path to be Changed. </param>
<param name = "newPath"> The New Path of the File. </param>

<returns> The Path Changed. </returns> */

public static string ChangePath(string oldPath, string newPath)
{

if(newPath == oldPath)
{
string rootPath = Path.GetDirectoryName(newPath);
char namePrefix = InputHelper.GenerateStringComplement();

string fileName = Path.GetFileNameWithoutExtension(newPath);
string fileExtension = Path.GetExtension(newPath);

return Path.Combine(rootPath, namePrefix + fileName + fileExtension);
}

return newPath;
}

/** <summary> Copies an Archive to a New Location. </summary>

<param name = "sourcePath"> The Path where the Archive to be Copied is Located. </param>
<param name = "destPath"> The Location where the File will be Copied (must be a Folder). </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CopyFile(string sourcePath, string destPath, bool replaceFile)
{
DirManager.CheckMissingFolder(destPath);

string targetPath = Path.Combine(destPath, Path.GetFileName(sourcePath) );
AvoidFileReplacement(targetPath, replaceFile);

File.Copy(sourcePath, targetPath, replaceFile);
}

/** <summary> Creates a Link that Serves as a Direct Access to a Specific Archive. </summary>

<param name = "sourcePath"> The Path of the File where the Direct Access will be Created from. </param>
<param name = "targetPath"> The Path where the Direct Access will be Created. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CreateDirectAccess(string sourcePath, string targetPath)
{
PathHelper.AddExtension(ref targetPath, ".lnk");

File.CreateSymbolicLink(targetPath, sourcePath);
}

/** <summary> Creates a new Archive on the specific Location. </summary>

<param name = "targetPath"> The Path where the File will be Created. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CreateFile(string targetPath, string fileName, int fileSize, FileOptions options, bool replaceFile)
{
DirManager.CheckMissingFolder(targetPath);

string newFilePath = Path.Combine(targetPath, fileName);
AvoidFileReplacement(newFilePath, replaceFile);

File.Create(newFilePath, fileSize, options);
}

/** <summary> Deletes an Archive from the Current Device. </summary>

<param name = "targetPath"> The Path where the File to be Deleted is Located. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void DeleteFile(string targetPath) => File.Delete(targetPath);

/** <summary> Checks if a File is Empty or not by Analizing its Content. </summary>

<param name = "targetPath"> The Path where the Archive to be Checked is Located. </param>

<returns> <b>true</b> if the File is Empty; otherwise, returns <b>false</b>. </returns> */

public static bool FileIsEmpty(string targetPath) => GetFileSize(targetPath) == 0;

/** <summary> Gets the Size in Bytes of an Archive. </summary>

<param name = "targetPath"> The Path to the File where the Properties will be Obtained from. </param>

<returns> The Size of the File. </returns> */

public static long GetFileSize(string targetPath) => new FileInfo(targetPath).Length;

// Get Output Stream (FileStream or MemoryStream)

public static Stream GetOutputStream(string outputPath = null, int bufferSize = -1)
{
bool useFilePath = !string.IsNullOrEmpty(outputPath);

bufferSize = useFilePath && bufferSize <= 0 ? 4096 : bufferSize;

return useFilePath ? OpenWrite(outputPath, bufferSize) : new MemoryStream();
}

/** <summary> Moves an Archive to a New Location. </summary>

<param name = "sourcePath"> The Path where the Archive to be Moved is Located. </param>
<param name = "destPath"> The Location where the Archive will be Moved (this must be a Directory). </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void MoveFile(string sourcePath, string destPath, bool replaceFile)
{
DirManager.CheckMissingFolder(destPath);

string targetPath = Path.Combine(destPath, Path.GetFileName(sourcePath) );
AvoidFileReplacement(targetPath, replaceFile);

File.Move(sourcePath, targetPath, replaceFile);
}

// Get Target Bytes

private static int CalculateTargetBytes(Stream inputStream, long maxBytes)
{
long targetBytes;

try
{
targetBytes = maxBytes < 0 ? inputStream.Length : maxBytes;
}

catch(NotSupportedException)
{
targetBytes = maxBytes;
}

return (int)targetBytes;
}

/** <summary> Process the Data Contained on a Stream and Writes it to another Stream. </summary>

<param name="inputStream"> The Stream which Contains the Data to be Processed. </param>
<param name="outputStream"> The Stream where the Processed Data will be Written. </param>
<param name="maxBytes"> The maximum number of bytes to be processed (-1 for unlimited). </param> */

public static void ProcessBuffer(Stream inputStream, Stream outputStream, 
long maxBytes = -1, ProgressBarTextConfig progressBarCfg = null)
{
int bufferSize = MemoryManager.GetBufferSize(inputStream);

var memoryOwner = MemoryPool<byte>.Shared.Rent(bufferSize);
Memory<byte> bufferData = memoryOwner.Memory;

long totalBytesRead = 0;
int bytesRead;

var progressBar = ProgressWindow.CreateNew(progressBarCfg);
int targetBytes = CalculateTargetBytes(inputStream, maxBytes);

void writeData(ReadOnlyMemory<byte> data, int count)
{
outputStream.Write(data.Slice(0, count).Span);
totalBytesRead += count;

progressBar?.SetProgress( (int)totalBytesRead, targetBytes, progressBarCfg.DisplayFormat, progressBarCfg.Body);
}

// Read until end of stream

if(maxBytes < 0)
{

while( (bytesRead = inputStream.Read(bufferData.Span) ) > 0)
writeData(bufferData, bytesRead);
        
}

// Read until maxBytes is reached
	
else
{

while(totalBytesRead < maxBytes)
{
int bytesToRead = (int)Math.Min(bufferSize, Math.Min(maxBytes - totalBytesRead, int.MaxValue) );
bytesRead = inputStream.Read(bufferData.Span.Slice(0, bytesToRead) );

if(bytesRead <= 0)
break;

writeData(bufferData, bytesRead);
}

}

outputStream.Flush();
memoryOwner.Dispose();

progressBar?.Destroy();
}


/** <summary> Processes the data contained in an input stream, applies a transformation function, 
and writes the transformed data to an output stream. </summary>

<param name="inputStream">The stream which contains the data to be processed.</param>
<param name="outputStream">The stream where the processed data will be written.</param>
<param name="processFunc">A function that determines how the buffer should be transformed.</param>
<param name="maxBytes">The maximum number of bytes to process (-1 for no limit).</param> */

public static void ProcessBuffer(Stream inputStream, Stream outputStream,
Func<ReadOnlyMemory<byte>, ReadOnlyMemory<byte>> processFunc, long maxBytes = -1,
ProgressBarTextConfig progressBarCfg = null)
{
int bufferSize = MemoryManager.GetBufferSize(inputStream);

var memoryOwner = MemoryPool<byte>.Shared.Rent(bufferSize);
Memory<byte> bufferData = memoryOwner.Memory;

long totalBytesRead = 0;
int bytesRead;

var progressBar = ProgressWindow.CreateNew(progressBarCfg);
int targetBytes = CalculateTargetBytes(inputStream, maxBytes);

void writeData(ReadOnlyMemory<byte> data, int count)
{
var processedData = processFunc(data.Slice(0, count) );
outputStream.Write(processedData.Span);

totalBytesRead += count;
progressBar?.SetProgress( (int)totalBytesRead, targetBytes, progressBarCfg.DisplayFormat, progressBarCfg.Body);
}

// Read until end of stream

if(maxBytes < 0)
{

while( (bytesRead = inputStream.Read(bufferData.Span) ) > 0)
writeData(bufferData, bytesRead);
        
}

// Read until maxBytes is reached

else
{
	
while(totalBytesRead < maxBytes)
{
int bytesToRead = (int)Math.Min(bufferSize, Math.Min(maxBytes - totalBytesRead, int.MaxValue) );
bytesRead = inputStream.Read(bufferData.Span.Slice(0, bytesToRead) );

if(bytesRead <= 0)
break;

writeData(bufferData, bytesRead);
}

}

outputStream.Flush();
memoryOwner.Dispose();

progressBar?.Destroy();
}

/** <summary> Processes the data contained in an input stream, applies a transformation function, 
and writes the transformed data to an output stream. </summary>

<param name="inputStream">The stream which contains the data to be processed.</param>
<param name="outputStream">The stream where the processed data will be written.</param>
<param name="processFunc">A function that determines how the buffer should be transformed.</param>
<param name="bufferSize">The size in bytes of the buffer (default is 4096 bytes).</param>
<param name="maxBytes">The maximum number of bytes to process (-1 for no limit).</param>  */

public static void ProcessBuffer(Stream inputStream, Stream outputStream, 
Func<ReadOnlyMemory<byte>, int, ReadOnlyMemory<byte>> processFunc, long maxBytes = -1, 
ProgressBarTextConfig progressBarCfg = null)
{
int bufferSize = MemoryManager.GetBufferSize(inputStream);

var memoryOwner = MemoryPool<byte>.Shared.Rent(bufferSize);
Memory<byte> bufferData = memoryOwner.Memory;

long totalBytesRead = 0;
int bytesRead;

var progressBar = ProgressWindow.CreateNew(progressBarCfg);
int targetBytes = CalculateTargetBytes(inputStream, maxBytes);

void writeData(ReadOnlyMemory<byte> data, int count)
{
ReadOnlyMemory<byte> processedData = processFunc(data, count);
outputStream.Write(processedData.Span);

totalBytesRead += count;
progressBar?.SetProgress( (int)totalBytesRead, targetBytes, progressBarCfg.DisplayFormat, progressBarCfg.Body);
}

// Read until end of Stream

if(maxBytes < 0)
{

while( (bytesRead = inputStream.Read(bufferData.Span) ) > 0)
writeData(bufferData, bytesRead);
        
}

// Read until maxBytes is Reached

else
{

while(totalBytesRead < maxBytes)
{
int bytesToRead = (int)Math.Min(bufferSize, Math.Min(maxBytes - totalBytesRead, int.MaxValue) );
bytesRead = inputStream.Read(bufferData.Span.Slice(0, bytesToRead));

if(bytesRead <= 0)
break;

writeData(bufferData, bytesRead);
}

}

outputStream.Flush();
memoryOwner.Dispose();

progressBar?.Destroy();
}

// Process Stream in Batches

public static void ProcessStreamView(string inputPath, string outputPath, Func<Stream, Stream, Stream> processFunc,
int bufferSize = 4096, int reservedBytes = 64)
{
long fileSize = GetFileSize(inputPath);
long offset = 0;

while(offset < fileSize)
{
long blockSize = Math.Min(bufferSize, fileSize - offset);

using var inputStream = OpenReadView(inputPath, offset, blockSize);
using var outputStream = OpenWriteView(outputPath, offset, blockSize + reservedBytes); // Error

using Stream transform = processFunc(inputStream, outputStream);

offset += blockSize;
}

}

// Optimized Variant for Opening Files

public static FileStream OpenFile(string filePath, FileMode openMode, int bufferSize = 4096)
{
return new(filePath, openMode, default, default, bufferSize, FileOptions.SequentialScan);
}

// Optimized Variant for Opening Files (ReadMode)

public static FileStream OpenRead(string filePath, int bufferSize = 4096)
{
return new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan);
}

// Optimized Variant for Opening Files (WriteMode)

public static FileStream OpenWrite(string filePath, int bufferSize = 4096)
{
return new(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read, bufferSize, FileOptions.SequentialScan);
}

// Open Stream View

private static MemoryMappedViewStream OpenStreamView(string filePath, MemoryMappedFileAccess access,
long offset = 0, long size = 0)
{

if(!File.Exists(filePath) )
{
using FileStream fs = new(filePath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);

fs.SetLength(size <= 0 ? 512 : size);
}

using var mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open);

return mmf.CreateViewStream(offset, size, access);
}

// Open Stream View for Reading

public static MemoryMappedViewStream OpenReadView(string filePath, long offset = 0, long size = 0)
{
return OpenStreamView(filePath, MemoryMappedFileAccess.Read, offset, size);
}

// Open Stream View for Writing

public static MemoryMappedViewStream OpenWriteView(string filePath, long offset = 0, long size = 0)
{
return OpenStreamView(filePath, MemoryMappedFileAccess.ReadWrite, offset, size);
}

/** <summary> Renames an Archive with the specific Name. </summary>

<param name = "sourcePath"> The Path where the Archive to be Renamed is Located. </param>
<param name = "newName"> The new Name of the File. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void RenameFile(string sourcePath, string newName)
{
string targetPath = Path.Combine(Path.GetDirectoryName(sourcePath), newName);

PathHelper.CheckDuplicatedPath(ref targetPath);

File.Move(sourcePath, targetPath);
}

}

}