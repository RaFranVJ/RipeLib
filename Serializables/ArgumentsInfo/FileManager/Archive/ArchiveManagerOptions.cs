namespace RipeLib.Serializables.ArgumentsInfo.FileManager.Archive
{
/// <summary> Stores Info related to Archives inside the File Manager. </summary>

public class ArchiveManagerOptions : ParamGroupInfo
{
/** <summary> Gets or Sets some Configs that Specific how to Handle File Properties. </summary>
<returns> The File Props Config. </returns> */

public FileSystemPropsInfo FilePropsConfig{ get; set; }

/** <summary> Gets or Sets some Configs that Specific how new Files should be Created. </summary>
<returns> The New Files Config. </returns> */

public FileCreatorParams NewFilesConfig{ get; set; }

/** <summary> Gets or Sets a boolean that Determines if existing Files should be Replaced or not. </summary>
<returns> <b>true</b> if Files should be Replaced with new Ones; otherwise <b>false</b>. </returns> */

public bool ReplaceExistingFiles{ get; set; }

/// <summary> Creates a new Instance of the <c>ArchiveManagerOptions</c>. </summary>

public ArchiveManagerOptions()
{
FilePropsConfig = new();
NewFilesConfig = new();
}

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
ArchiveManagerOptions defaultOptions = new();

#region ======== Set default Values to Null Fields ========

FilePropsConfig ??= defaultOptions.FilePropsConfig;
NewFilesConfig ??= defaultOptions.NewFilesConfig;

NewFilesConfig.CheckForNullFields();

#endregion
}

}

}