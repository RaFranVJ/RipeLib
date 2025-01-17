using System;
using System.Reflection;
using System.Threading.Tasks;

/// <summary> Allows the Derived Classes to be Updated Field by Field. </summary>

public abstract class FieldUtil
{
/// <summary> Checks each nullable Field of this Instance and Validates it, in case it's <c>null</c>. </summary>

public virtual void CheckForNullFields()
{
return;
}

}