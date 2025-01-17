using System;
using System.Text;

public static class EncodeHelper
{
// Get ANSI Encoding

public static readonly Encoding ANSI = GetAnsiEncoding();

static int ansiPage = LibInfo.CurrentCulture.TextInfo.ANSICodePage;

private static Encoding GetAnsiEncoding()
{
// First, Check if ANSI is Available at the Culture CodePage

try
{
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

Encoding ansiEncoding = Encoding.GetEncoding(ansiPage);
return ansiEncoding;
}

catch(Exception)
{
}
	
// Then, Check if ANSI is Available at the Windows CodePage

try
{
var encodingsList = Encoding.GetEncodings();

foreach(EncodingInfo singleInfo in encodingsList)
{
Encoding singleEncoding = singleInfo.GetEncoding();

// Check if the Encoding is the Expected (ANSI)

if(singleEncoding.WindowsCodePage == ansiPage && singleEncoding.CodePage != 20127)
return singleEncoding;
}

}

catch(Exception)
{
}

// By default, this Method returns the ASCII Encoding if no Matches are found
	
return Encoding.ASCII;
}
	
/** <summary> Gets the Encoding Type from a Representation String. </summary>

<param name = "sourceString"> The String that Represents the Encoding Type. </param>

<returns> The Encoding Type. </returns> */

public static Encoding GetEncodingType(string sourceString)
{
Encoding encodingType;

switch(sourceString)
{
case "System.Text.ASCIIEncoding+ASCIIEncodingSealed":
encodingType = Encoding.ASCII;
break;

case "System.Text.Latin1Encoding+Latin1EncodingSealed":
encodingType = Encoding.Latin1;
break;

case "System.Text.UnicodeEncoding":
encodingType = Encoding.BigEndianUnicode;
break;

case "System.Text.UTF8Encoding+UTF8EncodingSealed":
encodingType = Encoding.UTF8;
break;

case "System.Text.UTF32Encoding":
goto default;

default:
encodingType = Encoding.Default;
break;
}

return encodingType;
}

// Check if Str is Encoded with ASCII

public static bool IsASCII(string str)
{

for(int i = 0; i < str.Length; i++)
{

if(str[i] > 127)
return false;
        
}

return true;
}


}