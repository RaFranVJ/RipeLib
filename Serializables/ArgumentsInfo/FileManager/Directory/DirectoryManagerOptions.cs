using RipeLib.Serializables.ArgumentsInfo.FileManager.Archive;

namespace RipeLib.Serializables.ArgumentsInfo.FileManager.Directory
{
/// <summary> Stores Info related to Directories inside the File Manager. </summary>

public class DirectoryManagerOptions : FileNameFilter
{
/** <summary> Gets or Sets some Configs that Specific how to Handle Folder Properties. </summary>
<returns> The Config for Hanling Folder Properties. </returns> */

public FileSystemPropsInfo DirPropsConfig{ get; set; }

/** <summary> Gets or Sets the new Name of a Folder. </summary>
<returns> The new Name of the Folder. </returns> */

public string NewDirName{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if the Content inside a Directory should be Deleted or not. </summary>

<remarks> You can Set this Field to <b>null</b>.
The System will Automatically determine when to use Recursive Deletion and when to not. </remarks>

<returns> <b>true</b> if all the Content inside a Folder should be Deleted
(including Files and Sub-Folders); otherwise, <b>false</b>. </returns> */

public bool? RecursiveFolderDeletion{ get; set; }

/** <summary> Gets or Sets some Options that Specifie how to Search Files. </summary>
<returns> The Options for Searching Files. </returns> */

public FileSystemSearchParams SearchOptionsForFiles{ get; set; }

/** <summary> Gets or Sets some Options that Specifie how to Search Sub-Folders. </summary>
<returns> The Options for Searching Sub-Folders. </returns> */

public FileSystemSearchParams SearchOptionsForSubFolders{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if all the Files inside a Directory should be Processed or not. </summary>

<remarks> If set to <b>false</b>, the User must Choose a Specific File from the selected Folder. </remarks>

<returns> <b>true</b> if all Files should be Processed at once; otherwise, <b>false</b>. </returns> */

public bool ProcessAllFilesAtOnce{ get; set; }

/** <summary> Gets or Sets some Configs that Specific how to Handle Sub-Folder Properties. </summary>
<returns> The Sub-Folders Props Config. </returns> */

public FileSystemPropsInfo SubFolderPropsConfig{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if the Sub-Folders inside a Directory should be Filtered or not. </summary>
<returns> <b>true</b> if Sub-Folders should be Filtered; otherwise, <b>false</b>. </returns> */

public bool FilterSubFoldersInsideDirs{ get; set; }

/** <summary> Gets or Sets the Criteria used for Filtering Sub-Folders inside a Directory. </summary
<returns> The FilterCriteria for Sub-Folders. </returns> */

public DirectoryFilterCriteria FilterCriteriaForSubFolders{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if the Files inside a Directory should be Filtered or not. </summary>
<returns> <b>true<b/> if Files should be Filtered; otherwise, <b>false</b>. </returns> */

public bool FilterFilesInsideDirs{ get; set; }

/// <summary> Creates a new Instance of the <c>DirectoryManagerOptions</c>. </summary>

public DirectoryManagerOptions()
{
DirPropsConfig = new();
NewDirName = "(New Folder)";

RecursiveFolderDeletion = null;
SearchOptionsForFiles = new(true);

SearchOptionsForSubFolders = new(false);
ProcessAllFilesAtOnce = true;

SubFolderPropsConfig = new();
FilterSubFoldersInsideDirs = false;

FilterCriteriaForSubFolders = new();
FilterFilesInsideDirs = true;
}

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
DirectoryManagerOptions defaultOptions = new();

#region ======== Set default Values to Null Fields ========

DirPropsConfig ??= defaultOptions.DirPropsConfig;
NewDirName ??= defaultOptions.NewDirName;
SearchOptionsForFiles ??= defaultOptions.SearchOptionsForFiles;
SearchOptionsForSubFolders ??= defaultOptions.SearchOptionsForSubFolders;
SubFolderPropsConfig ??= defaultOptions.SubFolderPropsConfig;
FilterCriteriaForSubFolders ??= defaultOptions.FilterCriteriaForSubFolders;
FilterCriteriaForFiles ??= defaultOptions.FilterCriteriaForFiles;

#endregion

DirPropsConfig.CheckForNullFields();
SearchOptionsForFiles.CheckForNullFields();
SearchOptionsForSubFolders.CheckForNullFields();
SubFolderPropsConfig.CheckForNullFields();
FilterCriteriaForSubFolders.CheckForNullFields();
FilterCriteriaForFiles.CheckForNullFields();
}

}

}