using Newtonsoft.Json;

namespace RipeLib.Serializables.ArgumentsInfo.Parser.Json.RW
{
/// <summary> Groups some Params that Specify the Format for JSON Data when Read or Written. </summary>

public class JsonRWParams : ParamGroupInfo
{
/** <summary> Gets or Sets a Value that Determines how to Hande DateZone on JSON. </summary>
<returns> The Date Zone Handling. </returns> */

public DateTimeZoneHandling ZoneHandlingForDates{ get; set; }

/** <summary> Gets or Sets a String that Specify how to Format DateTimes. </summary>
<returns> The Date Format. </returns> */

public string DateFormat{ get; set; }

/// <summary> Creates a new Instance of the <c>JsonRWParams</c>. </summary>

public JsonRWParams()
{
ZoneHandlingForDates = DateTimeZoneHandling.RoundtripKind;
DateFormat = LibInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
}

/// <summary> Checks each nullable Field of the <c>JsonRWParams</c> Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
JsonRWParams defaultParams = new();

#region ======== Set default Values to Null Fields ========

DateFormat ??= defaultParams.DateFormat;

#endregion
}

}

}