using System.IO;
using RipeLib.Serializables.ArgumentsInfo.FileManager.Metadata;

namespace RipeLib.Serializables.ArgumentsInfo.FileManager
{
/// <summary> Stores some Params used for Loading/Saving Metadata. </summary>

public class MetadataHandlerInfo : ParamGroupInfo
{
/** <summary> Gets or Sets the Params used for Loading Data. </summary>
<returns> The Import Params. </returns> */

public MetadataImportParams ImportParams{ get; set; }

/** <summary> Gets or Sets the Params used for Saving Data. </summary>
<returns> The Export Params. </returns> */

public MetadataExportParams ExportParams{ get; set; }

/// <summary> Creates a new Instance of the <c>MetaModelInfo</c>. </summary>

public MetadataHandlerInfo()
{
ImportParams = new();
ExportParams = new();
}

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
MetadataHandlerInfo defaultOptions = new();

#region ======== Set default Values to Null Fields ========

ImportParams ??= defaultOptions.ImportParams;
ExportParams ??= defaultOptions.ExportParams;

#endregion

ImportParams.CheckForNullFields();
ExportParams.CheckForNullFields();
}

}

}