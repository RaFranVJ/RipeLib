using System.IO;
using System.Security;
using RipeLib.Modules.Other;

namespace RipeLib.Modules.FileSecurity
{
/// <summary> Initializes Digest Tasks for Files using the .NET Providers. </summary>

public static class DotNetHasher
{	
/** <summary> Hashes a File by using a Generic Digest. </summary>

<param name = "inputPath"> The Path where the File to be Hashed is Located. </param>
<param name = "outputPath"> The Path where the Hashed File will be Saved. </param>
<param name = "md5Info"> Specifies how Data should be Hashed. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "NotSupportedException"></exception>
<exception cref = "OutOfMemoryException"></exception>
<exception cref = "SecurityException"></exception>
<exception cref = "UnauthorizedAccessException"></exception> */

public static void HashFile(string inputPath, string outputPath, bool useHmac, string providerName,
StringCase strCase, byte[] authCode = null)
{
using FileStream inputFile = FileManager.OpenRead(inputPath);
 
PathHelper.ChangeExtension(ref outputPath, ".hash");
string hashStr = StringDigest.DigestData(inputFile, useHmac, providerName, strCase, authCode);

File.WriteAllText(outputPath, hashStr);
}

// Check Stream

public static void IntegrityCheck(Stream targetStream, bool useHmac, string providerName,
StringCase strCase, string originalHash, byte[] authCode = null)
{
string hashToCompare = StringDigest.DigestData(targetStream, useHmac, providerName, strCase, authCode);

if(!hashToCompare.Equals(originalHash) )
throw new SecurityException($"This file seems to be Modified.\n\nFile Hash: {hashToCompare}\nExpected: {originalHash}");

}

// Integrity Check

public static void IntegrityCheck(string sourcePath, string targetPath, bool useHmac,
string providerName, StringCase strCase, byte[] authCode = null)
{
using FileStream sourceFile = FileManager.OpenRead(sourcePath);
string originalHash = StringDigest.DigestData(sourceFile, useHmac, providerName, strCase, authCode);

using FileStream targetFile = FileManager.OpenRead(targetPath);

IntegrityCheck(targetFile, useHmac, providerName, strCase, originalHash, authCode);
}

}

}