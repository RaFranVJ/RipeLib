using System;
using System.IO;
using Microsoft.ClearScript.V8;
using RipeLib.Serializables.JavaScript;

/// <summary> Allows the execution of JavaScript in C# </summary>

public static class JavaScriptExecutor
{
/** <summary> Executes the Providen <c>V8Script</c> as a simple Expression. </summary>

<param name="engine"> The V8 script engine. </param>
<param name="script"> The Script to Execute. </param>

<returns> The result of the Script Execution, <c>undefined</c> if the JS Function returns no Result. </returns> */
 
public static ScriptResult RunScript(V8ScriptEngine engine, V8Script script)
{
engine ??= JSHelper.InitEngine();

ScriptResult result = new();
result.StartTimer();

engine.Execute(script); // Load Script Funcs and Const
var jsValue = engine.Evaluate(script);

result.StopTimer();
result.SetResult(jsValue);

return result;
}

/** <summary> Executes the Providen <c>V8Script</c> by Invoking a Specified Method. </summary>

<param name="engine"> The V8 script engine. </param>
<param name="script"> The script to execute. </param>
<param name="methodName"> The name of the mMthod to Invoke. </param>
<param name="args"> The arguments to Pass to the Method. </param>

<returns> The result of the method invocation, <c>undefined</c> if Method returns no Result </returns> */

public static ScriptResult RunScript(V8ScriptEngine engine, V8Script script, string methodName, params object[] args)
{
engine ??= JSHelper.InitEngine();

ScriptResult result = new();
result.StartTimer();

engine.Execute(script); // Load Script Funcs and Const
var jsValue = engine.Invoke(methodName, args);

result.StopTimer();
result.SetResult(jsValue);

return result;
}

/** <summary> Executes a JavaScript script from a file. </summary>

<param name="engine"> The V8 script engine.</param>
<param name="filePath"> The path to the JavaScript file.</param>
    
<returns> The result of the script execution. </returns> */

public static ScriptResult RunScriptFromFile(V8ScriptEngine engine, string filePath)
{
engine ??= JSHelper.InitEngine();

string fileName = Path.GetFileNameWithoutExtension(filePath);
V8Script script = engine.Compile(fileName, File.ReadAllText(filePath) );

return RunScript(engine, script);
}

/** <summary> Executes a JavaScript script from a file by invoking a specified method. </summary>

<param name="engine">The V8 script engine.</param>
<param name="filePath">The path to the JavaScript file.</param>
<param name="methodName">The name of the method to invoke.</param>
<param name="args">The arguments to pass to the method.</param>

<returns>The result of the method invocation.</returns> */

public static ScriptResult RunScriptFromFile(V8ScriptEngine engine, string filePath, string methodName, params object[] args)
{
engine ??= JSHelper.InitEngine();

string fileName = Path.GetFileNameWithoutExtension(filePath);
V8Script script = engine.Compile(fileName, File.ReadAllText(filePath) );

return RunScript(engine, script, methodName, args);
}

/** <summary> Executes a JavaScript script from a string. </summary>

<param name="engine">The V8 script engine.</param>
<param name="scriptContent">The JavaScript code as a string.</param>

<returns>The result of the script execution.</returns> */

public static ScriptResult RunScriptFromString(V8ScriptEngine engine, string code)
{
engine ??= JSHelper.InitEngine();

V8Script script = engine.Compile(code);

return RunScript(engine, script);
}

/** <summary> Executes a JavaScript script from a string by invoking a specified method. </summary>

<param name="engine">The V8 script engine.</param>
<param name="scriptContent">The JavaScript code as a string.</param>
<param name="methodName">The name of the method to invoke.</param>
<param name="args">The arguments to pass to the method.</param>

<returns> The result of the method invocation. </returns> */

public static ScriptResult RunScriptFromString(V8ScriptEngine engine, string code, string methodName, params object[] args)
{
engine ??= JSHelper.InitEngine();

V8Script script = engine.Compile(code);

return RunScript(engine, script, methodName, args);
}

}