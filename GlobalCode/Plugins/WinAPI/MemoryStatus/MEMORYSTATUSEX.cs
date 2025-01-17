using System.Runtime.InteropServices;

/** <summary> Represents the <c>MEMORYSTATUSEX</c> structure used in the Windows API. </summary>

<remarks> This Class provides Info about System's Memory Usage and Availability. </remarks> **/

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto) ]

public struct MEMORYSTATUSEX
{
/// <summary> The size of the structure in bytes.  </summary>
public uint dwLength;

/// <summary> Approximate percentage of Physical Memory currently in use. </summary>
public uint dwMemoryLoad;

/// <summary> Total amount of physical memory in the system (expressed in bytes). </summary>
public ulong ullTotalPhys;

/// <summary> Amount of physical memory currently available (expressed in bytes). </summary>
public ulong ullAvailPhys;

/// <summary> Amount of memory that can be committed to all processes (expressed in bytes). </summary>
public ulong ullTotalPageFile;

/// <summary> Amount of memory currently available for commitment (expressed in bytes). </summary>
public ulong ullAvailPageFile;

/// <summary> Total amount of virtual address space the calling process can use (expressed in Bytes). </summary>
public ulong ullTotalVirtual;

/// <summary> The amount of virtual address space currently available, in bytes. </summary>
public ulong ullAvailVirtual;

/// <summary> Reserved for further use. Always set to zero. </summary>
public ulong ullAvailExtendedVirtual;

public MEMORYSTATUSEX()
{
dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX) );
}

}