namespace RipeLib.Serializables.ArgumentsInfo.FileManager.Metadata
{
/// <summary> Groups options related to Importation of Metadata to Files. </summary>

public class MetadataImportParams : MetaModelInfo
{
/** <summary> Gets or Sets a Boolean that Determines if Metadata should be Imported to Files. </summary>
<returns> <b>true</b> if Metadata should be Imported to Files; otherwise, <b>false</b>. </returns> */

public bool ImportMetadataToFiles{ get; set; }

/// <summary> Creates a new Instance of the <c>MetadataImportParams</c>. </summary>

public MetadataImportParams()
{
ImportMetadataToFiles = false;
}

}

}