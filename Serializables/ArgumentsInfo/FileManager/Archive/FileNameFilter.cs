namespace RipeLib.Serializables.ArgumentsInfo.FileManager.Archive
{
/// <summary> Stores Info related to the FileName Filter. </summary>

public class FileNameFilter : ParamGroupInfo
{
/** <summary> Gets or Sets the Criteria used for Filtering Files inside a Directory. </summary
<returns> The FilterCriteria for Files. </returns> */

public ArchiveFilterCriteria FilterCriteriaForFiles{ get; set; }

/// <summary> Creates a new Instance of the <c>FileNameFilter</c>. </summary>

public FileNameFilter()
{
FilterCriteriaForFiles = new();
}

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
FileNameFilter defaultInfo = new();

#region ======== Set default Values to Null Fields ========

FilterCriteriaForFiles ??= defaultInfo.FilterCriteriaForFiles;

#endregion
}

}

}