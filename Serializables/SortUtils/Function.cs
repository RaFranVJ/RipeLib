using System.IO;
using System.Collections.Generic;
using RipeLib.Serializables.ArgumentsInfo.FileManager.Archive;
using RipeLib.Serializables.JavaScript;

namespace RipeLib.Serializables.SortUtils
{
/// <summary> Represents a Function that will be Called from a JS File. </summary>

public class Function : SerializableClass<Function>
{
/** <summary> Gets or Sets the ID of this Function.  </summary>
<returns> The Function ID. </returns> */

public ushort FuncID{ get; set; }

/** <summary> Gets or Sets the Display Name of this Function.  </summary>
<returns> The Function Name. </returns> */

public string FuncName{ get; set; }

/** <summary> Gets or Sets a Instance of the <c>ArchiveFilterCriteria</c>. </summary>

<remarks> This Field Determines which Files are Allowed by Name and Extension. </remarks>

<returns> The Archives Filter. </returns> */

public ArchiveFilterCriteria ArchivesFilter{ get; set; }

/** <summary> Gets or Sets a Path where the Script related to this Function is Located. </summary>
<returns> The Path to the Script Entry. </returns> */

public string PathToScriptEntry{ get; set; }

/** <summary> Gets or Sets the Name of the Method to be Called from the Script. </summary>

<remarks> This Field can be Null or Empty in case the Script is a direct Expression. </remarks>

<returns> The Method Name. </returns> */

public string ScriptMethodName{ get; set; }

/** <summary> Gets or Sets the Type of expected Arguments to Pass in the Script. </summary>
<returns> The Args Type. </returns> */

public ScriptArgsType ArgsType{ get; set; }

/** <summary> Gets or Sets the Input Path to be passed as argument to the Script method. </summary>
<returns> The Input Path. </returns> */

public string GenericPathForInput{ get; set; }

/** <summary> Gets or Sets the Output Path to be passed as argument to the Script method. </summary>
<returns> The Output Path. </returns> */

public string GenericPathForOutput{ get; set; }

/** <summary> Gets or Sets a List of Arguments to be Passed in a Script. </summary>
<returns> The Script Args. </returns> */

public List<object> GenericArgs{ get; set; }

/** <summary> Gets or Sets a Dictionary that Stores info about the Parameters to Expose in a Script. </summary>

<remarks> The Collection needs a ParamName along with a C# Object to Expose. </remarks>

<returns> The Args to Expose. </returns> */

public Dictionary<string, object> GenericArgsToExpose{ get; set; }

/** <summary> Gets or Sets the Input Path to be passed as argument to the Script method. </summary>

<remarks> This Field is an Array that Points to the <c>InputPath</c> Field of the <c>UserParams</c> Properties </remarks>

<returns> The Input Path. </returns> */

public string[] SpecificPathForInput{ get; set; }

/** <summary> Gets or Sets the Output Path to be passed as argument to the Script method. </summary>

<remarks> This Field is an Array that Points to the <c>OutputPath</c> Field of the <c>UserParams</c> Properties </remarks>

<returns> The Output Path. </returns> */

public string[] SpecificPathForOutput{ get; set; }

/** <summary> Gets or Sets a List of Params to be Passed in a Script. </summary>

<remarks> Each Element in the List is an Array that Points to a Field in the <c>UserParams</c> class </remarks>

<returns> The Script Params. </returns> */

public List<string[]> SpecificArgs{ get; set; }

/** <summary> Gets or Sets a Dictionary that Stores info about the Parameters to Expose in a Script. </summary>

<remarks> The Collection needs a ParamName along with a Path to that Field in the <c>UserParams</c> class. </remarks>

<returns> The Mapped Params. </returns> */

public Dictionary<string, string[]> SpecificArgsToExpose{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Arguments should be Expossed first. </summary>

<returns> <b>true</b> if Arguments should be Expossed first; otherwise, <b>false</b>. </returns> */

public bool ExposeArgsFirst{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if InputPath should be Ignored on MethodCalls. </summary>

<returns> <b>true</b> if InputPath should be Ignored; otherwise, <b>false</b>. </returns> */

public bool IgnoreInputPathOnMethodCalls{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if OutputPath should be Ignored on MethodCalls. </summary>

<returns> <b>true</b> if OutputPath should be Ignored; otherwise, <b>false</b>. </returns> */

public bool IgnoreOutputPathOnMethodCalls{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if BatchMode should be Disabled for Dirs. </summary>

<remarks> Setting this to <b>true</b> will hide the Function from being selected for Dirs. </remarks>

<returns> <b>true</b> if BatchMode is Disabled; otherwise, <b>false</b>. </returns> */

public bool DisableBatchMode{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Dirs should be Threated as Files. </summary>

<remarks> You may want to Set this to <b>true</b> when Operating with Directories only, not with their Content. </remarks>

<returns> <b>true</b> if SingleMode should be Forced; otherwise, <b>false</b>. </returns> */

public bool ForceSingleMode{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Script should be Run exclusively for Results. </summary>

<remarks> Setting this to <b>true</b> will make the Tool to Only Evaluate the Script, ignoring FileSystems. </remarks>

<returns> <b>true</b> if FileSystems should be Ignored; otherwise, <b>false</b>. </returns> */

public bool RunScriptForResultOnly{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if this Function should be Hidden from QuickMode. </summary>

<returns> <b>true</b> if Func should be Hidden for QuickMode; otherwise, <b>false</b>. </returns> */

public bool HideFromQuickMode{ get; set; }

/** <summary> Groups the Message Status IDS for the Method of this Instance. </summary>

<remarks> This Field can be Null in case Displaying Process Status is not Desired. </remarks>

<returns> The Process Msgs. </returns> */

public FuncStatusMsgs ProcessMsgs{ get; set; }

/** <summary> Gets the Parent Dir where Functions should be Located. </summary>
<returns> The Parent Dir to Funcs. </returns> */

protected override string ParentDir => Path.Combine(base.ParentDir, "Functions");

/// <summary> Creates a new Instance of the <c>Function</c> Class. </summary>

public Function()
{
FuncName = "My Function Template";
PathToScriptEntry = new JSCriptEntry().GetFilePath();

ArgsType = ScriptArgsType.None;
}

/// <summary> Checks each nullable Field of this instance given and Validates it, in case it's <c>null</c>. </summary>

protected override void CheckForNullFields()
{
Function defaultFunc = new();

#region ======== Set default Values to Null Fields ========

FuncName ??= defaultFunc.FuncName;
PathToScriptEntry ??= defaultFunc.PathToScriptEntry;

#endregion

ArchivesFilter?.CheckForNullFields();
}

}

}