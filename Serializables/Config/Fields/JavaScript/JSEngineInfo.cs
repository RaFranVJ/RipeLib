using Microsoft.ClearScript.V8;

namespace RipeLib.Serializables.Config.Fields.JavaScript
{
/// <summary> Groups Info related to the Engine used for Running JavaScript Code. </summary>

public class JSEngineInfo : ConfigField
{
/** <summary> Gets or Sets the Engine Name. </summary>
<returns> The Engine Name. </returns> */

public string EngineName{ get; set; }

/** <summary> Gets or Sets some Constraints for the V8 Runtime. </summary>
<returns> The Runtime Constraints. </returns> */

public V8RuntimeConstraints RuntimeConstraints{ get; set; }

/** <summary> Gets or Sets some Attributes related to the JS Engine. </summary>
<returns> The Engine Flags. </returns> */

public V8ScriptEngineFlags EngineFlags{ get; set; }

/** <summary> Gets or Sets the TCP Port used for Debbuging JS Code. </summary>

<remarks> You can leave this Field as null if Debbuging is not Desired. </remarks>

<returns> The TCP Debug Port. </returns> */

public int? TCPortForDebugging{ get; set; }

/** <summary> Gets or Sets the Engine Properties. </summary>
<returns> The Engine Properties. </returns> */

public JSEngineProperties EngineProperties{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if the Host Data should be Kept Updated. </summary>
<returns> <b>true</b> if Host Data should be Updated; otherwise, <b>false</b>. </returns> */

public bool ShowExecutionDetails{ get; set; }

/** <summary> Gets or Sets a Value that Determines how to Compile Scripts. </summary>

<returns> The Compilation Mode. </returns> */

public ScriptCompilationMode CompilationMode{ get; set; }

/// <summary> Creates a new Instance of the <c>JSEngineInfo</c>. </summary>

public JSEngineInfo()
{
EngineName = "My JavaScript Engine";
RuntimeConstraints = new();

EngineProperties = new();
CompilationMode = ScriptCompilationMode.FastRecompilation;
}

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
JSEngineInfo defaultInfo = new();

EngineName ??= defaultInfo.EngineName;
RuntimeConstraints ??= defaultInfo.RuntimeConstraints;
}

}

}