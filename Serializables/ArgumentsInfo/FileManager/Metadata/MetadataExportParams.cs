namespace RipeLib.Serializables.ArgumentsInfo.FileManager.Metadata
{
/// <summary> Groups options related to Exportation of Metadata inside Files. </summary>

public class MetadataExportParams : MetaModelInfo
{
/** <summary> Gets or Sets a Boolean that Determines if SMF Tags should be Generated on Compression. </summary>
<returns> <b>true</b> if Metadata should be Exported from Files; otherwise, <b>false</b>. </returns> */

public bool ExportMetadataFromFiles{ get; set; }

/// <summary> Creates a new Instance of the <c>MetadataExportParams</c>. </summary>

public MetadataExportParams()
{
ExportMetadataFromFiles = true;
}

}

}