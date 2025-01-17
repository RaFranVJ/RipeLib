using System.Collections.Generic;

namespace RipeLib.Serializables.ArgumentsInfo.FileManager.Archive
{
/// <summary> Groups Info related to the File Filtering inside Directory. </summary>

public class ArchiveFilterCriteria : ParamGroupInfo
{
/** <summary> Obtains or Creates a List of FileExtensions to search in a Folder.  </summary>
<returns> The Extensions to Search. </returns> */

public List<string> FileExtensionsToSearch{ get; set; }

/** <summary> Gets or Sets the FileNames to search in a Folder. </summary>
<returns> The Names to Search. </returns> */

public List<string> FileNamesToSearch{ get; set; }

/** <summary> Obtains or Creates a List of FileExtensions to Exclude.  </summary>
<returns> The Extensions to Exclude. </returns> */

public List<string> FileExtensionsToExclude{ get; set; }

/** <summary> Gets or Sets the FileNames to Exclude. </summary>
<returns> The Names to Exclude. </returns> */

public List<string> FileNamesToExclude{ get; set; }

/// <summary> Creates a new Instance of the <c>ArchiveFilterCriteria</c>. </summary>

public ArchiveFilterCriteria()
{
FileExtensionsToSearch = new() { ".*" };
FileNamesToSearch = new() { "*" };
}

/** <summary> Creates a new Instance of the <c>ArchiveFilterCriteria</c> with the given Extensions. </summary>
<param name = "extensionsList"> The File Extensions to Search. </param> */

public ArchiveFilterCriteria(params string[] extensions)
{
FileExtensionsToSearch = new(extensions);
}

/** <summary> Creates a new Instance of the <c>ArchiveFilterCriteria</c> with the given Extensions and Names. </summary>

<param name = "extensionsList"> The File Extensions to Search. </param>
<param name = "namesList"> The File Names to Search. </param> */

public ArchiveFilterCriteria(List<string> extensions, List<string> names)
{
FileExtensionsToSearch = extensions;
FileNamesToSearch = names;
}

/** <summary> Checks each nullable Field of the <c>ArchiveFilterCriteria</c> Instance 
and Validates it, in case it's <c>null</c>. </summary> */

public override void CheckForNullFields()
{
ArchiveFilterCriteria defaultFilter = new();

#region ======== Set default Values to Null Fields ========

if(FileNamesToSearch == null)
FileExtensionsToSearch ??= defaultFilter.FileExtensionsToSearch;

else if(FileExtensionsToSearch == null)
FileNamesToSearch ??= defaultFilter.FileNamesToSearch;

#endregion
}

}

}