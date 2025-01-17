using JsonParams = ServiceStack.Text.Config;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text.Json;

/// <summary> Initializes serializing Functions for JSON Data. </summary>

public static partial class JsonSerializer
{
/// <summary> The Settings used for Parsing JSON Data. </summary>

private static readonly Action<JsonParams> parseSettings = delegate(JsonParams serialConfig)
{
serialConfig.ParsePrimitiveFloatingPointTypes = ParseAsType.Double;
serialConfig.TryParseIntoBestFit = true;
serialConfig.TreatEnumAsInteger = false;
serialConfig.ExcludeTypeInfo = true;
serialConfig.Indent = true;
serialConfig.MaxDepth = 64;
serialConfig.EscapeUnicode = false;
};

public static void CleanJsonString(ref string sourceStr)
{
InputHelper.RemoveLiteralChars(ref sourceStr);

sourceStr = sourceStr.Replace("\"{", "{").Replace("}\"", "}");

sourceStr = JsonPropRegex().Replace(sourceStr, "\"$1\"");
sourceStr = JsonArrayRegex().Replace(sourceStr, "\"$1\"");
}

// Evluate Str Type

private static object EvaluateString(string str)
{
CleanJsonString(ref str);

if(IsSpecialString(str) )
return str;

else if(IsJson(str) )
return ParseJson(str);

else if(IsJsonArray(str) )
return ParseArray(str);

return str;
}

// Process Tokens

private static object ProcessJsonElement(JsonElement element)
{

return element.ValueKind switch
{
JsonValueKind.Object => ParseJson(element.GetRawText() ),
JsonValueKind.Array => ParseArray(element.GetRawText() ),
JsonValueKind.String => EvaluateString(element.GetString() ),
JsonValueKind.Number => element.GetDecimal(),
JsonValueKind.True or JsonValueKind.False  => element.GetBoolean(),
_ => null
};

}

// Convert a Json Str into a Dictionary

public static Dictionary<string, object> ParseJson(string jsonStr)
{
CleanJsonString(ref jsonStr);

using var jsonDoc = JsonDocument.Parse(jsonStr);
Dictionary<string, object> result = new();

foreach(var property in jsonDoc.RootElement.EnumerateObject() )
result.Add(property.Name, ProcessJsonElement(property.Value) );

return result;
}

// Convert a Json Str into an Array

public static List<object> ParseArray(string jsonStr)
{
CleanJsonString(ref jsonStr);

using var jsonDoc = JsonDocument.Parse(jsonStr);
List<object> result = new();

foreach(var element in jsonDoc.RootElement.EnumerateArray() )
result.Add(ProcessJsonElement(element) );

return result;
}

/** <summary> Formats a JSON String by using the Defined Params by User. </summary>

<param name = "sourceStr"> The String to be Formatted. </param>

<returns> The Formatted String. </returns> */

public static void FormatJsonString(ref string sourceStr)
{
Newtonsoft.Json.JsonSerializer jsonParser = new();

using StringReader strReader = new(sourceStr);
using JsonTextReader jsonReader = new(strReader);

object jsonObj = jsonParser.Deserialize(jsonReader);

if(jsonObj == null)
return;

StringWriter strWriter = new();

using JsonTextWriter jsonWriter = new(strWriter)
{
Formatting = Formatting.Indented,
Indentation = 2,
IndentChar = '\t'
};

jsonParser.Serialize(jsonWriter, jsonObj);
sourceStr = strWriter.ToString();
}

// Check if Str is Json

public static bool IsJson(string str)
{
CleanJsonString(ref str);

try
{
using var jsonDoc = JsonDocument.Parse(str);

return jsonDoc.RootElement.ValueKind == JsonValueKind.Object;
}

catch(System.Text.Json.JsonException)
{
return false;
}

}

// Str ID

public static bool IsSpecialString(string str) => SpecialStrRegex().IsMatch(str);

// Check if Str is Json Array

public static bool IsJsonArray(string str)
{
InputHelper.RemoveLiteralChars(ref str);

try
{
using var jsonDoc = JsonDocument.Parse(str);

return jsonDoc.RootElement.ValueKind == JsonValueKind.Array;
}

catch(System.Text.Json.JsonException)
{
return false;
}

}

/** <summary> Reads the Data of a JSON File. </summary>

<param name = "targetPath"> The Path where the JSON File to be Read is Located. </param>

<returns> The Data Read. </returns> */

public static JToken ReadJsonFile(string targetPath, JsonLoadSettings loadCfg)
{
return JToken.Parse(File.ReadAllText(targetPath), loadCfg);
}

// Method WriteJsonFile here

/** <summary> Serializes a Object as a JSON String. </summary>

<param name = "targetObject"> The Object to be Serialized. </param>

<returns> The Object serialized. </returns> */

public static string SerializeObject<T>(T targetObj) => targetObj.ToJson(parseSettings);

/** <summary> Deserializes a JSON String as a Object. </summary>

<param name = "targetStr"> The String to be Deserialized. </param>

<returns> The deserialized Object. </returns> */

public static T DeserializeObject<T>(string targetStr) => targetStr.FromJson<T>();

[GeneratedRegex(@"(?<![""\\])(?<=\{|\s|,)([a-zA-Z_][a-zA-Z0-9_]*)(?=\s*:)") ]
private static partial Regex JsonPropRegex();

[GeneratedRegex(@"(?<=\[|\s)(\w+)(?=,|\s|\])") ]
private static partial Regex JsonArrayRegex();

[GeneratedRegex(@"^\[[a-zA-Z][a-zA-Z0-9_]*\]$") ]
private static partial Regex SpecialStrRegex();
}