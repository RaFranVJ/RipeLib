using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary> Initializes Filtering Functions for Input Values. </summary>

public static partial class InputHelper
{
/// <summary> Searches for a Number on a String. </summary>

[ GeneratedRegex("([-+]?\\d*\\.?\\d+)") ]
private static partial Regex NumericPattern();

// Upper or Lower

public static void ApplyStringCase(ref string targetStr, StringCase strCase)
{

switch(strCase)
{
case StringCase.Lower:
targetStr = targetStr.ToLower();
break;

case StringCase.Upper:
targetStr = targetStr.ToUpper();
break;
}

}

public static byte[] ConvertHexBytes(string hexString, string separator = " ")
{
string[] hexValues = hexString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
byte[] hexBytes = new byte[hexValues.Length];

for(int i = 0; i < hexValues.Length; i++)
hexBytes[i] = Convert.ToByte(hexValues[i], 16);

return hexBytes;
}

/** <summary> Converts an Array of Bytes into a Hexadecimal String. </summary>

<param name = "inputBytes"> The Bytes to Convert. </param>

<returns> The HexString Converted. </returns> */

public static string ConvertHexString(byte[] inputBytes, StringCase strCase, string separator = "")
{
const string defaultSeparator = "-";

string hexString = (separator == defaultSeparator) ? BitConverter.ToString(inputBytes) :
BitConverter.ToString(inputBytes).Replace(defaultSeparator, separator);

ApplyStringCase(ref hexString, strCase);

return hexString;
}

/** <summary> Filters a <c>DateTime</c> from user's Input. </summary>

<param name = "userInput"> The user's Input to be Filtered. </param>

<returns> The Filtered Value. </returns> */

public static DateTime FilterDateTime(string userInput)
{
bool isValidDate = DateTime.TryParse(userInput, LibInfo.CurrentCulture, out DateTime filteredDate);

return isValidDate ? filteredDate : DateTime.Now;
}

/** <summary> Filters a Name from User's Input. </summary>

<param name = "sourceName"> The Name to be Filtered. </param>

<returns> The Filtered Name. </returns> */

public static string FilterName(string sourceName)
{
string validStr = sourceName;
char[] invalidChars = GetInvalidChars(true);

int invalidCharsCount = invalidChars.Length;
string filteredName = string.Empty;

for(int i = 0; i < invalidChars.Length; i++)
{
char invalidSymbol = invalidChars[i];
bool hasInvalidChar = sourceName.Contains(invalidSymbol);

if(hasInvalidChar)
{
filteredName = validStr.Replace(invalidSymbol.ToString(), string.Empty);
validStr = filteredName;
}

filteredName = validStr;
}

return filteredName;
}

/** <summary> Extracts the Numeric Digits from user's Input. </summary>

<param name = "sourceStr"> The String to be Analized. </param>

<returns> A Sequence of Chars that represent the numerical Digits. </returns> */

private static string ExtractNumericDigits(string sourceStr)
{
Match numericMatch = NumericPattern().Match(sourceStr);

if(numericMatch.Success)
{
Group numbersGroup = numericMatch.Groups[1];

return numbersGroup.Value;
}

return "0";
}

/** <summary> Filters a numeric Value from user's Input. </summary>

<param name = "userInput"> The user's Input to be Filtered. </param>

<returns> The Filtered Value. </returns> */

public static T FilterNumber<T>(string userInput) where T : struct
{
return ValidateNumericRange<T>( ExtractNumericDigits(userInput) );
}

/** <summary> Generates a Random Value that serves as a String Complement. </summary>
<returns> The String Complement. </returns> */

public static char GenerateStringComplement()
{
char[] fillingChars = { '°', '¬', '(', ')', '¿', '¡', '¨', '~', '[', ']', '^', '`', ',', ';', '.', '-', '_' };

Random randomizer = new();
int randomIndex = randomizer.Next(fillingChars.Length - 1);

return fillingChars[randomIndex];
}

/** <summary> Gets the Display Size of a Meassure expressed in Bits, Bytes, Kilobytes, Megabytes and Gigabytes.  </summary>

<param name = "sourceAmount"> The Amount to be Displayed. </param>

<returns> The Display Size of the Meassure. </returns> */

public static string GetDisplaySize(long sourceAmount)
{
double sizeFactor;
string meassureSymbol;

if(sourceAmount >= Constants.ONE_GIGABYTE)
{
sizeFactor = sourceAmount / Constants.ONE_GIGABYTE;
meassureSymbol = "GB";
}

else if(sourceAmount >= Constants.ONE_MEGABYTE)
{
sizeFactor = sourceAmount / Constants.ONE_MEGABYTE;
meassureSymbol = "MB";
}

else if(sourceAmount >= Constants.ONE_KILOBYTE)
{
sizeFactor = sourceAmount / Constants.ONE_KILOBYTE;
meassureSymbol = "KB";
}

else
{
sizeFactor = sourceAmount;
meassureSymbol = "B";
}

string sizeValue = sizeFactor.ToString("n0", LibInfo.CurrentCulture);

return $"{sizeValue} {meassureSymbol}";
}

/** <summary> Gets a List of Invalid Chars for FileNames or DirNames. </summary>

<param name = "isShortName"> Determines if the File/Folder name is a Name (Short Name) or a Path (Full Name). </param>

<returns> The Invalid Chars. </returns> */

public static char[] GetInvalidChars(bool isShortName)
{
return isShortName ? Path.GetInvalidFileNameChars() : Path.GetInvalidPathChars();
}

/** <summary> Fills an Array in order to Reach the specified Length. </summary>

<param name = "sourceArray"> The Array to be Filled. </param>
<param name = "expectedLength"> The Length expected. </param>

<returns> The Array Filled. </returns> */

public static void FillArray<T>(ref T[] sourceArray, int expectedLength)
{
expectedLength = (expectedLength < 0) ? 1 : expectedLength;
sourceArray ??= new T[expectedLength];

int currentLength = sourceArray.Length;

if(currentLength == expectedLength)
return;

T[] paddedArray = new T[expectedLength];

if(currentLength < expectedLength)
sourceArray.CopyTo(paddedArray, 0);

else
Array.Copy(sourceArray, paddedArray, expectedLength);

sourceArray = paddedArray;
}

/** <summary> Fills a String in order to Reach the specified Length. </summary>

<param name = "sourceStr"> The String to be Filled. </param>
<param name = "expectedLength"> The Length expected. </param>

<returns> The String Filled. </returns> */

public static void FillString(ref string sourceStr, int expectedLength)
{
expectedLength = (expectedLength < 0) ? 1 : expectedLength;
sourceStr ??= string.Empty;

int currentLength = sourceStr.Length;

if(currentLength == expectedLength)
return;

if(expectedLength < currentLength)
throw new ArgumentException("Padding Value can't be Less than String Length");

sourceStr = sourceStr.PadLeft(expectedLength, '0');
}

/** <summary> Merges two Arrays as a Single one. </summary>

<param name="arrayX"> The First Array to be Merged. </param>
<param name="arrayY"> The Second Array to be Merged. </param>

<returns> The Arrays Merged. </returns> */

public static T[] MergeArrays<T>(T[] arrayX, T[] arrayY)
{
T[] mergedArray;

if( (arrayX == null || arrayX.Length == 0) && (arrayY == null || arrayY.Length == 0) )
mergedArray = new T[0];

else if(arrayX == null || arrayX.Length == 0)
mergedArray = arrayY;

else if(arrayY == null || arrayY.Length == 0)
mergedArray = arrayX;

else
mergedArray = arrayX.Concat(arrayY).ToArray();

return mergedArray;
}

/** <summary> Merges three Arrays as a Single one. </summary>

<param name="arrayX"> The First Array to be Merged. </param>
<param name="arrayY"> The Second Array to be Merged. </param>
<param name="arrayZ"> The Third Array to be Merged. </param>

<returns> The Arrays Merged. </returns> */

public static T[] MergeArrays<T>(T[] arrayX, T[] arrayY, T[] arrayZ)
{
T[] arrayXY = MergeArrays(arrayX, arrayY);
T[] mergedArray;

if( (arrayXY == null || arrayXY.Length == 0) && (arrayZ == null || arrayZ.Length == 0) )
mergedArray = new T[0];

else if(arrayXY == null || arrayXY.Length == 0)
mergedArray = arrayZ;

else if(arrayZ == null || arrayZ.Length == 0)
mergedArray = arrayXY;

else
mergedArray = MergeArrays(arrayXY, arrayZ);

return mergedArray;
}

/** <summary> Merges four Arrays as a Single one. </summary>

<param name="arrayW"> The First Array to be Merged. </param>
<param name="arrayX"> The Second Array to be Merged. </param>
<param name="arrayY"> The Third Array to be Merged. </param>
<param name="arrayZ"> The Fourth Array to be Merged. </param>

<returns> The Arrays Merged. </returns> */

public static T[] MergeArrays<T>(T[] arrayW, T[] arrayX, T[] arrayY, T[] arrayZ)
{
T[] arrayWX = MergeArrays(arrayW, arrayX);
T[] arrayYZ = MergeArrays(arrayY, arrayZ);

T[] mergedArray;

if( (arrayWX == null || arrayWX.Length == 0) && (arrayYZ == null || arrayYZ.Length == 0) )
mergedArray = new T[0];

else if(arrayWX == null || arrayWX.Length == 0)
mergedArray = arrayYZ;

else if(arrayYZ == null || arrayYZ.Length == 0)
mergedArray = arrayWX;

else
mergedArray = MergeArrays(arrayWX, arrayYZ);

return mergedArray;
}

// Remove Literal Sequence of Chars on String

public static void RemoveLiteralChars(ref string targetStr)
{
targetStr = targetStr.Replace("\\r\\n", string.Empty)
.Replace("\\r", string.Empty)
.Replace("\\n", string.Empty)
.Replace("\\t", string.Empty);
}

/** <summary> Checks if a sequence of Numbers represented as a String is on Range or not. </summary>

<param name = "numericDigits"> The numeric Digits Sequence. </param>

<returns> A Value that is Inside the Range of the expected Type. </returns> */

private static T ValidateNumericRange<T>(string numericDigits) where T : struct
{
object parsedObj = Convert.ChangeType(numericDigits, typeof(T) );

T numericValue = (parsedObj == null) ? default : (T)parsedObj;

return numericValue;
}

}