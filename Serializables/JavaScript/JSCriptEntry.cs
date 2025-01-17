using Microsoft.ClearScript.V8;
using System;
using System.IO;
using System.Collections.Generic;
using RipeLib.Modules;

namespace RipeLib.Serializables.JavaScript
{
/// <summary> Represents a Entry for a JavaScript File. </summary>

public class JSCriptEntry : SerializableClass<JSCriptEntry>
{
/** <summary> Gets or Sets the Cache Kind used for Handling Script Data on Compilation. </summary>
<returns> The Cache Kind. </returns> */

public V8CacheKind CacheKind{ get; set; }

/** <summary> Gets or Sets a Path to a File from which to Read/Write the Script Cache. </summary>

<remarks> Set this to Null if Fast Recompilation Mode is disabled. </remarks>

<returns> The Path to Cache Data. </returns> */

public string PathToScriptCache{ get; set; }

/** <summary> Gets or Sets some Info related to the Script Document. </summary>
<returns> The Script Metadata. </returns> */

public JSCriptMetadata ScriptMetadata{ get; set; }

/** <summary> Gets or Sets a Dictionary that Maps each Variable to Expose and its respective Type. </summary>

<remarks> You must define C# Types here (or define nothing if no Types need to be Expossed) </remarks>

<returns> The Types to Expose. </returns> */

public Dictionary<string, Type> TypesToExpose{ get; set; }

/** <summary> Gets or Sets a Dictionary that Maps each Item within its Name. </summary>

<remarks> Named Items are C# Objects that will be Expossed in the Script (set this to null, otherwise). </remarks>

<returns> The Named Items. </returns> */

public Dictionary<string, object> NamedItems{ get; set; }

/** <summary> Gets the Parent Dir where ScriptEntries should be Located. </summary>
<returns> The Parent Dir to ScriptEntries. </returns> */

protected override string ParentDir => Path.Combine(base.ParentDir, "ScriptEntries");

/// <summary> Creates a new Instance of the <c>JSCriptEntry</c>. </summary>

public JSCriptEntry()
{
CacheKind = V8CacheKind.Code;
PathToScriptCache = GetDefaultCachePath();

ScriptMetadata = new();
}

// Get default Path for Script Cache

private static string GetDefaultCachePath()
{
string filePath = JSCriptMetadata.GetDefaultJSPath();
string basePath = Path.GetDirectoryName(filePath).Replace("SourceCode", "CacheBytes");

return Path.Combine(basePath, Path.GetFileNameWithoutExtension(filePath) + ".bin");
}

// Read Cache Bytes generated by the Compilation Process

private byte[] ReadCache()
{
DirManager.CheckMissingFolder(Path.GetDirectoryName(PathToScriptCache) );

if(!File.Exists(PathToScriptCache) || FileManager.FileIsEmpty(PathToScriptCache) )
return Array.Empty<byte>();

return File.ReadAllBytes(PathToScriptCache);
}

// Write Cache Bytes generated by the Compilation Process

private void WriteCache(byte[] cacheBytes)
{
DirManager.CheckMissingFolder(Path.GetDirectoryName(PathToScriptCache) );

File.WriteAllBytes(PathToScriptCache, cacheBytes);
}

// Compile the Script

public V8Script CompileScript(V8ScriptEngine sourceEngine)
{
return sourceEngine.Compile(ScriptMetadata.GetScriptName(), ScriptMetadata.ReadScript() );
}

// Compile the Script using DocInfo

public V8Script CompileScriptAsDoc(V8ScriptEngine sourceEngine)
{
return sourceEngine.Compile(ScriptMetadata.GetDocumentInfo(), ScriptMetadata.ReadScript() );
}

// Compile the Script by exporting the CacheBytes

public V8Script CompileCacheScript(V8ScriptEngine sourceEngine)
{
byte[] cacheBytes = ReadCache();

V8Script compiledScript;

if(cacheBytes == null || cacheBytes.Length == 0)
{
compiledScript = sourceEngine.CompileDocument(ScriptMetadata.PathToJScriptFile, CacheKind, out cacheBytes);

WriteCache(cacheBytes);
}

else
compiledScript = sourceEngine.CompileDocument(ScriptMetadata.PathToJScriptFile, CacheKind, ref cacheBytes, out _);

return compiledScript;
}

// Compile the Script Document by exporting the CacheBytes

public V8Script CompileCacheScriptAsDoc(V8ScriptEngine sourceEngine)
{
byte[] cacheBytes = ReadCache();

V8Script compiledScript;

if(cacheBytes == null || cacheBytes.Length == 0)
{
compiledScript = sourceEngine.CompileDocument(ScriptMetadata.PathToJScriptFile, ScriptMetadata.Category, CacheKind,
out cacheBytes);

WriteCache(cacheBytes);
}

else
compiledScript = sourceEngine.CompileDocument(ScriptMetadata.PathToJScriptFile, ScriptMetadata.Category, CacheKind,
ref cacheBytes, out _);

return compiledScript;
}

}

}