namespace RipeLib.Serializables.ArgumentsInfo.FileSecurity.Integrity
{
/// <summary> Groups options related to the Adler32 Algorithm. </summary>

public class Adler32BytesInfo : ParamGroupInfo
{
/** <summary> Gets or Sets the Maximum number of Adler32 Bytes. </summary>
<returns> The Block Factor. </returns> */

public uint MaxAdler32Bytes{ get; set; }

/** <summary> Gets or Sets the Number of Hex Bytes to Use when Parsing the File Size. </summary>
<returns> The Number of Bytes to Analize. </returns> */

public int NumberOfBytesToAnalize{ get; set; }

/** <summary> Gets or Sets the Offset where to Start Analizing the Adler32 Bytes. </summary>
<returns> The Bytes Offset. </returns> */

public uint AnalizedBytesOffset{ get; set; }

/** <summary> Gets or Sets the String Case used when Generating Checksum. </summary>
<returns> The Adler32 String Case. </returns> */

public StringCase StringCaseForChecksums{ get; set; }

/// <summary> Creates a new Instance of the <c>Adler32Info</c>. </summary>

public Adler32BytesInfo()
{
MaxAdler32Bytes = 8;
NumberOfBytesToAnalize = -1;
}

}

}