using ICSharpCode.SharpZipLib.Tar;

namespace RipeLib.Serializables.ArgumentsInfo.Compressor.Tar
{
/// <summary> Groups options related to the Tar Compressor. </summary>

public class TarSettings : DataBlockInfo
{
/** <summary> Gets or Sets the Block Factor used on Compression. </summary>
<returns> The Block Factor. </returns> */

public int BlockFactorForCompression{ get; set; }

/// <summary> Creates a new Instance of the <c>TarInfo</c>. </summary>

public TarSettings()
{
BlockFactorForCompression = TarBuffer.DefaultBlockFactor;
}

}

}