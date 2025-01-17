using Newtonsoft.Json;

namespace RipeLib.Serializables.ArgumentsInfo.Parser.Json.RW
{
/// <summary> Groups some Params that Specify how to Read Data from JSON Files. </summary>

public class JsonReadConfig : JsonRWParams
{
/** <summary> The allowed Data Depth. </summary>

<remarks> Minimum Depth is <b>1</b>; Maximum is <b>128</b>. <para>
</para> You can also Set the Depth to <c>null</c> so No Limit is Applied. </remarks> */

private static readonly Limit<int> DataDepthRange = new(1, 128);

/** <summary> Gets or Sets a Boolean that Determines if InputStream should be Closed when the Reader is so. </summary>
<returns> <b>true</b> if InputStream should be Closed; otherwise, <b>false</b>. </returns> */

public bool CloseInputStreamWithReader{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Reading Multiple Data from Continous Streams 
is Allowed without Rasing Exceptions. </summary>

<returns> <b>true</b> if Continous Reading is Allowed; otherwise, <b>false</b>. </returns> */

public bool AllowContinousReadingFromDifStreams{ get; set; }

/** <summary> Gets or Sets a Value that Determines how to Handle Date Parsing on JSON. </summary>
<returns> The Date Parse Handling. </returns> */

public DateParseHandling ParseHandlingForDates{ get; set; }

/** <summary> Gets or Sets a Value that Determines how to Handle Float Values on JSON. </summary>
<returns> The Float Parse Handling. </returns> */

public FloatParseHandling ParseHandlingForFloats{ get; set; }

/** <summary> Gets or Sets the Maximum Depth when Reading JSON Data. </summary>
<returns> The Max Data Depth. </returns> */

public int? MaxDataDepth{ get; set; }

/** <summary> Gets or Sets a Table that Contains Info about the PropertyNames used by the Reader. </summary>
<returns> The Property Names. </returns> */

public JsonNameTable JsonPropertyNames{ get; set; }

/// <summary> Creates a new Instance of the <c>JsonReadConfig</c>. </summary>

public JsonReadConfig()
{
CloseInputStreamWithReader = true;
AllowContinousReadingFromDifStreams = false;

ParseHandlingForDates = DateParseHandling.DateTime;
ParseHandlingForFloats = FloatParseHandling.Double;

MaxDataDepth = DataDepthRange.MaxValue;
}

}

}