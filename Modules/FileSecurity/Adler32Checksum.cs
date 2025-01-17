using System;
using System.IO;
using System.Security;
using RipeLib.Modules.Other;
using RipeLib.Serializables.ArgumentsInfo.FileSecurity.Integrity;

namespace RipeLib.Modules.FileSecurity
{
/// <summary> Initializes Adler32 Digest for Files. </summary>

public static class Adler32Checksum
{
/** <summary> Gets the Adler32 Checksum of a File. </summary>

<param name = "inputPath"> The Path where the File to Digest. </param>
<param name = "outputPath"> The Location where the Adler32 Checksum will be Saved. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void DigestFile(string inputPath, string outputPath, Adler32BytesInfo adler32Cfg)
{
using FileStream inputFile = FileManager.OpenRead(inputPath);
 
PathHelper.ChangeExtension(ref outputPath, ".hash");
string hashedString = Adler32BytesChecksum.GetAdler32String(inputFile, adler32Cfg, true);

File.WriteAllText(outputPath, hashedString);
}

// Check Stream

public static void IntegrityCheck(Stream targetStream, Adler32BytesInfo adler32Cfg, string originalChecksum)
{
string checksumToCompare = Adler32BytesChecksum.GetAdler32String(targetStream, adler32Cfg);

if(!checksumToCompare.Equals(originalChecksum) )
throw new SecurityException($"Wrong Adler32 sign.\n\nFile Checksum: {checksumToCompare}\nExpected: {originalChecksum}");

}

// Integrity Check

public static void IntegrityCheck(string sourcePath, string targetPath, Adler32BytesInfo adler32Cfg)
{
using FileStream sourceFile = FileManager.OpenRead(sourcePath);
string originalChecksum = Adler32BytesChecksum.GetAdler32String(sourceFile, adler32Cfg);

using FileStream targetFile = FileManager.OpenRead(targetPath);

IntegrityCheck(targetFile, adler32Cfg, originalChecksum);
}

}

}