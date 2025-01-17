using ICSharpCode.SharpZipLib.BZip2;
using System;
using System.IO;

namespace RipeLib.Modules.FileCompressors
{
/// <summary> Initializes Compression Tasks for Files by using the BZip2 algorithm. </summary>

public static class BZip2Compressor
{
/// <summary> The BZip2 Extension </summary>

private const string BZip2Ext = ".bz2";

// Get BZip2 Stream

public static BZip2OutputStream CompressStream(Stream input, Stream output, int blockSize, bool reportProgress = false)
{
using BZip2OutputStream bz2Stream = new(output, blockSize);
var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "BZip2 Compressor", "Compressing Data");

FileManager.ProcessBuffer(input, bz2Stream, -1, progressCfg);
output.Seek(0, SeekOrigin.Begin);

return bz2Stream;
}

/** <summary> Compresses the Contents of a File by using BZip2 Compression. </summary>

<param name = "inputPath"> The Access Path where the File to be Compressed is Located. </param>
<param name = "outputPath"> The Location where the Compressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CompressFile(string inputPath, string outputPath, int blockSize, int bufferSize)
{
PathHelper.AddExtension(ref outputPath, BZip2Ext);

using FileStream inputFile = FileManager.OpenRead(inputPath, bufferSize);
using FileStream outputFile = FileManager.OpenWrite(outputPath, bufferSize);

using BZip2OutputStream compressionStream = CompressStream(inputFile, outputFile, blockSize, true);
}

// Get Plain Stream

public static Stream DecompressStream(BZip2InputStream input, int bufferSize,
string outputPath = null, bool reportProgress = false)
{
Stream output = FileManager.GetOutputStream(outputPath, bufferSize);
var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "BZip2 Compressor", "Decompressing Data");

FileManager.ProcessBuffer(input, output, -1, progressCfg);
output.Seek(0, SeekOrigin.Begin);

return output;
}

/** <summary> Decompresses the Contents of a File using BZip2 Compression. </summary>

<param name = "inputPath"> The Path where the File to be Decompressed is Located. </param>
<param name = "outputPath"> The Location where the Decompressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void DecompressFile(string inputPath, string outputPath, int bufferSize)
{
PathHelper.RemoveExtension(ref outputPath, BZip2Ext);

using FileStream inputFile = FileManager.OpenRead(inputPath, bufferSize);
using BZip2InputStream decompressionStream = new(inputFile);

using Stream outputFile = DecompressStream(decompressionStream, bufferSize, outputPath, true);
}

}

}