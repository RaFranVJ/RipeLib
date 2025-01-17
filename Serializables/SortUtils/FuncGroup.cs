using System.Collections.Generic;
using System.IO;

namespace RipeLib.Serializables.SortUtils
{
/// <summary> Represents a Group of Functions. </summary>

public class FuncGroup : SerializableClass<FuncGroup>
{
/** <summary> Creates a Base Instance of the <c>Function</c> Class </summary>

<remarks> This Instance is used for Serializing Classes from the same Type. </remarks>

<returns> The Base Function </returns> */

private static readonly Function baseFunc = new();

/** <summary> Gets or Sets the ID of this Group. </summary>
<returns> The Group ID. </returns> */

public ushort GroupID{ get; set; }

/** <summary> Gets or Sets the Display Name of this Group. </summary>
<returns> The Group Name. </returns> */

public string GroupName{ get; set; }

/** <summary> Gets or Sets a Path to the Directory where the Sub-Groups are Located. </summary>

<remarks> This Field can be Null or Empty if using SubGroups is not Desired. </remarks>

<returns> The Path to the Sub-Groups Dir. </returns> */

public string PathToSubGroupsDir{ get; set; }

/** <summary> Gets or Sets a Path to the Directory where the Functions are Located. </summary>

<remarks> This Field can be Null or Empty if using Sub-Groups is Desired. </remarks>

<returns> The Path to the Funcs Dir. </returns> */

public string PathToFuncsDir{ get; set; }

/** <summary> Obtains or Creates a List of Sub-Groups inside a Group. </summary>
<returns> The SubGroups. </returns> */

protected List<FuncGroup> SubGroups;

/** <summary> Obtains or Creates a List of Function from a Group. </summary>
<returns> The Functions. </returns> */

protected List<Function> Functions;

/** <summary> Gets the Parent Dir where FuncGroups should be Located. </summary>
<returns> The Parent Dir to Groups. </returns> */

protected override string ParentDir => Path.Combine(base.ParentDir, "Groups");

/// <summary> Creates a new Instance of the <c>FuncGroup</c> Class. </summary>

public FuncGroup()
{
GroupName = "My Group Template";
}

/// <summary> Checks each nullable Field of the this instance given and Validates it, in case it's <c>null</c>. </summary>

protected override void CheckForNullFields()
{
FuncGroup defaultGroup = new();

#region ======== Set default Values to Null Fields ========

GroupName ??= defaultGroup.GroupName;

#endregion
}

/** <summary> Obtains a List of Sub-Groups from this Group. </summary>
<returns> The Sub-Groups List. </returns> */

public List<FuncGroup> GetSubGroups() => SubGroups;

/// <summary> Sets a List of Sub-Groups to this Group. </summary>

public void SetSubGroups(List<FuncGroup> sourceSubGroups) => SubGroups = sourceSubGroups;

/// <summary> Loads the Sub-Groups of this Group. </summary>

public void LoadSubGroups() => SubGroups = ReadObjects(PathToSubGroupsDir);

/** <summary> Obtains a List of Functions from this Group. </summary>
<returns> The Functions List. </returns> */

public List<Function> GetFunctions() => Functions;

/// <summary> Sets the Functions Stored on this Group. </summary>

public void SetFunctions(List<Function> sourceFuncs) => Functions = sourceFuncs;

/// <summary> Loads the Functions Stored of this Group. </summary>

public void LoadFunctions() => Functions = baseFunc.ReadObjects(PathToFuncsDir);
}

}