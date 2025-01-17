using System;
using System.Collections.Generic;
using Microsoft.ClearScript.V8;
using RipeLib.Serializables.Config.Fields.JavaScript;
using RipeLib.Serializables.JavaScript;

/// <summary> Handles the Execution of JavaScript (JS) Files. </summary>

public static class JSHelper
{
// Compile a new Script

public static V8Script CompileScript(JSCriptEntry sourceEntry, V8ScriptEngine jsEngine, ScriptCompilationMode compileMode)
{

return compileMode switch
{
ScriptCompilationMode.CompileAsDocument => sourceEntry.CompileScriptAsDoc(jsEngine),
ScriptCompilationMode.FastRecompilation => sourceEntry.CompileCacheScript(jsEngine),
ScriptCompilationMode.RecompileDocuments => sourceEntry.CompileCacheScriptAsDoc(jsEngine),
_ => sourceEntry.CompileScript(jsEngine)
};

}

// Setup the ScriptEngine by Expossing some Types

public static void ExposeTypes(V8ScriptEngine engine, Dictionary<string, Type> types, Dictionary<string, object> instances)
{

if(types == null || types.Count == 0)
return;

foreach(var type in types)
engine.AddHostType(type.Key, type.Value);

if(instances == null || instances.Count == 0)
return;

foreach(var item in instances)
engine.AddHostObject(item.Key, item.Value);

}

// Obtain an Instance of the V8ScriptEngine with the Params Set by User

public static V8ScriptEngine InitEngine(JSEngineInfo engineCfg = default)
{
engineCfg ??= new();

V8ScriptEngine engine;

if(engineCfg.TCPortForDebugging != null)
engine = new(engineCfg.EngineName, engineCfg.RuntimeConstraints, engineCfg.EngineFlags, (int)engineCfg.TCPortForDebugging);

else
engine = new(engineCfg.EngineName, engineCfg.RuntimeConstraints, engineCfg.EngineFlags);

#region ======== Setup Engine Properties ========

engine.AccessContext = engineCfg.EngineProperties.AccessContext;
engine.AllowReflection = engineCfg.EngineProperties.AllowReflection;
engine.CpuProfileSampleInterval = engineCfg.EngineProperties.TimeBetweenCpuSamples;
engine.DefaultAccess = engineCfg.EngineProperties.DefaultScriptAccess;
engine.DisableDynamicBinding = engineCfg.EngineProperties.DisableDynamicBinding;
engine.DisableExtensionMethods = engineCfg.EngineProperties.DisableExtensionMethods;
engine.DisableFloatNarrowing = engineCfg.EngineProperties.UseFloatNarrowing;
engine.DisableListIndexTypeRestriction = engineCfg.EngineProperties.StrictTypingForCollections;
engine.DisableTypeRestriction = engineCfg.EngineProperties.StrictTyping;
engine.DocumentSettings = engineCfg.EngineProperties.ConfigForDocumentsLoader;
engine.EnableAutoHostVariables = engineCfg.EngineProperties.EnableAutoHostVariables;
engine.EnableNullResultWrapping = engineCfg.EngineProperties.WrapNullResults;
engine.EnableRuntimeInterruptPropagation = engineCfg.EngineProperties.InterruptRuntimePropagation;
engine.EnforceAnonymousTypeAccess = engineCfg.EngineProperties.EnforceAnonymousTypeAccess;
engine.ExposeHostObjectStaticMembers = engineCfg.EngineProperties.ExposeStaticMembers;
engine.FormatCode = engineCfg.EngineProperties.CodeFormatting;
engine.MaxRuntimeHeapSize = engineCfg.EngineProperties.MaxRuntimeHeapSize;
engine.MaxRuntimeStackUsage = engineCfg.EngineProperties.MaxRuntimeHeapSize;
engine.RuntimeHeapSizeSampleInterval = engineCfg.EngineProperties.TimeBetweenHeapSamples;
engine.RuntimeHeapSizeViolationPolicy = engineCfg.EngineProperties.RuntimeHeapBehavior;
engine.SuppressExtensionMethodEnumeration = engineCfg.EngineProperties.EnumerateExtensionMethods;
engine.SuppressInstanceMethodEnumeration = engineCfg.EngineProperties.EnumerateInstanceMethods;
engine.UseReflectionBindFallback = engineCfg.EngineProperties.UseReflectionBindFallback;

#endregion

return engine;
}

}