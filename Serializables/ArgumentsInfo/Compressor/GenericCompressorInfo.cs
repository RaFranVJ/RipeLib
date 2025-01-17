using System.IO.Compression;

namespace RipeLib.Serializables.ArgumentsInfo.Compressor
{
/// <summary> Groups Info related to common Params used for Compressing Data. </summary>

public class GenericCompressorInfo : DataBlockInfo
{
/** <summary> Gets or Sets the Compression Level used when Compressing Data. </summary>
<returns> The Compression Level. </returns> */

public CompressionLevel CompressionLvl{ get; set; }

/// <summary> Creates a new Instance of the <c>GenericCompressorInfo</c>. </summary>

public GenericCompressorInfo()
{
BufferSizeForIOTasks = BufferSizeRange.MinValue * 2;

CompressionLvl = CompressionLevel.Optimal;
}

}

}