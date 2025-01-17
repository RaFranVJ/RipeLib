using System;
using System.IO;
using RipeLib.Serializables.ArgumentsInfo.FileSecurity.Integrity;

namespace RipeLib.Modules.Other
{
/// <summary> Initializes Adler32 Digest for Bytes. </summary>

public static class Adler32BytesChecksum
{
/// <summary> The Bits to Shift. </summary>

private const int BITS_TO_SHIFT = 16;

/// <summary> The Number of Bytes per Iteration. </summary>

private const int BYTES_PER_ITERATION = 3800;

/// <summary> The Adler Modular Factor. </summary>

private const uint MOD_ADLER = 65521;

/** <summary> Gets the Checksum of an Array of Bytes by using the Adler32 Algorithm. </summary>

<param name = "inputBytes"> The Bytes where the Checksum will be Obtained from. </param>
<param name = "bytesCount"> The Number of Bytes to be Analized. </param>
<param name = "bytesOffset"> The Offset of the Data stored in the Bytes Array. </param>

<returns> The Checksum of the Bytes. </returns> */

public static uint CalculateChecksum(ReadOnlySpan<byte> inputBytes, int bytesCount, int bytesOffset)
{
uint checksumValue = 1;
uint sumX = checksumValue & 0xFFFF;

uint sumY = checksumValue >> BITS_TO_SHIFT;
int bytesChecked;

while(bytesCount > 0)
{
bytesChecked = (BYTES_PER_ITERATION > bytesCount) ? bytesCount : BYTES_PER_ITERATION;
bytesCount -= bytesChecked;

while(--bytesChecked >= 0)
{
sumX += (uint)inputBytes[bytesOffset++] & 0xFF;
sumY += sumX;
}

sumX %= MOD_ADLER;
sumY %= MOD_ADLER;
}

checksumValue = (sumY << BITS_TO_SHIFT) | sumX;

return checksumValue;
}

/** <summary> Gets the Adler32 Bytes from a Stream. </summary>

<param name = "targetStream"> The Stream which Contains the Adler32 Bytes to be Analized. </param>
<param name = "bytesCount"> The Number of Bytes to be Analized. </param>
<param name = "bytesOffset"> The Offset of the Data stored in the Bytes Array (Default is 0). </param>

<returns> The Adler32 Bytes. </returns> */

public static byte[] GetAdler32Bytes(Stream targetStream, Adler32BytesInfo adler32Cfg = null, 
bool reportProgress = false)
{
MemoryManager.CheckStreamSize(targetStream);

adler32Cfg ??= new();

var progressCfg = ProgressBarTextConfig.CreateNew(reportProgress, "Adler32 Digest", "Generating Checksum", default);
var progressBar = ProgressWindow.CreateNew(progressCfg);

Span<byte> inputBytes = new byte[targetStream.Length];
targetStream.Read(inputBytes);

targetStream.Seek(0, SeekOrigin.Begin);
int bytesToAnalize = (adler32Cfg.NumberOfBytesToAnalize < 0) ? inputBytes.Length : adler32Cfg.NumberOfBytesToAnalize;

uint checksumValue = CalculateChecksum(inputBytes, bytesToAnalize, (int)adler32Cfg.AnalizedBytesOffset);
string hexString = checksumValue.ToString("x2");

int hexStringLength = hexString.Length;
uint expectedLength = adler32Cfg.MaxAdler32Bytes;

if(hexStringLength < expectedLength)
InputHelper.FillString(ref hexString, (int)expectedLength);

else if(hexStringLength > expectedLength)
throw new DataMisalignedException($"Adler32 Bytes Count ({hexStringLength}) can't be Higher than {expectedLength}");

byte[] adler32Bytes = new byte[expectedLength / 2];

for(int i = 0; i < adler32Bytes.Length; i++)
{
string hexPair = hexString.Substring(i * 2, 2);
adler32Bytes[i] = Convert.ToByte(hexPair, 16);

progressBar?.SetProgress(i+1, adler32Bytes.Length, progressCfg.DisplayFormat, progressCfg.Body);
}

progressBar?.Destroy();

return adler32Bytes;
}

// Get Adler32 String

public static string GetAdler32String(Stream targetStream, Adler32BytesInfo adler32Cfg,
bool reportProgress = false)
{
byte[] adler32Bytes = GetAdler32Bytes(targetStream, adler32Cfg, reportProgress);

return InputHelper.ConvertHexString(adler32Bytes, adler32Cfg.StringCaseForChecksums);
}

// JS Fun a

public static object GetAdler32StringJS(string arg, Adler32BytesInfo adler32Cfg)
{
using MemoryStream buffer = new();

buffer.Write(Console.InputEncoding.GetBytes(arg) );

return GetAdler32String(buffer, adler32Cfg);
}

}

}