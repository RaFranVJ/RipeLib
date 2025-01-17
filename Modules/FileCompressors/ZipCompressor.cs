using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using RipeLib.Serializables.ArgumentsInfo.Compressor.Zip;
using RipeLib.Serializables.ArgumentsInfo.FileManager;

namespace RipeLib.Modules.FileCompressors
{
/// <summary> Initializes Compressing Tasks for Files by using the Zip algorithm. </summary>

public static class ZipCompressor
{
/// <summary> The ZIP Extension </summary>

private const string ZipExt = ".zip";

/** <summary> Adds a new Entry to the Specified <c>ZipArchive</c>. </summary>

<param name = "sourcePath"> The Path where the Entry to Add is Located. </param>
<param name = "targetStream"> The Stream where the Zip Entry will be Added. </param> */

public static void AddZipEntry(string pathToEntry, ZipArchive zipFile, Stream targetStream,
ZipEntriesInfo entryCfg = null, bool reportProgress = false)
{
entryCfg ??= new();

var fileEntry = zipFile.CreateEntry(pathToEntry, entryCfg.CompressionLvl);

if(!string.IsNullOrEmpty(entryCfg.OptionalEntryComment) )
fileEntry.Comment = entryCfg.OptionalEntryComment;

fileEntry.ExternalAttributes = entryCfg.ExternalOSAttributes;

if(entryCfg.LastWriteTime != null)
fileEntry.LastWriteTime = (DateTime)entryCfg.LastWriteTime;

using Stream entryStream = fileEntry.Open();
string msg = $"Adding: \"{Path.GetFileName(pathToEntry)}\"";

var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "Zip Compressor", msg, default);
FileManager.ProcessBuffer(entryStream, targetStream, -1, progressCfg);
}

// Get ZIP Archive

public static ZipArchive CompressStream(Stream target, ZipArchiveMode zipMode, Encoding encoding, 
ZipEntriesInfo entriesInfo, string comment = null, bool reportProgress = false, params string[] entryNames)
{
ZipArchive zipFile = new(target, zipMode, true, encoding);

if(!string.IsNullOrEmpty(comment) )
zipFile.Comment = comment;

foreach(string name in entryNames)
AddZipEntry(name, zipFile, target, entriesInfo, reportProgress);

target.Seek(0, SeekOrigin.Begin);

return zipFile;
}

/** <summary> Compresses the Contents of a File by using Zip Compression. </summary>

<param name = "inputPath"> The Path where the File to be Compressed is Located. </param>
<param name = "outputPath"> The Location where the Compressed File will be Saved. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CompressFile(string inputPath, string outputPath, ZipArchiveMode zipMode, string encodingStr,
string comment, ZipEntriesInfo entriesInfo, FileSystemSearchParams filesFilter = null)
{
PathHelper.AddExtension(ref outputPath, ZipExt);

var encoding = EncodeHelper.GetEncodingType(encodingStr);
string[] filesList = DirManager.GetEntryNames(inputPath, filesFilter);

using FileStream outputFile = FileManager.OpenWrite(outputPath);
using ZipArchive compressionStream = CompressStream(outputFile, zipMode, encoding, entriesInfo, comment, true, filesList);
}

/** <summary> Extracts the specified <c>ZipArchiveEntry</c> to a new Location. </summary>

<param name = "sourceEntry"> The Entry to Extract. </param>
<param name = "targetPath"> The Path where the Extracted Entry should be Saved. </param> */

public static Stream ExtractZipEntry(ZipArchiveEntry sourceEntry, int bufferSize,
string targetPath = null, bool reportProgress = false)
{

if(!string.IsNullOrEmpty(targetPath) )
DirManager.CheckMissingFolder(Path.GetDirectoryName(targetPath) );

using Stream outputFile = FileManager.GetOutputStream(targetPath, bufferSize);
using Stream entryStream = sourceEntry.Open();

string msg = $"Extracting: \"{sourceEntry.FullName}\"";
var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "Zip Compressor", msg, default);

FileManager.ProcessBuffer(entryStream, outputFile, -1, progressCfg);
entryStream.Seek(0, SeekOrigin.Begin);

return entryStream;
}

/** <summary> Decompresses the Contents of a File by using Zip Compression. </summary>

<param name = "inputPath"> The Path where the File to be Decompressed is Located. </param>
<param name = "outputPath"> The Location where the Decompressed contents will be Saved. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "PathTooLongException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void DecompressFile(string inputPath, string outputPath, ZipArchiveMode zipMode,
string encodingStr, int bufferSize)
{
zipMode = (zipMode != ZipArchiveMode.Create) ? zipMode : ZipArchiveMode.Read;

using FileStream inputFile = FileManager.OpenRead(inputPath, bufferSize);
var encoding = EncodeHelper.GetEncodingType(encodingStr);

using ZipArchive decompressionStream = new(inputFile, zipMode, false, encoding);
string baseName = Path.GetFileNameWithoutExtension(inputPath);

foreach(ZipArchiveEntry entry in decompressionStream.Entries)
{
string filePath = Path.Combine(outputPath, baseName, entry.Name);

using var entryStream = ExtractZipEntry(entry, bufferSize, filePath, true);
}

}

}

}