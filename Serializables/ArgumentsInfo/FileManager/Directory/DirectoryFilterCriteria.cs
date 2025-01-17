using System.Collections.Generic;

namespace RipeLib.Serializables.ArgumentsInfo.FileManager.Directory
{
/// <summary> Groups Info related to the Sub-Folder Filtering inside Directory. </summary>

public class DirectoryFilterCriteria : ParamGroupInfo
{
/** <summary> Gets or Sets the Sub-Folder Names to search in a Directory. </summary>
<returns> The Names to Search. </returns> */

public List<string> DirNamesToSearch{ get; set; }

/** <summary> Gets or Sets the Sub-Folder Names to Exclude. </summary>
<returns> The Names to Exclude. </returns> */

public List<string> DirNamesToExclude{ get; set; }

/** <summary> Gets or Sets a Value that Determines which Sub-Folders should be Filtered according to their Content. </summary>
<returns> The Matching Content Length. </returns> */

public int MatchingContentLength{ get; set; }

/// <summary> Creates a new Instance of the <c>DirectoryFilterCriteria</c>. </summary>

public DirectoryFilterCriteria()
{
DirNamesToSearch = new() { "*" };
MatchingContentLength = -1;
}

/// <summary> Checks each nullable Field of the <c>SubFolderFilterCriteria</c> Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
DirectoryFilterCriteria defaultFilter = new();

#region ======== Set default Values to Null Fields ========

DirNamesToSearch ??= defaultFilter.DirNamesToSearch;

#endregion
}

}

}