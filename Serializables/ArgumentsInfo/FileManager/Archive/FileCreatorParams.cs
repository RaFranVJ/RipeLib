using System.IO;

namespace RipeLib.Serializables.ArgumentsInfo.FileManager.Archive
{
/// <summary> Stores Info related to new Files on Creation. </summary>

public class FileCreatorParams : ParamGroupInfo
{
/// <summary> The allowed Buffer Size. </summary>

protected Limit<int> BufferSizeRange{ get; set; } = new(0, (int)Constants.ONE_KILOBYTE);

/** <summary> Gets or Sets the new Name of a File. </summary>
<returns> The new Name of the File. </returns> */

public string NewFileName{ get; set; }

/** <summary> Gets or Sets a Value that Determines the Buffer Size a Created File should Occupy. </summary>
<returns> The Buffer Size of the new Files. </returns> */

public int BufferSizeOnFileCreation{ get; set; }

/** <summary> Gets or Sets a Value that Determines how Files should be Created. </summary>
<returns> The File Creation Options. </returns> */

public FileOptions FileCreationOptions{ get; set; }

/// <summary> Creates a new Instance of the <c>FileCreatorParams</c>. </summary>

public FileCreatorParams()
{
NewFileName = "(New File)";
BufferSizeOnFileCreation = BufferSizeRange.MinValue;

FileCreationOptions = FileOptions.None;
}

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
FileCreatorParams defaultOptions = new();

#region ======== Set default Values to Null Fields ========

NewFileName ??= defaultOptions.NewFileName;

#endregion

BufferSizeOnFileCreation = BufferSizeRange.CheckParamRange(BufferSizeOnFileCreation);
}

}

}