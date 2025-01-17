using System.IO;

namespace RipeLib.Serializables.ArgumentsInfo.FileManager
{
/// <summary> Stores Info that Specifies how FileSystems should be Searched. </summary>

public class FileSystemSearchParams : ParamGroupInfo
{
/** <summary> Gets or Sets the Pattern used for Searching Files inside a Directory. </summary>
<returns> The Search Pattern. </returns> */

public string SearchPattern{ get; set; }

/** <summary> Gets or Sets some Options used for Searching FileSystems inside a Directory. </summary>
<returns> The Search Pattern. </returns> */

public EnumerationOptions EnumOptions{ get; set; }

/// <summary> Creates a new Instance of the <c>FileSystemSearchParams</c>. </summary>

public FileSystemSearchParams()
{
SearchPattern = "*.*";
EnumOptions = GetDefaultOptions(true);
}

/// <summary> Creates a new Instance of the <c>FileSystemSearchParams</c>. </summary>

public FileSystemSearchParams(string searchPattern)
{
SearchPattern = searchPattern;
EnumOptions = GetDefaultOptions(true);
}

/// <summary> Creates a new Instance of the <c>FileSystemSearchParams</c>. </summary>

public FileSystemSearchParams(bool isForFiles)
{
SearchPattern = isForFiles ? "*.*" : "*";
EnumOptions = GetDefaultOptions(isForFiles);
}

private static EnumerationOptions GetDefaultOptions(bool isForFiles)
{
EnumerationOptions enumOptions = new();

if(isForFiles)
{
enumOptions.RecurseSubdirectories = false;
enumOptions.ReturnSpecialDirectories = false;
}

else
{
enumOptions.RecurseSubdirectories = true;
enumOptions.ReturnSpecialDirectories = false;
}

enumOptions.AttributesToSkip = FileAttributes.Hidden | FileAttributes.System;
enumOptions.IgnoreInaccessible = true;

return enumOptions;
}

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
FileSystemSearchParams defaultInfo = new();

#region ======== Set default Values to Null Fields ========

SearchPattern ??= defaultInfo.SearchPattern;
EnumOptions ??= defaultInfo.EnumOptions;

#endregion
}

}

}