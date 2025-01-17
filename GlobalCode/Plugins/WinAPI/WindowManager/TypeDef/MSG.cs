using System;
using System.Runtime.InteropServices;

/// <summary> Represents the <c>MSG</c> structure used in the Windows API. </summary>

[StructLayout(LayoutKind.Sequential) ]

public struct MSG
{
        public IntPtr hwnd;
        public uint message;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public POINT pt;
}