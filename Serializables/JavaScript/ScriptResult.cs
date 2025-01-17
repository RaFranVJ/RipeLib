using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RipeLib.Serializables.JavaScript
{
/// <summary> Represents the Result of a JavaScript Execution. </summary>

public class ScriptResult
{
/** <summary> Gets the Value returned by Script Execution. </summary>
<returns> The Value. </returns> */

public object ValueReturned{ get; protected set; }

/// <summary> The Time elapssed since Execution started. </summary>

protected Stopwatch Timer = new();

/// <summary> A List of Exceptions caught during Script Execution. </summary>

protected List<string> ErrorsCaught = new();

/// <summary> Creates a new <c>ScriptResult</c>. </summary>

public ScriptResult()
{
}

/// <summary> Creates a new <c>ScriptResult</c>. </summary>

public ScriptResult(object result)
{
ValueReturned = result;
}

// Set Resukt

public void SetResult(object result) => ValueReturned = result;

/// <summary> Starts the Timer of this Instance </summary>

public void StartTimer() => Timer.Start();

/// <summary> Gets the Elapsed Time of this Result </summary>

public TimeSpan GetElapsedTime() => Timer.Elapsed;

/// <summary> Stops the Timer of this Instance </summary>

public void StopTimer() => Timer.Stop();

// Add ExceptionName to List

public void RegistException(string error) => ErrorsCaught.Add(error);

/// <summary> Gets the ErrorsCaught during Script Execution </summary>

public List<string> GetErrorsCaught() => ErrorsCaught;
}

}