using System.IO;
using System.Linq;
using System.Security.Cryptography;
using RipeLib.Serializables.ArgumentsInfo.Cryptor;

namespace RipeLib.Modules.FileSecurity
{
/// <summary> Initializes Ciphering Tasks for Files by using the .NET Providers. </summary>

public static class DotNetCryptor
{
// Get Size Range for Symmetric Keys

private static Limit<int> GetKeySizeRange(SymmetricAlgorithm cipherAlg)
{
KeySizes[] keySizes = cipherAlg.LegalKeySizes;

int minSize = keySizes.Min(k => k.MinSize) / 8;
int maxSize = keySizes.Max(j => j.MaxSize) / 8;

return new(minSize, maxSize);
}

// Get Size Range for IV

private static Limit<int> GetIVLength(SymmetricAlgorithm cipherAlg) => new(cipherAlg.BlockSize / 8);

// Create ICryptoTransform by using a Symmetric Key, IV and Operation Mode

private static ICryptoTransform GetTransform(bool isForEncryption, SymmetricAlgorithm cipherAlg, 
byte[] cipherKey = null, byte[] IV = null)
{

if(isForEncryption)
return cipherAlg.CreateEncryptor(cipherKey, IV);

return cipherAlg.CreateDecryptor(cipherKey, IV);
}

/** <summary> Gets a Path to the exported Key. </summary>

<param name = "filePath"> The Path to the File where the Key should be Saved. </param>

<returns> The Path to the generated Key. </returns> */

private static string GetCryptoPath(string containerName, string filePath)
{
return PathHelper.BuildPathFromDir(containerName, filePath, ".bin");
}

// Returns a new Key or the given Key if its Length is Valid

public static byte[] ValidateKey(SymmetricAlgorithm cipherAlg, bool isForEncryption, bool deriveKeys, string keyPath,
byte[] key = null, byte[] salt = null, string hashType = null, uint? iterations = null)
{
Limit<int> keySizeRange = GetKeySizeRange(cipherAlg);
byte[] dK = key;

if(key == null && isForEncryption)
{
cipherAlg.GenerateKey();
dK = cipherAlg.Key;

File.WriteAllBytes(keyPath, dK);
}

else if(key == null && !isForEncryption)
dK = File.ReadAllBytes(keyPath);

return CryptoParams.CipherKeySchedule(dK, deriveKeys, salt, hashType, iterations, keySizeRange);
}

// Validate IV

public static byte[] ValidateIV(SymmetricAlgorithm cipherAlg, bool isForEncryption, bool randomIV,
byte[] key, string ivPath, byte[] IV = null)
{
Limit<int> ivLength = GetIVLength(cipherAlg);
byte[] dV = IV;

if(IV == null && randomIV && isForEncryption)
{
cipherAlg.GenerateIV();
dV = cipherAlg.IV;

File.WriteAllBytes(ivPath, dV);
}

else if(IV == null && randomIV && !isForEncryption)
dV = File.ReadAllBytes(ivPath);

else if(IV == null && !randomIV)
dV = CryptoParams.InitVector(key, ivLength);

return CryptoParams.CipherKeySchedule(dV, false, expectedKeySize: ivLength);
}

// Get Crypto Stream

public static CryptoStream CipherStream(Stream input, Stream output, bool isForEncryption,
SpecificCryptoInfo cfg = default, string keyPath = default, string ivPath = default, 
bool reportProgress = false)
{
cfg ??= new();

keyPath = string.IsNullOrEmpty(keyPath) ? cfg.KeyContainer + "MyCipherKey.bin" : keyPath;
ivPath = string.IsNullOrEmpty(ivPath) ? cfg.IVContainer + "MyIV.bin" : ivPath;

#pragma warning disable SYSLIB0045 // Type or member is obsolete
SymmetricAlgorithm cipherAlg = SymmetricAlgorithm.Create(cfg.ProviderName);

cipherAlg.Mode = cfg.CipheringMode;
cipherAlg.Padding = cfg.DataPadding;

byte[] key = ValidateKey(cipherAlg, isForEncryption, cfg.DeriveKeys, keyPath, 
cfg.CipherKey, cfg.SaltValue, cfg.HashType, cfg.IterationsCount);

byte[] iv = ValidateIV(cipherAlg, isForEncryption, cfg.RandomizeIVS, key, ivPath, cfg.IV);

ICryptoTransform transform = GetTransform(isForEncryption, cipherAlg, key, iv);
CryptoStream cipheredStream = new(output, transform, CryptoStreamMode.Write);

string msg = isForEncryption ? "Encrypting Data" : "Decrypting Data";
var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, ".NET Cryptor", msg);

FileManager.ProcessBuffer(input, cipheredStream, -1, progressCfg);
cipheredStream.FlushFinalBlock();

return cipheredStream;
}

/** <summary> Performs Generic Task for Ciphering on Files. </summary>

<param name = "inputPath"> Th Path where the File to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted File will be Saved. </param>
<param name = "isForEncryption"> Determines if the Files should be Encrypted or not. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception> */

private static void CipherFile(string inputPath, string outputPath, bool isForEncryption, SpecificCryptoInfo cfg)
{
using FileStream inputFile = FileManager.OpenRead(inputPath);
using FileStream outputFile = FileManager.OpenWrite(outputPath);

string keyPath = GetCryptoPath(cfg.KeyContainer, inputPath);
string ivPath = GetCryptoPath(cfg.IVContainer, inputPath);

using var cipheredStream = CipherStream(inputFile, outputFile, isForEncryption, cfg, keyPath, ivPath, true);
}

/** <summary> Encrypts a File by using a Generic Ciphering. </summary>

<param name = "inputPath"> The Path where the File to be Encrypted is Located. </param>
<param name = "outputPath"> The Location where the Encrypted File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception> */

public static void Encrypt(string inputPath, string outputPath, SpecificCryptoInfo cfg = null)
{
CipherFile(inputPath, outputPath, true, cfg);
}

/** <summary> Decrypts a File by using a Generic Decryption. </summary>

<param name = "inputPath"> The Path where the File to be Decrypted is Located. </param>
<param name = "outputPath"> The Location where the Decrypted File will be Saved. </param>

<exception cref = "FileNotFoundException"></exception>
<exception cref = "IOException"></exception> */

public static void Decrypt(string inputPath, string outputPath, SpecificCryptoInfo cfg = null)
{
CipherFile(inputPath, outputPath, false, cfg);
}

}

}