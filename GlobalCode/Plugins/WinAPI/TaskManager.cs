using System;
using System.Runtime.InteropServices;

/** <summary> Initializes Handling Functions for any Task that is being Executed in 
the Runtime Enviroment of the Device. </summary> */

public static class TaskManager
{
// Imported from Windows API

[DllImport("kernel32.dll") ]

public static extern bool SetProcessWorkingSetSize(IntPtr hProcess, uint dwMinimumWorkingSetSize, uint dwMaximumWorkingSetSize);

[DllImport("kernel32.dll") ]

public static extern IntPtr GetCurrentProcess();

/** <summary> Releases all the Resources that a Task Occupies. </summary>
<param name = "targetHandle"> The Handle of the Process to be Released. </param> */

public static void ReleaseTaskResources(IntPtr targetHandle = default)
{
targetHandle = targetHandle == default ? GetCurrentProcess() : targetHandle;

SetProcessWorkingSetSize(targetHandle, 0, 0);
}

}