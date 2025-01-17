using System.IO;

namespace RipeLib.Serializables.ArgumentsInfo.FileManager
{
/// <summary> Stores Info related to how Properties should be Handled after retreived from a FileSystem. </summary>

public class FileSystemPropsInfo : ParamGroupInfo
{
/** <summary> Gets or Sets a boolean that Determines if existing File Properties should be Saved after being Displayed. </summary>
<returns> <b>true</b> if File Properties should be Saved after being Printed on Screen; otherwise <b>false</b>. </returns> */

public bool SavePropsAfterDisplayingThem{ get; set; }

/** <summary> Gets or Sets the Path where Properties should be Saved. </summary>
<returns> The Path where Props should be Saved. </returns> */

public string PathForProperties{ get; set; }

/// <summary> Creates a new Instance of the <c>FileSystemPropsInfo</c>. </summary>

public FileSystemPropsInfo()
{
PathForProperties = GetDefaultPathForProps();
}

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
FileSystemPropsInfo defaultOptions = new();

#region ======== Set default Values to Null Fields ========

PathForProperties ??= defaultOptions.PathForProperties;

#endregion

PathHelper.CheckExistingPath(PathForProperties, SavePropsAfterDisplayingThem);
}

/** <summary> Gets the default Path to the Props basing on the CurrentAppDirectory. </summary>
<returns> The default Info Path. </returns> */

private static string GetDefaultPathForProps() => Path.Combine(LibInfo.CurrentDllDirectory, "Storage", "Data", "FsProps");
}

}