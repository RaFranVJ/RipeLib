using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using System;

namespace RipeLib.Serializables.Config.Fields.JavaScript
{
/// <summary> Groups Info related to the Engine used for Running JavaScript Code. </summary>

public class JSEngineProperties : ConfigField
{
/** <summary> Gets or Sets a Type that allows Script code to access non-host resources. </summary>
<returns> The Access Context. </returns> */

public Type AccessContext{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Script code can use Reflection or not. </summary>
<returns> <b>true</b> if Reflection is Allowed; otherwise, <b>false</b> </returns> */

public bool AllowReflection{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Script code should be Formatted or not. </summary>
<returns> <b>true</b> if Code Formatting is Enabled; otherwise, <b>false</b> </returns> */

public bool CodeFormatting{ get; set; }

/** <summary> Gets or Sets some Config used for Loading Documents. </summary>
<returns> The Documents Settings. </returns> */

public DocumentSettings ConfigForDocumentsLoader{ get; set; }

/** <summary> Gets or Sets the default Access sfor all members of exposed objects in a Script. </summary>
<returns> The Default Script Access. </returns> */

public ScriptAccess DefaultScriptAccess{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Method Binding is Enabled or not. </summary>
<returns> <b>true</b> if Dynamic Bindind is Enabled; otherwise, <b>false</b> </returns> */

public bool DisableDynamicBinding{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Extension Methods are Disabled or not. </summary>
<returns> <b>true</b> if Extension Methods are Disabled; otherwise, <b>false</b> </returns> */

public bool DisableExtensionMethods{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if automatic host variable tunneling is Enabled or not. </summary>
 
<remarks> Automatic host variable tunneling is Used for by-reference arguments to script functions and delegates </remarks>
 
<returns> <b>true</b> if Type Restriction is Disabled; otherwise, <b>false</b> </returns> */

public bool EnableAutoHostVariables{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Access Restrictions for Anonymous Types are Enabled or not. </summary>
<returns> <b>true</b> if Anonymous Type Restriction is Enabled; otherwise, <b>false</b> </returns> */

public bool EnforceAnonymousTypeAccess{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Extension Methods should be Enumerated or not. </summary>
<returns> <b>true</b> if Extension Methods should be Enumerated; otherwise, <b>false</b> </returns> */

public bool EnumerateExtensionMethods{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Instance Methods should be Enumerated or not. </summary>
<returns> <b>true</b> if Instance Methods should be Enumerated; otherwise, <b>false</b> </returns> */

public bool EnumerateInstanceMethods{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Static Members in Host Objects should be Expossed or not. </summary>
<returns> <b>true</b> if Static Members should be Expossed; otherwise, <b>false</b> </returns> */

public bool ExposeStaticMembers{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Runtime Propagation should be Interrupted or not. </summary>
<returns> <b>true</b> if Float Narrowing is Used; otherwise, <b>false</b> </returns> */

public bool InterruptRuntimePropagation{ get; set; }

/** <summary> Gets or Sets the Max size of the V8 runtime's heap. </summary>
<returns> The Max Runtime Heap Size.  </returns> */

public UIntPtr MaxRuntimeHeapSize{ get; set; }

/** <summary> Gets or Sets the Max amount by which the V8 runtime is permitted to grow the stack during script execution. </summary>
<returns> The Max Runtime Stack Usage.  </returns> */

public UIntPtr MaxRuntimeStackUsage{ get; set; }

/** <summary> Gets or Sets a Behavior in response to a violation of the maximum heap size.. </summary>
<returns> The Runtime Heap Behavior. </returns> */

public V8RuntimeViolationPolicy RuntimeHeapBehavior{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Type Restriction is Disabled for Fields, Properties or Method Return Values. </summary>
<returns> <b>true</b> if Type Restriction is Disabled; otherwise, <b>false</b> </returns> */

public bool StrictTyping{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Type Restriction is Disabled for Collections and its Elements. </summary>
<returns> <b>true</b> if Type Restriction is Disabled; otherwise, <b>false</b> </returns> */

public bool StrictTypingForCollections{ get; set; }

/** <summary> Gets or Sets the time between automatic CPU profile samples, in microseconds. </summary>
<returns> The Cpu Interval . </returns> */

public uint TimeBetweenCpuSamples{ get; set; }

/** <summary> Gets or Sets the time between consecutive heap size samples. </summary>
<returns> The Heap Interval . </returns> */

public TimeSpan TimeBetweenHeapSamples{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Float Narrowing should be Used or not. </summary>
<returns> <b>true</b> if Float Narrowing is Used; otherwise, <b>false</b> </returns> */

public bool UseFloatNarrowing{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if flection-based method binding as a fallback should be Used. </summary>
<returns> <b>true</b> if Reflection Bind is Used; otherwise, <b>false</b> </returns> */

public bool UseReflectionBindFallback{ get; set; }

 /** <summary> Gets or Sets a Boolean that Determines if Properties or Method Values should be Wrapped when null . </summary>
<returns> <b>true</b> if Null Results should be Wrapped; otherwise, <b>false</b> </returns> */

public bool WrapNullResults{ get; set; }

/// <summary> Creates a new Instance of the <c>JSEngineProperties</c>. </summary>

public JSEngineProperties()
{

ConfigForDocumentsLoader = new()
{
AccessFlags = DocumentAccessFlags.EnableAllLoading // Load Web & Local Files
};

CodeFormatting = true;
StrictTyping = true;

StrictTypingForCollections = true;
TimeBetweenCpuSamples = 1000;
}

/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
JSEngineProperties defaultProps = new();

ConfigForDocumentsLoader ??= defaultProps.ConfigForDocumentsLoader;
}

}

}