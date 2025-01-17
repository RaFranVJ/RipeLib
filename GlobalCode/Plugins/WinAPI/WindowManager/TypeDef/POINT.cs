using System.Runtime.InteropServices;

/// <summary> Represents the <c>POINT</c> structure used in the Windows API. </summary>

[StructLayout(LayoutKind.Sequential) ]
   
public struct POINT
{
public int X;

public int Y;
}