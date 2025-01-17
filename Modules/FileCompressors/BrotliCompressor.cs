using System.IO;
using System.IO.Compression;

namespace RipeLib.Modules.FileCompressors
{
/// <summary> Initializes Compression Tasks for Files by using the Brotli algorithm. </summary>

public static class BrotliCompressor
{
/// <summary> The Brotli Extension </summary>

private const string BrotliExt = ".br";

// Get Brotli Stream

public static BrotliStream CompressStream(Stream input, Stream output, CompressionLevel level, 
bool reportProgress = false)
{
BrotliStream brStream = new(output, level);
var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "Brotli Compressor", "Compressing Data");

FileManager.ProcessBuffer(input, brStream, -1, progressCfg);
output.Seek(0, SeekOrigin.Begin);

return brStream;
}

/** <summary> Compresses the Contents of a File by using the Brotli Algorithm. </summary>

<param name = "inputPath"> The Path where the File to be Compressed is Located. </param>
<param name = "outputPath"> The Location where the Compressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception> */

public static void CompressFile(string inputPath, string outputPath, CompressionLevel compressLevel, int bufferSize)
{
PathHelper.AddExtension(ref outputPath, BrotliExt);

using FileStream inputFile = FileManager.OpenRead(inputPath, bufferSize);
using FileStream outputFile = FileManager.OpenWrite(outputPath, bufferSize);

using BrotliStream compressionStream = CompressStream(inputFile, outputFile, compressLevel, true);
}

// Get Plain Stream

public static Stream DecompressStream(BrotliStream input, int bufferSize,
string outputPath = null, bool reportProgress = false)
{
Stream output = FileManager.GetOutputStream(outputPath, bufferSize);
var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "Brotli Compressor", "Decompressing Data");

FileManager.ProcessBuffer(input, output, -1, progressCfg);
output.Seek(0, SeekOrigin.Begin);

return output;
}

/** <summary> Decompresses the Contents of a File by using the Brotli Algorithm. </summary>

<param name = "inputPath"> The Path where the File to be Decompressed is Located. </param>
<param name = "outputPath"> The Location where the Decompressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception> */

public static void DecompressFile(string inputPath, string outputPath, int bufferSize)
{
PathHelper.RemoveExtension(ref outputPath, BrotliExt);

using FileStream inputFile = FileManager.OpenRead(inputPath, bufferSize);
using BrotliStream decompressionStream = new(inputFile, CompressionMode.Decompress);

using Stream outputFile = DecompressStream(decompressionStream, bufferSize, outputPath, true);
}

}

}