using System.IO;

namespace ZCore.Serializables.Arguments
{
/// <summary> Groups a Set of generic Arguments that are mostly Used on Function Calls. </summary>

public class ArgumentsSet : FieldUtil
{
/** <summary> Gets or Sets the Input Path entered by User. </summary>
<returns> The Input Path. </returns> */

public string InputPath{ get; set; }

/** <summary> Gets or Sets the Output Path entered by User. </summary>
<returns> The Output Path. </returns> */

public string OutputPath{ get; set; }

/// <summary> Creates a new Instance of the <c>ArgumentSet</c>. </summary>

public ArgumentsSet()
{
InputPath = GetDefaultInputPath();
OutputPath = GetDefaultOutputPath();
}

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
ArgumentsSet defaultArgs = new();

#region ======== Set default Values to Null Fields ========

InputPath ??= defaultArgs.InputPath;
OutputPath ??= defaultArgs.OutputPath;

#endregion

PathHelper.CheckExistingPath(InputPath, true);
PathHelper.CheckExistingPath(OutputPath, false);
}

/** <summary> Gets the default Input Path basing on the CurrentAppDirectory. </summary>
<returns> The default Input Path. </returns> */

protected virtual string GetDefaultInputPath() => Path.Combine(LibInfo.CurrentDllDirectory, "Storage", "Input.txt");

/** <summary> Gets the default Output Path basing on the CurrentAppDirectory. </summary>
<returns> The default Output Path. </returns> */

protected virtual string GetDefaultOutputPath() => Path.Combine(LibInfo.CurrentDllDirectory, "Storage", "Output.bin");
}

}