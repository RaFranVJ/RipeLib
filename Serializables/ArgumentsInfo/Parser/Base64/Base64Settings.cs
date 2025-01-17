namespace RipeLib.Serializables.ArgumentsInfo.Parser.Base64
{
/// <summary> Groups Info related to the Base64 Parser. </summary>

public class Base64Settings : GenericParseInfo
{
/** <summary> Gets or Sets a Boolean that Determines how Base64 strings should be Generated. </summary>
<returns> <b>true</b> if Base64 Strings should be threated as WebSafe Strings; otherwise, <b>false</b>. </returns> */

public bool IsWebSafe{ get; set; }

/// <summary> Creates a new Instance of the <c>Base64Info</c>. </summary>

public Base64Settings()
{
IsWebSafe = true;
}

}

}