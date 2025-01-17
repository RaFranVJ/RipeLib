using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

/// <summary> Stores additional Info related to Tasks </summary>

public class TaskInfo
{
/// <summary> Gets or Sets the Task Name </summary>

public string TaskName{ get; set; }

/// <summary> Gets or Sets the Task itself </summary>

public Task TaskContext{ get; protected set; }

/// <summary> Gets or Sets the Elapsed Time since Tasks started. </summary>

protected Stopwatch Timer;

/// <summary> Gets or Sets the Cancellation Source for Tasks. </summary>

private readonly CancellationTokenSource CancelTokenSrc;

/// <summary> Creates a new Instance of the <c>TaskInfo</c> </summary>

public TaskInfo()
{
Timer = new();
CancelTokenSrc = new();

TaskName = "My Task Template";
}

/// <summary> Creates a new Instance of the <c>TaskInfo</c> </summary>

public TaskInfo(string name)
{
Timer = new();
CancelTokenSrc = new();

TaskName = name;
}

/// <summary> Creates a new Instance of the <c>TaskInfo</c> </summary>

public TaskInfo(string name, Action taskAction)
{
Timer = new();
CancelTokenSrc = new();

TaskName = name;
TaskContext = new(taskAction, CancelTokenSrc.Token);
}

/// <summary> Starts the Task contained on this Instance </summary>

public void StartTask()
{

if(TaskContext.Status != TaskStatus.Created)
return;

Timer.Start();

TaskContext.Start();
}

/// <summary> Cancels the Task contained on this Instance </summary>
	
public void CancelTask()
{

if(TaskContext.Status == TaskStatus.Running || TaskContext.Status == TaskStatus.WaitingToRun)
{
Timer.Stop();

CancelTokenSrc.Cancel();
}

}

/// <summary> Gets the Elapsed Time of this Task </summary>

public TimeSpan GetElapsedTime() => Timer.Elapsed;

/// <summary> Stops the Timer of this Instance </summary>

public void StopTimer() => Timer.Stop();
}