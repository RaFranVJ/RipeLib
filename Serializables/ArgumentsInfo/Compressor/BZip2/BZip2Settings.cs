namespace RipeLib.Serializables.ArgumentsInfo.Compressor.BZip2
{
/// <summary> Groups options related to the BZip2 Compressor. </summary>

public class BZip2Settings : DataBlockInfo
{
/// <summary> The allowed Buffer Size. </summary>

private static readonly Limit<int> CompressedBlockSizeRange = new(1, 9);

/** <summary> Gets or Sets the Block Size used on Compresion. </summary>
<returns> The BlockSize For Compression. </returns> */

public int BlockSizeForCompression{ get; set; }

/// <summary> Creates a new Instance of the <c>BZip2Settings</c>. </summary>

public BZip2Settings()
{
BlockSizeForCompression = CompressedBlockSizeRange.MinValue;
}

/// <summary> Checks each nullable Field of the <c>BZip2Info</c> Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
BlockSizeForCompression = CompressedBlockSizeRange.CheckParamRange(BlockSizeForCompression);
BufferSizeForIOTasks = BufferSizeRange.CheckParamRange(BufferSizeForIOTasks);
}

}

}