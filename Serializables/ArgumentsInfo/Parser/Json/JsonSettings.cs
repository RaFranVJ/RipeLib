using RipeLib.Serializables.ArgumentsInfo.Parser.Json.RW;

namespace RipeLib.Serializables.ArgumentsInfo.Parser.Json
{
/// <summary> Groups Info related to the JSON Parser. </summary>

public class JsonSettings : ParamGroupInfo
{
/** <summary> Gets or Sets some Config related to the JSON Reader. </summary>
<returns> The Reader Config. </returns> */

public JsonReadConfig ReaderConfig{ get; set; }

/** <summary> Gets or Sets some Config related to the JSON Writer. </summary>
<returns> The JSON Writer Config. </returns> */

public JsonWriteConfig WriterConfig{ get; set; }

/// <summary> Creates a new Instance of the <c>JsonConfig</c>. </summary>

public JsonSettings()
{
ReaderConfig = new();
WriterConfig = new();
}

/// <summary> Checks each nullable Field of the <c>JsonInfo</c> Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
JsonSettings defaultOptions = new();

#region ======== Set default Values to Null Fields ========

ReaderConfig ??= defaultOptions.ReaderConfig;
WriterConfig ??= defaultOptions.WriterConfig;

#endregion
}

}

}