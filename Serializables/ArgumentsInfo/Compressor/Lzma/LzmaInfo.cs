using SevenZip;
using System.Collections.Generic;

namespace RipeLib.Serializables.ArgumentsInfo.Compressor.Lzma
{
/// <summary> Groups options related to the LZMA Compressor. </summary>

public class LzmaSettings : ParamGroupInfo
{
/// <summary> The allowed Data Size. </summary>

private static readonly Limit<int> DataSizeRange = new(-1, (int)Constants.ONE_GIGABYTE * 4);

/** <summary> Obtains or Creates a List of IDS used in the Coder Properties. </summary>
<returns> The CoderProps IDS. </returns> */

public CoderPropID[] IDSForCoderProps{ get; set; }

/** <summary> Obtains or Creates a List of Properties for the LZMA Coder. </summary>
<returns> The Coder Properties. </returns> */

public object[] CoderProperties{ get; set; }

/** <summary> Gets o Sets a Boolean that Determines if SizeInfo should be Written on Compressed Files for Decompressing them later. </summary>

<remarks> If set as <b>true</b>, the Properties <b>InputDataSize</b> and <b>OutputDataSize</b> will be Ignored.<para>
</para> Instead, the Current File Size will be used in the Coder Properties. </remarks>

<returns> <b>true</b> if SizeInfo should be Used on Compression; otherwise, <b>false</b>. </returns> */

public bool UseSizeInfo{ get; set; }

/** <summary> Gets or Sets the Endian Order used when Parsing Files. </summary>
<returns> The Endian Order. </returns> */

public Endian? BytesOrderForSizeInfo{ get; set; }

/** <summary> Gets or Sets the Size for Input Files. </summary>
<returns> The Input Size. </returns> */

public long InputDataSize{ get; set; }

/** <summary> Gets or Sets the Size for Output Files. </summary>
<returns> The Output Size. </returns> */

public long OutputDataSize{ get; set; }

/** <summary> Gets or Sets the Amount of Props to Read as Byte for Decompressing the LZMA Files. </summary>
<returns> The Props Count. </returns> */

public int PropsCountForDecompression{ get; set; }

/// <summary> Creates a new Instance of the <c>LzmaInfo</c>. </summary>

public LzmaSettings()
{
UseSizeInfo = true;
BytesOrderForSizeInfo = Endian.Default;

InputDataSize = -1;
OutputDataSize = -1;

PropsCountForDecompression = 5;
}

/// <summary> Checks each nullable Field of the <c>LzmaInfo</c> Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
InputDataSize = DataSizeRange.CheckParamRange( (int)InputDataSize);
OutputDataSize = DataSizeRange.CheckParamRange( (int)OutputDataSize);
}

}

}