using System;
using System.Security.Cryptography;

/// <summary> Initializes Handling Functions for Parameters that are used on Data Encryption or Decryption. </summary>

public static class CryptoParams
{
/** <summary> Checks if the Size of the given Data Block meets the expected Range. </summary>

<param name = "blockData"> The Data Block to be Validated. </param>
<param name = "expectedSize"> The Size Excepted. </param> 

<returns> The Block Size. </returns> */

private static int CheckBlockSize(byte[] blockData, int minLength, int maxLength)
{

if(blockData.Length < minLength)
{
InputHelper.FillArray(ref blockData, minLength);

return minLength;
}

else if(blockData.Length > maxLength)
return maxLength;

return blockData.Length;
}

/** <summary> Checks if the providen Block meets the expected Size. </summary>

<remarks> In case it doesn't meet the expected Size, a similar will be Generated instead. </remarks>

<param name = "blockData"> The Data Block to be Validated. </param>
<param name = "expectedSize"> The Size Excepted. </param>  */

private static void CheckBlockSize(byte[] blockData, Limit<int> expectedLength)
{
blockData ??= Console.InputEncoding.GetBytes("<Block Data Expected>");

if(blockData.Length < expectedLength.MinValue)
InputHelper.FillArray(ref blockData, expectedLength.MinValue);

else if(blockData.Length > expectedLength.MaxValue)
Array.Resize(ref blockData, expectedLength.MaxValue);

}

/** <summary> Generates a derived Key from an Existing one, by Performing some Iterations. </summary>

<param name = "cipherKey"> The Cipher Key to Derive. </param>
<param name = "saltValue"> The Salt Value used for Reinforcing the Cipher Key. </param>
<param name = "hashType"> The Hash to be used. </param>
<param name = "iterationsCount"> The number of Iterations to be Perfomed. </param>
<param name = "expectedKeySize"> The Key Size Excepted. </param>
<param name = "expectedIterations"> The expected Number of Iterations. </param>

<returns> The derived Cipher Key. </returns> */

public static byte[] CipherKeySchedule(byte[] cipherKey, bool derivedKeys, byte[] saltValue = null,
string hashType = null, uint? iterations = null, Limit<int> expectedKeySize = null)
{
expectedKeySize ??= new(1, Array.MaxLength);
	
CheckBlockSize(cipherKey, expectedKeySize);

if(derivedKeys)
{
saltValue ??= Console.InputEncoding.GetBytes("<Enter a SaltValue>");
hashType = string.IsNullOrEmpty(hashType) ? "MD5" : hashType;

PasswordDeriveBytes derivedKey = new(cipherKey, saltValue, hashType, (int)(iterations ?? 1) );
int derivedKeySize = CheckBlockSize(cipherKey, expectedKeySize.MinValue, expectedKeySize.MaxValue);

return derivedKey.GetBytes(derivedKeySize);
}

return cipherKey;
}

/** <summary> Initializates a Vector from a Cipher Key. </summary>

<param name = "cipherKey"> The Cipher Key to used. </param>
<param name = "expectedVectorSize"> The Vector Size Excepted. </param>
<param name = "vectorIndex"> Specifies where the Vector should Start Copying the Bytes from the Cipher Key (Default Index is 0). </param>

<returns> The IV that was Generated. </returns> */

public static byte[] InitVector(byte[] cipherKey, Limit<int> expectedVectorSize, int vectorIndex = 0)
{
int vectorSize = CheckBlockSize(cipherKey, expectedVectorSize.MinValue, expectedVectorSize.MaxValue);
byte[] IV = new byte[vectorSize];

if(cipherKey.Length == vectorSize)
Array.Reverse(IV);

else
Array.Copy(cipherKey, vectorIndex, IV, 0, vectorSize);

return IV;
}

}