using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace RipeLib.Serializables
{
/// <summary> Groups a List of Strings that can be Localized by Language. </summary>

public class LocalizedStrings : SerializableClass<LocalizedStrings>
{
/** <summary> Gets a Value that Contains info about the Culture Name. </summary>
<returns> The Culture Name. </returns> */

public string CultureName{ get; set; }

/** <summary> Gets or Sets a Value that Contains info about the Strings localized by Language. </summary>
<returns> The Strings localized by Language. </returns> */

public SortedDictionary<string, string> StringsMap{ get; set; }

/** <summary> Gets or Sets the <c>CultureInfo> asociated with the Language of this Strings. </summary>
<returns> The Language Info. </returns> */

protected CultureInfo LanguageInfo;

/** <summary> Gets the Parent Dir where LocalizedStrings should be Located. </summary>
<returns> The Parent Dir to LocalizedStrings. </returns> */

protected override string ParentDir => Path.Combine(base.ParentDir, GetType().Name);

/// <summary> Creates a new Instance of the <c>LocalizedStrings</c> </summary>

public LocalizedStrings()
{
CultureName = "en-US";

// String template

StringsMap = new()
{

{ "LOCAL_STRING_ID", "<Strings follow an ID preferably written in UPPERCASE>" }

};

SetLanguageInfo();
}

/// <summary> Checks each nullable Field of the this instance given and Validates it, in case it's <c>null</c>. </summary>

protected override void CheckForNullFields()
{
LocalizedStrings defaultInfo = new();

#region ======== Set default Values to Null Fields ========

CultureName ??= defaultInfo.CultureName;
StringsMap ??= defaultInfo.StringsMap;
LanguageInfo ??= defaultInfo.LanguageInfo;

#endregion
}

/** <summary> Gets a Reference to the LanguageInfo related to the LocalizedStrings </summary>
<returns> The Language Info </returns> */

public CultureInfo GetLanguageInfo() => LanguageInfo;

/// <summary> Sets the LanguageInfo related to the LocalizedStrings by using the Specified CultureInfo </summary>

public void SetLanguageInfo()
{

if(string.IsNullOrEmpty(CultureName) )
CultureName = "en-US";

LanguageInfo = new(CultureName, false);
}

/** <summary> Locates a Text by searching for its ID. </summary>

<param name = "sourceID"> The ID of the Text. </param>
<param name="useSpecialTagOnMissingStr">Indicates whether to use a Special Tag for missing text.</param>

<returns> The text Localized by its ID. </returns> */

public string LocateByID(string sourceID, bool useSpecialTagOnMissingStr = true)
{

if(string.IsNullOrEmpty(sourceID) )
throw new ArgumentNullException(nameof(sourceID), "ID can't be Null or Empty");

if(StringsMap.TryGetValue(sourceID, out string locValue) )
return locValue;

return useSpecialTagOnMissingStr ? $"<Missing Text: {sourceID}>" : sourceID;
}

}

}