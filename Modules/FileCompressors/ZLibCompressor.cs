using System;
using System.IO;
using System.IO.Compression;

namespace RipeLib.Modules.FileCompressors
{
/// <summary> Initializes Compression Tasks for Files by using the ZLib algorithm. </summary>

public static class ZLibCompressor
{
/// <summary> The ZLib Extension </summary>

private const string ZLibExt = ".zlib";

// Get ZLIB Stream

public static ZLibStream CompressStream(Stream input, Stream output, CompressionLevel level,
bool reportProgress = false)
{
ZLibStream zlStream = new(output, level);
var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "ZLib Compressor", "Compressing Data");

FileManager.ProcessBuffer(input, zlStream, -1, progressCfg);
output.Seek(0, SeekOrigin.Begin);

return zlStream;
}

/** <summary> Compresses the Contents of a File by using the ZLib Algorithm. </summary>

<param name = "inputPath"> The Path where the File to be Compressed is Located. </param>
<param name = "outputPath"> The Location where the Compressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CompressFile(string inputPath, string outputPath, CompressionLevel level, int bufferSize)
{
PathHelper.AddExtension(ref outputPath, ZLibExt);

using FileStream inputFile = FileManager.OpenRead(inputPath, bufferSize);
using FileStream outputFile = FileManager.OpenWrite(outputPath, bufferSize);

using ZLibStream compressionStream = CompressStream(inputFile, outputFile, level, true);
}

// Get Plain Stream

public static Stream DecompressStream(ZLibStream input, int bufferSize,
string outputPath = default, bool reportProgress = false)
{
Stream output = FileManager.GetOutputStream(outputPath, bufferSize);
var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "ZLib Compressor", "Decompressing Data");

FileManager.ProcessBuffer(input, output, -1, progressCfg);
output.Seek(0, SeekOrigin.Begin);

return output;
}

/** <summary> Decompresses the Contents of a File by using the ZLib Algorithm. </summary>

<param name = "inputPath"> The Path where the File to be Decompressed is Located. </param>
<param name = "outputPath"> The Location where the Decompressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void DecompressFile(string inputPath, string outputPath, int bufferSize)
{
PathHelper.RemoveExtension(ref outputPath, ZLibExt);

using FileStream inputFile = FileManager.OpenRead(inputPath, bufferSize);
using ZLibStream decompressionStream = new(inputFile, CompressionMode.Decompress);

using Stream outputFile = DecompressStream(decompressionStream, bufferSize, outputPath, true);
}

}

}