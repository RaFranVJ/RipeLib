using K4os.Compression.LZ4.Streams;

namespace RipeLib.Serializables.ArgumentsInfo.Compressor.LZ4
{
/// <summary> Groups options related to the LZ4 Compressor. </summary>

public class LZ4Settings : DataBlockInfo
{
/** <summary> Gets or Sets some Options for Encoding LZ4 Files. </summary>
<returns> The Encoder Info. </returns> */

public LZ4EncoderSettings EncoderSettings{ get; set; }

/** <summary> Gets or Sets some Options for Decoding LZ4 Files. </summary>
<returns> The Decoder Info. </returns> */

public LZ4DecoderSettings DecoderSettings{ get; set; }

/// <summary> Creates a new Instance of the <c>LZ4Info</c>. </summary>

public LZ4Settings()
{
EncoderSettings = new();
DecoderSettings = new();
}

/// <summary> Checks each nullable Field of the <c>LZ4Info</c> Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
LZ4Settings defaultInfo = new();

#region ======== Set default Values to Null Fields ========

EncoderSettings ??= defaultInfo.EncoderSettings;
DecoderSettings ??= defaultInfo.DecoderSettings;

#endregion

BufferSizeForIOTasks = BufferSizeRange.CheckParamRange(BufferSizeForIOTasks);
}

}

}