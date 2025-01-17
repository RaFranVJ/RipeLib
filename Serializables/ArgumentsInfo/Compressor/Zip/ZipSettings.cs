using System.IO.Compression;
using System;

namespace RipeLib.Serializables.ArgumentsInfo.Compressor.Zip
{
/// <summary> Groups options related to the Zip Compressor. </summary>

public class ZipSettings : ParamGroupInfo
{
/** <summary> Gets or Sets the Mode used when Compressing Data. </summary>
<returns> The ZIP Mode. </returns> */

public ZipArchiveMode ZipMode{ get; set; }

/** <summary> Gets or Sets the Type of Encoding to use when Naming File Entries. </summary>
<returns> The Encoding Type. </returns> */

public string EncodingForEntryNames{ get; set; }

/** <summary> Gets or Sets the Comment to Write on the ZIP Files. </summary>

<remarks> The Encoding is determined by the <b>EncodingForEntryNames</b> Field in the <c>ZipArchive</c> Constructor. </remarks>

<returns> The File Comment. </returns> */

public string OptionalFileComment{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if all Entties inside a ZIP File should be Extracted at once or not. </summary>

<remarks> If set to <b>false</b> the User must Select the desired Entry from the ZIP File. </remarks>

<returns> <b>truee</b> if all Entries should be Extracted at once; otherwise, <b>false</b>. </returns> */

public bool ExtractAllEntriesAtOnce{ get; set; }

/** <summary> Gets or Sets Info for the ZIP Entries. </summary>
<returns> The Entries Info. </returns> */

public ZipEntriesInfo EntriesInfo{ get; set; }

/// <summary> Creates a new Instance of the <c>ZipInfo</c>. </summary>

public ZipSettings()
{
ZipMode = ZipArchiveMode.Create;
EncodingForEntryNames = Console.OutputEncoding.ToString();

ExtractAllEntriesAtOnce = true;
EntriesInfo = new();
}

/// <summary> Checks each nullable Field of the <c>BZip2Info</c> Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields()
{
ZipSettings defaultOptions = new();

#region ======== Set default Values to Null Fields ========

EncodingForEntryNames ??= defaultOptions.EncodingForEntryNames;
EntriesInfo ??= defaultOptions.EntriesInfo;

#endregion
}

}

}