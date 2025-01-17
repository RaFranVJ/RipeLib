using System;
using System.IO;
using System.Text;
using RipeLib.Modules.Other;

namespace RipeLib.Modules.FileParsers
{
/// <summary> Initializes Parsing Task for Files by using the Base64 Algorithm. </summary>

public static class Base64Parser
{
// Get Base64 Stream

public static Stream EncodeStream(Stream input, bool isWebSafe, int bufferSize = -1,
string outputPath = null, bool reportProgress = false)
{
Stream output = FileManager.GetOutputStream(outputPath, bufferSize);

ReadOnlyMemory<byte> encodeFunc(ReadOnlyMemory<byte> bufferData)
{
var encodedStr = Base64StringParser.EncodeBytes(bufferData.Span, isWebSafe);

return Encoding.UTF8.GetBytes(encodedStr);
};

var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "Base64 Parser", "Encoding Data");
FileManager.ProcessBuffer(input, output, encodeFunc, -1, progressCfg);

output.Seek(0, SeekOrigin.Begin);

return output;
}
	
/** <summary> Encodes a File by using Base64 Encoding. </summary>

<param name = "inputPath"> The Path where the File to be Encoded is Located. </param>
<param name = "outputPath"> The Path where the Encoded File will be Saved. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "OutOfMemoryException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void EncodeFile(string inputPath, string outputPath, bool isWebSafe, int bufferSize)
{
using FileStream inputFile = FileManager.OpenRead(inputPath, bufferSize);

using Stream outputFile = EncodeStream(inputFile, isWebSafe, bufferSize, outputPath, true);
}

// Get Plain Stream

public static Stream DecodeStream(Stream input, bool isWebSafe, int bufferSize, 
string outputPath = null, bool reportProgress = false)
{
Stream output = FileManager.GetOutputStream(outputPath, bufferSize);

ReadOnlyMemory<byte> decodeFunc(ReadOnlyMemory<byte> bufferData)
{
string inputStr = Encoding.UTF8.GetString(bufferData.Span);

return Base64StringParser.DecodeString(inputStr, isWebSafe);
};

var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "Base64 Parser", "Decoding Data");
FileManager.ProcessBuffer(input, output, decodeFunc, -1, progressCfg);

output.Seek(0, SeekOrigin.Begin);

return output;
}

/** <summary> Decodes a File by using Base64 Encoding. </summary>

<param name = "inputPath"> The Path where the File to be Decoded is Located. </param>
<param name = "outputPath"> The Path where the Decoded File will be Saved. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "OutOfMemoryException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void DecodeFile(string inputPath, string outputPath, bool isWebSafe, int bufferSize)
{
using FileStream inputFile = FileManager.OpenRead(inputPath, bufferSize);

using Stream outputFile = DecodeStream(inputFile, isWebSafe, bufferSize, outputPath);
}

}

}