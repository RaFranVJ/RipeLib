using Newtonsoft.Json;

namespace RipeLib.Serializables.ArgumentsInfo.Parser.Json.RW
{
/// <summary> Groups some Params that Specify how to Write Data to JSON Files. </summary>

public class JsonWriteConfig : JsonRWParams
{
/** <summary> Gets or Sets a Boolean that Determines if OutputStream should be Closed when the Writer is also Closed. </summary>
<returns> <b>true</b> if OutputStream should be Closed; otherwise, <b>false</b>. </returns> */

public bool CloseOutputStreamWithWriter{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if JSON should be Auto-Completed when the Writer is Closed. </summary>
<returns> <b>true</b> if JSON should be Auto-Completed; otherwise, <b>false</b>. </returns> */

public bool AutoCompleteJsonOnClose{ get; set; }

/** <summary> Gets or Sets a Value that Determines how to Hande JSON Formatting. </summary>
<returns> The JSON Formatting. </returns> */

public Formatting JsonFormatting{ get; set; }

/** <summary> Gets or Sets a Value that Determines how to Hande DateFormat on JSON. </summary>
<returns> The Date Format Handling. </returns> */

public DateFormatHandling FormatHandlingForDates{ get; set; }

/** <summary> Gets or Sets a Value that Determines how to Hande StringEscape on JSON. </summary>
<returns> The Escape Handling. </returns> */

public StringEscapeHandling EscapeHandlingForStrings{ get; set; }

/** <summary> Gets or Sets a Value that Determines how to Hande Float Values on JSON. </summary>
<returns> The Float Format Handling. </returns> */

public FloatFormatHandling FormatHandlingForFloats{ get; set; }

/** <summary> Gets or Sets a Value that Determines how many Chars to Add when Identation is Enabled. </summary>
<returns> The Json Identation. </returns> */

public int JsonIdentation{ get; set; }

/** <summary> Gets or Sets a Char used on Identation. </summary>
<returns> The Indent Cbar. </returns> */

public char IndentChar{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Object Names should be Surrounded by Queotes (' or "). </summary>
<returns> The Identation Cnars Count. </returns> */

public bool SurroundNamesWithQuotes{ get; set; }

/** <summary> Gets or Sets a Quote Char used for Surrounding Object Names. </summary>
<returns> The Quote Char. </returns> */

public char QuoteChar{ get; set; }

/// <summary> Creates a new Instance of the <c>JsonWriteConfig</c>. </summary>

public JsonWriteConfig()
{
CloseOutputStreamWithWriter = true;
AutoCompleteJsonOnClose = true;

JsonFormatting = Formatting.Indented;
FormatHandlingForDates = DateFormatHandling.IsoDateFormat;

EscapeHandlingForStrings = StringEscapeHandling.Default;
FormatHandlingForFloats = FloatFormatHandling.String;

JsonIdentation = 2;
IndentChar = '\t';

SurroundNamesWithQuotes = true;
QuoteChar = '\"';
}

}

}