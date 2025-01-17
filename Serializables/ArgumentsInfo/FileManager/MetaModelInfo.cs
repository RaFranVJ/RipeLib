using System.IO;

namespace RipeLib.Serializables.ArgumentsInfo.FileManager
{
/// <summary> Stores Info that Specifies how to Deal with Metadata Models. </summary>

public class MetaModelInfo : ParamGroupInfo
{
/** <summary> Gets or Sets the Path to the Container that Contains the Files. </summary>

<remarks> Setting this to a Value different from <c>null</c> will make the Program to Resolve the Paths by Name. </remarks>

<returns> The Path to the Metadata Container. </returns> */

public string MetadataContainerPath{ get; set; }

/** <summary> Gets or Sets the Path to the File that Contains the Data. </summary>
<returns> The Path to the Metadata File. </returns> */

public string PathToMetadataFile{ get; set; }

/// <summary> Creates a new Instance of the <c>MetaModelInfo</c>. </summary>

public MetaModelInfo()
{
MetadataContainerPath = GetDefaultPath();
}

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
MetaModelInfo defaultOptions = new();

#region ======== Set default Values to Null Fields ========

MetadataContainerPath ??= defaultOptions.MetadataContainerPath;

#endregion

PathHelper.CheckExistingPath(MetadataContainerPath, true);
}

/** <summary> Gets the default Path to the SMF Info basing on the CurrentAppDirectory. </summary>
<returns> The default Info Path. </returns> */

private static string GetDefaultPath() => Path.Combine(LibInfo.CurrentDllDirectory, "Storage", "Info");
}

}