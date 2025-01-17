using System;
using System.IO;
using RipeLib.Modules.Other;

namespace RipeLib.Modules.FileSecurity
{
/// <summary> Initializes Exclusive-OR (XOR) Ciphering Functions for Files. </summary>

public static class XorCryptor
{
// Get Xor Stream

public static Stream CipherStream(Stream input, byte[] cipherKey, int bufferSize = -1,
string outputPath = null, bool reportProgress = false)
{
Stream output = FileManager.GetOutputStream(outputPath, bufferSize);
ReadOnlyMemory<byte> processFunc(ReadOnlyMemory<byte> bufferData) => XorBytesCryptor.CipherData(bufferData.Span, cipherKey);

var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "XOR Cryptor", "Ciphering Data");
FileManager.ProcessBuffer(input, output, processFunc, -1, progressCfg);

output.Seek(0, SeekOrigin.Begin);

return output;
}

/** <summary> Ciphers a File by using XOR Ciphering. </summary>

<param name = "inputPath"> The Path where the File to Cipher is Located. </param>
<param name = "outputPath"> The Location where the Ciphered File will be Saved. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void CipherFile(string inputPath, string outputPath, byte[] cipherKey, int bufferSize,
bool deriveKey = false, byte[] saltValue = null, string hashType = null, uint? iterations = null)
{
cipherKey = CryptoParams.CipherKeySchedule(cipherKey, deriveKey, saltValue, hashType, iterations);

using FileStream inputFile = FileManager.OpenRead(inputPath, bufferSize);

using Stream outputFile = CipherStream(inputFile, cipherKey, bufferSize, outputPath, true);
}

}

}