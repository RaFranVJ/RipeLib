using System.Collections.Generic;
using System.IO;

namespace RipeLib.Serializables.SortUtils
{
/// <summary> Represents a Group Category. </summary>

public class GroupCategory : SerializableClass<GroupCategory>
{
/** <summary> Creates a Base Instance of the <c>FuncGroup</c> Class

<remarks> This Instance is used for Serializing Classes from the same Type. </summary>

<returns> The Base Group </returns> */

private static readonly FuncGroup baseGroup = new();

/** <summary> Gets or Sets the ID of this Category.  </summary>
<returns> The Category ID. </returns> */

public ushort CategoryID{ get; set; }

/** <summary> Gets or Sets the Display Name of this Category.  </summary>
<returns> The Category Name. </returns> */

public string CategoryName{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Files are allowed for this Category or not.  </summary>
<returns> <b>true</b> if Files are Allowed; otherwise, <b>false</b>. </returns> */

public bool AllowInputFromFiles{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Folders are allowed for this Category or not.  </summary>
<returns> <b>true</b> Folders are Allowed; otherwise, <b>false</b>. </returns> */

public bool AllowInputFromDirs{ get; set; }

/** <summary> Gets or Sets a Path to the Directory where the Groups are Located. </summary>
<returns> The Path to the Group Dir. </returns> */

public string PathToGroupsDir{ get; set; }

/** <summary> Obtains or Creates a List of Groups from a Category. </summary>
<returns> The Groups. </returns> */

protected List<FuncGroup> Groups;

/** <summary> Gets the Parent Dir where Categorys should be Located. </summary>
<returns> The Parent Dir to Categorys. </returns> */

protected override string ParentDir => Path.Combine(base.ParentDir, "GroupCategories");

/// <summary> Creates a new <c>GroupCategory</c>. </summary>

public GroupCategory()
{
CategoryID = 0;
CategoryName = "My Category Template";

PathToGroupsDir = baseGroup.GetParentDir();
}

/// <summary> Checks each nullable Field of the this instance given and Validates it, in case it's <c>null</c>. </summary>

protected override void CheckForNullFields()
{
GroupCategory defaultCategory = new();

#region ======== Set default Values to Null Fields ========

CategoryName ??= defaultCategory.CategoryName;
PathToGroupsDir ??= defaultCategory.PathToGroupsDir;

#endregion
}

/** <summary> Obtains a List of Groups from this Category. </summary>
<returns> The Groups. </returns> */

public List<FuncGroup> GetGroups() => Groups;

/// <summary> Sets the Groups Stored on this Category. </summary>

public void SetGroups(List<FuncGroup> sourceGroups) => Groups = sourceGroups;

/// <summary> Loades the Groups of this Category. </summary>

public void LoadGroups() => Groups = baseGroup.ReadObjects(PathToGroupsDir);
}

}