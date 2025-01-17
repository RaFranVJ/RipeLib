using Microsoft.ClearScript;
using System.IO;
using RipeLib.Modules;

namespace RipeLib.Serializables.JavaScript
{
/// <summary> Represents Metadata Info related to a JavaScript File. </summary>

public class JSCriptMetadata
{
/** <summary> Gets or Sets a Descriptive Name for the Script. </summary>

<remarks> This field is Optional, by default Script Names are Obtained from JS Files. </remarks>

<returns> The Document Name. </returns> */

public string DocumentName{ get; set; }

/** <summary> Gets or Sets a Path where the JS File is Located. </summary>
<returns> The Path to the Script. </returns> */

public string PathToJScriptFile{ get; set; }

/** <summary> Gets or Sets the Document Category. </summary>
<returns> The Document Category </returns> */

public DocumentCategory Category{ get; set; }

/** <summary> Gets or Sets the Document Flags. </summary>
<returns> The Document Flags </returns> */

public DocumentFlags Flags{ get; set; }

/// <summary> Creates a new Instance of the <c>JSCriptMetadata</c>. </summary>

public JSCriptMetadata()
{
DocumentName = "My Script Template";
PathToJScriptFile = GetDefaultJSPath();

Category = DocumentCategory.Script;
}

// Safe way for Retreiving the 'ScriptName' field

public string GetScriptName()
{
string defaultName = Path.GetFileNameWithoutExtension(PathToJScriptFile);

return string.IsNullOrEmpty(DocumentName) ? defaultName : DocumentName;
}

// Get default Path to the JS File

public static string GetDefaultJSPath()
{
return Path.Combine(LibInfo.CurrentDllDirectory, "Storage", "Scripts", "SourceCode", "HelloWorld.js");
}

// Get Document Info

public DocumentInfo GetDocumentInfo()
{

return new(DocumentName)
{
Category = Category,
Flags = Flags
};

}

// Dialog for Missing Scripts

public void CheckMissingScript(string dirPath = default)
{
dirPath ??= Path.GetDirectoryName(PathToJScriptFile);

DirManager.CheckMissingFolder(dirPath);

if(!File.Exists(PathToJScriptFile) || FileManager.FileIsEmpty(PathToJScriptFile) )
WriteScript();

}

/// <summary> Reads the Contents of the JS File. </summary>

public string ReadScript()
{
string dirPath = Path.GetDirectoryName(PathToJScriptFile);

CheckMissingScript(dirPath);

return File.ReadAllText(PathToJScriptFile);
}

// Write Data to JS File

public void WriteScript(string content = "console.log(\"Hello, World :)\")")
{
DirManager.CheckMissingFolder(Path.GetDirectoryName(PathToJScriptFile) );

File.WriteAllText(PathToJScriptFile, content);
}

}

}