using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RipeLib.Modules.Other
{
/// <summary> Initializes Base64 Parse Tasks for Strings. </summary>

public static class Base64StringParser
{
// Check if String is Base64

private static bool IsBase64String(ReadOnlySpan<char> targetStr, bool webSafeCheck)
{

if(targetStr.TrimEnd('=').Length % 4 != 0)
return false;

string pattern = webSafeCheck ? @"^[A-Za-z0-9\-_]*={0,2}$" : @"^[A-Za-z0-9+/]*={0,2}$";
Regex base64Regex = new(pattern);

return base64Regex.IsMatch(targetStr);
}
	
// Convert Base64 String to Web Safe (Internal)

private static string ToWebSafe(string targetStr, bool skipValidation)
{
bool isStandarBase64 = skipValidation || IsBase64String(targetStr, false);
bool isSafeBase64 = !skipValidation && IsBase64String(targetStr, true);

if(!isStandarBase64)
throw new ArgumentException("String is not Base64");

else if(isSafeBase64)
return targetStr;

StringBuilder safeBuilder = new(targetStr);

while(safeBuilder.Length > 0 && safeBuilder[^1] == '=')
safeBuilder.Length--; // TrimEnd

safeBuilder.Replace('+', '-').Replace('/', '_'); 	

return safeBuilder.ToString();
}

// Convert Base64 String to Web Safe

public static string ToWebSafe(string targetStr) => ToWebSafe(targetStr, false);

/** <summary> Encodes an Array of Bytes as a Base64 String. </summary>

<param name = "inputBytes"> The Bytes to be Encoded. </param>
<param name = "isWebSafe"> A boolean that Determines if the Base64 string will be Generated as a Web Safe string or not. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FormatException"></exception>
<exception cref = "OutOfMemoryException"></exception>

<returns> The Base64 String. </returns> */

public static string EncodeBytes(ReadOnlySpan<byte> inputBytes, bool isWebSafe)
{
string encodedString = Convert.ToBase64String(inputBytes);

return isWebSafe ? ToWebSafe(encodedString, true) : encodedString;
}

// Method for JS

public static object EncodeBytesJS(string arg, string arg2)
{
ReadOnlySpan<byte> data = Console.InputEncoding.GetBytes(arg);

if(!bool.TryParse(arg2, out bool isWebSafe) )
isWebSafe = default;

return EncodeBytes(data, isWebSafe);
}

// Convert Base64 String to Standar (Internal)

private static string ToStandar(string targetStr, bool skipValidation)
{
bool isSafeBase64 = skipValidation || IsBase64String(targetStr, true);	
bool isStandarBase64 = !skipValidation && IsBase64String(targetStr, false);

if(!isSafeBase64)
throw new ArgumentException("String is not Web-Safe Base64");

else if(isStandarBase64)
return targetStr;

StringBuilder standardBuilder = new(targetStr);
standardBuilder.Replace('_', '/').Replace('-', '+');

int paddingRequired = 4 - (standardBuilder.Length % 4);

if(paddingRequired < 4)
standardBuilder.Append( new string('=', paddingRequired) );

return standardBuilder.ToString();
}

// Convert Base64 String to Web Safe

public static string ToStandar(string targetStr) => ToStandar(targetStr, false);

/** <summary> Decodes a Base64 String as an Array of Bytes. </summary>

<param name = "inputString"> The String to be Decoded. </param>
<param name = "isWebSafe"> A boolean that Determines if the Base64 string was Generated as a Web Safe string or not. </param>

<exception cref = "ArgumentException"></exception>
<exception cref = "ArgumentNullException"></exception>
<exception cref = "FormatException"></exception>
<exception cref = "OutOfMemoryException"></exception>

<returns> The Bytes Decoded. </returns> */

public static byte[] DecodeString(string inputString, bool isWebSafe)
{
inputString = isWebSafe ? ToStandar(inputString, true) : inputString;

return Convert.FromBase64String(inputString);
}

// Method for JS

public static object DecodeStringJS(string arg, string arg2)
{

if(!bool.TryParse(arg2, out bool isWebSafe) )
isWebSafe = default;

return DecodeString(arg, isWebSafe);
}

}

}