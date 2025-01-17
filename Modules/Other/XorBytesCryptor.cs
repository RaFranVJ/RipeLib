using System;

namespace RipeLib.Modules.Other
{
/// <summary> Initializes Xor Ciphering functions for Bytes. </summary>

public static class XorBytesCryptor
{
/** <summary> Ciphers an Array of Bytes by using the XOR Algorithm. </summary>

<remarks> Passing an Array of plain Bytes to this Method, will Output the encrypted Bytes; otherwise, the decrypted Bytes. </remarks>

<param name = "inputBytes"> The Bytes to be Ciphered. </param>
<param name = "cipherKey"> The Cipher Key to be Used. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "IndexOutOfRangeException"></exception>
<exception cref = "NullReferenceException"></exception>

<returns> The Ciphered Data. </returns> */

public static byte[] CipherData(ReadOnlySpan<byte> inputBytes, ReadOnlySpan<byte> cipherKey)
{
byte[] cipheredData = new byte[inputBytes.Length];

for(int i = 0; i < inputBytes.Length; i++)
cipheredData[i] = (byte)(inputBytes[i] ^ cipherKey[i % cipherKey.Length] );

return cipheredData;
}

// Get Xor Bytes as Hex String

public static string GetXorString(ReadOnlySpan<byte> inputBytes, ReadOnlySpan<byte> cipherKey)
{
var xorBytes = CipherData(inputBytes, cipherKey);

return InputHelper.ConvertHexString(xorBytes, StringCase.Upper, " ");
}

// JS Fun

public static object CipherDataJS(string arg, string arg2, string arg3)
{

if(!bool.TryParse(arg3, out bool displayAsHex) )
displayAsHex = default;

byte[] data = displayAsHex ? Console.InputEncoding.GetBytes(arg) : InputHelper.ConvertHexBytes(arg);
byte[] key = Console.InputEncoding.GetBytes(arg2);

return displayAsHex ? GetXorString(data, key) : CipherData(data, key);
}

}

}