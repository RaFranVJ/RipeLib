using System.IO.Compression;
using System;
using RipeLib.Serializables.ArgumentsInfo;

namespace RipeLib.Serializables.ArgumentsInfo.Compressor.Zip
{
/// <summary> Groups options related to the Zip Entries. </summary>

public class ZipEntriesInfo : DataBlockInfo
{
/** <summary> Gets or Sets the Compression Level used when Compressing Data. </summary>
<returns> The Compression Level. </returns> */

public CompressionLevel CompressionLvl{ get; set; }

/** <summary> Gets or Sets the Comment to Write on the ZIP Files. </summary>

<returns> The File Comment. </returns> */

public string OptionalEntryComment{ get; set; }

/** <summary> Gets or Sets the external Attributes that Determines on which OS the Entry was Written. </summary>
<returns> The external Attributes. </returns> */

public int ExternalOSAttributes{ get; set; }

/** <summary> Gets or Sets the Last Write Time of a ZIP Entry. </summary>
<returns> The Last Write Time. </returns> */

public DateTime? LastWriteTime{ get; set; }

/// <summary> Creates a new Instance of the <c>ZipEntriesInfo</c>. </summary>

public ZipEntriesInfo()
{
CompressionLvl = CompressionLevel.Optimal;
}

}

}