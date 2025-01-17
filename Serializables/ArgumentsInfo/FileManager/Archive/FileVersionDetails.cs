namespace RipeLib.Serializables.ArgumentsInfo.FileManager.Archive
{
/// <summary> Stores Info related to the Version of external Files Handled by the Tool. </summary>

public class FileVersionDetails<T> : ParamGroupInfo where T : struct
{
/** <summary> Gets or Sets the Version Number for Specific Types of Files. </summary>
<returns> The Version Number. </returns> */

public T VersionNumber{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if only Compatible Versions should be Used. </summary>
<returns> <b>true</b> if Compatibility should be Adapted; otherwise, <b>false</b>. </returns> */

public bool AdaptCompatibilityBetweenVersions{ get; set; }

/// <summary> Creates a new Instance of the <c>FileVersionDetails</c>. </summary>

public FileVersionDetails()
{
AdaptCompatibilityBetweenVersions = true;
}

/// <summary> Creates a new Instance of the <c>FileVersionDetails</c> with the given Params. </summary>

public FileVersionDetails(T version, bool adaptVer = true)
{
VersionNumber = version;
AdaptCompatibilityBetweenVersions = adaptVer;
}

}

}