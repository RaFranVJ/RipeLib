namespace RipeLib.Serializables.JavaScript
{
/// <summary> Determines how to Treat the Arguments before Passing them to a Script </summary>

public enum ScriptArgsType
{
/// <summary> Arguments should be Ignored </summary>
None,

/// <summary> Arguments will be Passed as Object References. </summary>
Generic,

/// <summary> Arguments will be Passed as <c>UserParams</c> Fields.  </summary>
Specific
}

}