using K4os.Compression.LZ4.Streams;
using System;
using System.IO;
using RipeLib.Modules;

namespace RipeLib.Modules.FileCompressors
{
/// <summary> Initializes Compression Tasks for Files by using the LZ4 algorithm. </summary>

public static class LZ4Compressor
{
/// <summary> The LZ4 Extension </summary>

private const string LZ4Ext = ".lz4";

// Get LZ4 Stream

public static LZ4EncoderStream CompressStream(Stream input, Stream output, LZ4EncoderSettings settings,
bool reportProgress = false)
{
LZ4EncoderStream lz4Stream = LZ4Stream.Encode(output, settings);
var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "LZ4 Compressor", "Compressing Data");

FileManager.ProcessBuffer(input, lz4Stream, -1, progressCfg);
output.Seek(0, SeekOrigin.Begin);

return lz4Stream;
}

/** <summary> Compresses the Contents of a File by using LZ4 Compression. </summary>

<param name = "inputPath"> The Access Path where the File to be Compressed is Located. </param>
<param name = "outputPath"> The Location where the Compressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CompressFile(string inputPath, string outputPath, LZ4EncoderSettings encoderSettings, int bufferSize)
{
PathHelper.AddExtension(ref outputPath, LZ4Ext);

using FileStream inputFile = FileManager.OpenRead(inputPath, bufferSize);
using FileStream outputFile = FileManager.OpenWrite(outputPath, bufferSize);

using LZ4EncoderStream compressionStream = CompressStream(inputFile, outputFile, encoderSettings, true);
}

// Get Plain Stream

public static Stream DecompressStream(LZ4DecoderStream input, int bufferSize,
string outputPath = null, bool reportProgress = false)
{
Stream output = FileManager.GetOutputStream(outputPath, bufferSize);
var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "LZ4 Compressor", "Decompressing Data");

FileManager.ProcessBuffer(input, output, -1, progressCfg);
output.Seek(0, SeekOrigin.Begin);

return output;
}

/** <summary> Decompresses the Contents of a File by using LZ4 Compression. </summary>

<param name = "inputPath"> The Path where the File to be Decompressed is Located. </param>
<param name = "outputPath"> The Location where the Decompressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void DecompressFile(string inputPath, string outputPath, LZ4DecoderSettings decoderSettings, int bufferSize)
{
PathHelper.RemoveExtension(ref outputPath, LZ4Ext);

using FileStream inputFile = FileManager.OpenRead(inputPath, bufferSize);
using LZ4DecoderStream decompressionStream = LZ4Stream.Decode(inputFile, decoderSettings);

using Stream outputFile = DecompressStream(decompressionStream, bufferSize, outputPath, true);
}

}

}