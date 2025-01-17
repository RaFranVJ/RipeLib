using SevenZip;
using SevenZip.Compression.LZMA;
using System;
using System.IO;

namespace RipeLib.Modules.FileCompressors
{
/// <summary> Initializes Compression Tasks for Files by using the LZMA algorithm. </summary>

public static class LzmaCompressor
{
/// <summary> The LZMA Extension </summary>

private const string LZMAExt = ".lzma";

/** <summary> An Interface used for Displaying the Progress of a Task from the Compressor. </summary>
<remarks> I'm keeping this as <c>null</c> for Console Projects. </remarks> */

private static readonly ICodeProgress processReporter = null;

// Compress LZMA Stream

public static BinaryStream CompressStream(Stream input, bool useSizeInfo, long inputSize, long outputSize,
CoderPropID[] propIDs = null, object[] props = null, Endian? endian = null, string outputPath = null)
{
using BinaryStream output = BinaryStream.GetOutputStream(outputPath);
Encoder fileCompressor = new();

if(propIDs != null && props != null)
fileCompressor.SetCoderProperties(propIDs, props);

fileCompressor.WriteCoderProperties(output);
long plainDataSize = inputSize;

if(useSizeInfo)
{
plainDataSize = input.Length;

output.WriteLong(plainDataSize, (Endian)endian);
}

fileCompressor.Code(input, output, plainDataSize, outputSize, processReporter);
output.Seek(0, SeekOrigin.Begin);

return output;
}

/** <summary> Compresses the Contents of a File by using LZMA Compression. </summary>

<param name = "inputPath"> The Path where the File to be Compressed is Located. </param>
<param name = "outputPath"> The Location where the Compressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IndexOutOfRangeException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "NullReferenceException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CompressFile(string inputPath, string outputPath, bool useSizeInfo, long inputSize,
long outputSize, CoderPropID[] propIDs = null, object[] props = null, Endian? endian = null)
{
PathHelper.AddExtension(ref outputPath, LZMAExt);

using FileStream inputFile = FileManager.OpenRead(inputPath);

using var outputFile = CompressStream(inputFile, useSizeInfo, inputSize, outputSize, propIDs, props, endian, outputPath);
}

/** <summary> Decompresses the Contents of a File by using LZMA Compression. </summary>

<param name = "inputPath"> The Path where the File to be Decompressed is Located. </param>
<param name = "outputPath"> The Location where the Decompressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IndexOutOfRangeException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "NullReferenceException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static Stream DecompressStream(BinaryStream input, bool useSizeInfo, long inputSize,
long outputSize, int propsCount = 5, Endian? endian = null, string outputPath = null)
{
Stream output = FileManager.GetOutputStream(outputPath);
Decoder fileDecompressor = new();

byte[] coderPropsInfo = input.ReadBytes(propsCount);
fileDecompressor.SetDecoderProperties(coderPropsInfo);

long plainDataSize = useSizeInfo ? input.ReadLong( (Endian)endian) : outputSize;
fileDecompressor.Code(input, output, inputSize, plainDataSize, processReporter);

output.Flush();
output.Seek(0, SeekOrigin.Begin);

return output;
}

/** <summary> Compresses the Contents of a File by using LZMA Compression. </summary>

<param name = "inputPath"> The Path where the File to be Compressed is Located. </param>
<param name = "outputPath"> The Location where the Compressed File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IndexOutOfRangeException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "NullReferenceException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void DecompressFile(string inputPath, string outputPath, bool useSizeInfo, long inputSize,
long outputSize, int propsCount = 5, Endian? endian = null)
{
PathHelper.RemoveExtension(ref outputPath, LZMAExt);

using BinaryStream inputFile = BinaryStream.OpenRead(inputPath);

using var outputFile = DecompressStream(inputFile, useSizeInfo, inputSize, outputSize, propsCount, endian, outputPath);
}

}

}