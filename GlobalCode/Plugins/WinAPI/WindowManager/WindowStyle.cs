using System;

/// <summary> Represent a Style used for Creating a WindowGUI in the Windows API. </summary>

[Flags]

public enum WindowStyle : uint
{
/// <summary> Standard overlapped window (default style). </summary>
WS_OVERLAPPED = 0,

/// <summary> Enables the user to select the control with the TAB key. </summary>
WS_TABSTOP = 65536,

/// <summary> Groups related child controls in a dialog box. </summary>
WS_GROUP = 131072,

/// <summary> Adds a thick border for resizing the window. </summary>
WS_THICKFRAME = 262144,

/// <summary> Includes a system menu in the title bar. </summary>
WS_SYSMENU = 524288,

/// <summary> Includes a horizontal scroll bar. </summary>
WS_HSCROLL = 1048576,

/// <summary> Includes a vertical scroll bar. </summary>
WS_VSCROLL = 2097152,

/// <summary> Adds a dialog frame to the window. </summary>
WS_DLGFRAME = 4194304,

/// <summary> Adds a thin border around the window. </summary>
WS_BORDER = 8388608,

/// <summary> Combines WS_BORDER and WS_DLGFRAME. Adds a title bar. </summary>
WS_CAPTION = 12582912,

/// <summary> The window is initially maximized. </summary>
WS_MAXIMIZE = 16777216,

/// <summary> Clips child windows so they cannot draw outside their parent. </summary>
WS_CLIPCHILDREN = 33554432,

/// <summary> Clips child windows that overlap each other. </summary>
WS_CLIPSIBLINGS = 67108864,

/// <summary> The window is initially disabled, preventing interaction. </summary>
WS_DISABLED = 134217728,

/// <summary> The window is initially visible. </summary>
WS_VISIBLE = 268435456,

/// <summary> The window is initially minimized. </summary>
WS_MINIMIZE = 536870912,

/// <summary> Creates a child window; cannot have a menu bar. </summary>
WS_CHILD = 1073741824,

/// <summary> Creates a pop-up window. </summary>
WS_POPUP = 2147483648
}