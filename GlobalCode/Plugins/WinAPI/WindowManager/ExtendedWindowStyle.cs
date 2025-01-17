using System;

/// <summary> Defines extended window styles for creating and managing windows in the Windows API. </summary>

[Flags]

public enum ExtendedWindowStyle : int
{
/// <summary> The window has generic "left-aligned" properties. </summary>
WS_EX_LEFT = 0,

/// <summary> The window has a double border; the WS_BORDER style must also be specified. </summary>
WS_EX_DLGMODALFRAME = 1,

/// <summary> Prevents the window from becoming a maximized window. </summary>
WS_EX_NOPARENTNOTIFY = 4,

/// <summary> The window is a topmost window. </summary>
WS_EX_TOPMOST = 8,

/// <summary> The window accepts drag-drop files. </summary>
WS_EX_ACCEPTFILES = 16,

/// <summary> Creates a window that does not pass its window region to child windows. </summary>
WS_EX_TRANSPARENT = 32,

/// <summary> The window is intended to be used as a floating toolbar. </summary>
WS_EX_TOOLWINDOW = 128,
 
/// <summary> The window has a three-dimensional border style. </summary>
WS_EX_WINDOWEDGE = 256,

/// <summary> The window has a border with a sunken edge. </summary>
WS_EX_CLIENTEDGE = 512,

/// <summary> The window includes a question mark in the title bar. </summary>
WS_EX_CONTEXTHELP = 1024,

/// <summary> Forces a top-level window onto the taskbar when visible. </summary>
WS_EX_APPWINDOW = 262144,
 
/// <summary> The window is a layered window. </summary>
WS_EX_LAYERED = 524288, 
 
/// <summary> The window text is displayed using right-to-left reading order. </summary>
WS_EX_LAYOUTRTL = 4194304,

/// <summary> The window paints all descendants in bottom-to-top order. </summary>
WS_EX_COMPOSITED = 33554432,

WS_EX_NOACTIVATE = 134217728
}