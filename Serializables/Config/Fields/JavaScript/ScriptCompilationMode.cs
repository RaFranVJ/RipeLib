using System;

namespace RipeLib.Serializables.Config.Fields.JavaScript
{

/// <summary> Determines how Scripts should be Compiled. </summary>

[Flags]

public enum ScriptCompilationMode
{
/// <summary> Default Script Compilation. </summary>
Default = 0,

/// <summary> Scripts will be Compiled as JS Documents. </summary>
CompileAsDocument = 1,

/// <summary> Cache Bytes will be Used for Fast Script Recompilation. </summary>
FastRecompilation = 2,

/// <summary> Compile JS Documents using Cache for Fast Recompilation. </summary>
RecompileDocuments = FastRecompilation | CompileAsDocument
}

}