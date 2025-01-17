using System;
using System.IO;
using System.Security.Cryptography;

namespace RipeLib.Modules.Other
{
/** <summary> Initializes Generic Digest functions for Strings. </summary> */

public static class StringDigest
{
/** <summary> Gets the Length an Auth Code must Have. </summary>
<returns> The Auth Code Length. </returns> */

private static Limit<int> GetAuthCodeLength(HMAC hmacAlg) => new(hmacAlg.OutputBlockSize / 8);

/** <summary> Hashes an Array of Bytes by using a Generic Digest. </summary>

<param name = "inputBytes"> The Bytes to be Hashed. </param>
<param name = "useHmac"> A boolean that Determines if HMAC should be used or not. </param>
<param name = "cipherKey"> The Cipher Key to use as an Authentification Code. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "OutOfMemoryException"></exception>

<returns> The String Digest. </returns> */

public static string DigestData(byte[] inputBytes, bool useHmac, string providerName,
StringCase strCase = StringCase.Lower, byte[] authCode = null)
{
byte[] hashedBytes;

if(useHmac)
{
authCode ??= Console.InputEncoding.GetBytes("<Enter Auth Code>");

#pragma warning disable SYSLIB0045 // Type or member is obsolete
HMAC digestAlgForHmac = HMAC.Create(providerName);

var authCodeLength = GetAuthCodeLength(digestAlgForHmac);
digestAlgForHmac.Key = CryptoParams.CipherKeySchedule(authCode, false, expectedKeySize: authCodeLength);

hashedBytes = digestAlgForHmac.ComputeHash(inputBytes);
}

else
hashedBytes = HashAlgorithm.Create(providerName).ComputeHash(inputBytes);

return InputHelper.ConvertHexString(hashedBytes, strCase);
}

/** <summary> Hashes a Stream by using a Generic Digest. </summary>

<param name = "inputStream"> The Stream which contains the Data to be Hashed. </param>
<param name = "useHmac"> A boolean that Determines if HMAC should be used or not. </param>
<param name = "cipherKey"> The Cipher Key to use as an Authentification Code. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "CryptographicException"></exception>
<exception cref = "IOException"></exception>
<exception cref = "ObjectDisposedException"></exception>
<exception cref = "OutOfMemoryException"></exception>

<returns> The String Digest. </returns> */

public static string DigestData(Stream inputStream, bool useHmac, string providerName, 
StringCase strCase = StringCase.Lower, byte[] authCode = null)
{
MemoryManager.CheckStreamSize(inputStream);

byte[] hashedBytes;

if(useHmac)
{
authCode ??= Console.InputEncoding.GetBytes("<Enter Auth Code>");

HMAC digestAlgForHmac = HMAC.Create(providerName);
var authCodeLength = GetAuthCodeLength(digestAlgForHmac);

digestAlgForHmac.Key = CryptoParams.CipherKeySchedule(authCode, false, expectedKeySize: authCodeLength);
hashedBytes = digestAlgForHmac.ComputeHash(inputStream);
}

else
hashedBytes = HashAlgorithm.Create(providerName).ComputeHash(inputStream);

return InputHelper.ConvertHexString(hashedBytes, strCase);
}

// JS Fun

public static object DigestDataJS(string arg, string arg2, string arg3, string arg4, string arg5)
{
byte[] data = Console.InputEncoding.GetBytes(arg);
byte[] key = string.IsNullOrEmpty(arg5) ? default : Console.InputEncoding.GetBytes(arg5);

if(!bool.TryParse(arg2, out bool useHmac) )
useHmac = default;

if(!Enum.TryParse(arg4, out StringCase strCase) )
strCase = StringCase.Lower;

return DigestData(data, useHmac, arg3, strCase, key);
}

}

}