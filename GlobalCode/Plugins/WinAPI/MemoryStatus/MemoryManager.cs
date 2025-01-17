using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using RipeLib.Modules;

/// <summary> Initializes Handling Functions for the Memory Consumed by the Process of this Program. </summary>

public static class MemoryManager
{
[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true) ]

private static extern bool GlobalMemoryStatusEx( [In, Out] MEMORYSTATUSEX lpBuffer);

/// <summary> Releases the memory Consumed by a Process that its being Executed by this Program. </summary>

public static void ReleaseMemory(Process targetProcess)
{
GC.Collect();
GC.WaitForPendingFinalizers();

if(!PlatformHelper.IsWindows)
return;

TaskManager.ReleaseTaskResources(targetProcess.Handle);
}

// Get Memory Available

public static ulong GetAvailableRAM()
{
MEMORYSTATUSEX memStatus = new();

if(PlatformHelper.IsWindows && GlobalMemoryStatusEx(memStatus) )
return memStatus.ullAvailPhys;

return 0;
}

// Generic Dialog

private static void CheckMemoryNeeded(ulong available, ulong required, string path = null)
{

if(available >= required)
return;

string displaySizeA = InputHelper.GetDisplaySize( (long)available);
string displaySizeB = InputHelper.GetDisplaySize( (long)required);

string streamType = string.IsNullOrEmpty(path) ? "Stream" : $"File ({Path.GetFileName(path)})";
string sizeDiff = $"Memory Required: {displaySizeB} - Memory Available: {displaySizeA}";

throw new OutOfMemoryException($"This {streamType} is too big, you'll need more RAM for Processing it. ({sizeDiff})");
}

// Check if File exceeds memory Length

public static void CheckFileSize(string filePath)
{
ulong fileSize = (ulong)FileManager.GetFileSize(filePath);
ulong availableMemory = GetAvailableRAM();

CheckMemoryNeeded(availableMemory == 0 ? fileSize : availableMemory, fileSize, filePath);
}

// Check if Stream exceeds memory Length

public static void CheckStreamSize(Stream targetStream)
{
ulong availableMemory = GetAvailableRAM();
ulong streamSize = 0;

try
{
streamSize = (ulong)targetStream.Length; // May throw Exception
}

catch(NotSupportedException)
{
}

finally
{
CheckMemoryNeeded(availableMemory == 0 ? streamSize : availableMemory, streamSize);
}

}

// Get Buffer Size without Exceding Memory

public static int GetBufferSize(Stream targetStream)
{
long availableMemory = (long)GetAvailableRAM();

if(availableMemory == 0)
return (int)(Constants.ONE_MEGABYTE * 64);

long streamSize;

try
{
streamSize = targetStream.Length; // May throw Exception
}

catch(NotSupportedException)
{
streamSize = availableMemory / 2;
}

long sizeFactor = Math.Min(availableMemory, streamSize);

if(sizeFactor >= Constants.ONE_GIGABYTE)
return (int)(sizeFactor / Constants.ONE_MEGABYTE);

else if(streamSize >= Constants.ONE_MEGABYTE)
return (int)(streamSize / Constants.ONE_KILOBYTE);

return (int)sizeFactor;
}

}