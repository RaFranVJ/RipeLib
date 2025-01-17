/// <summary>Defines the Window Messages (WM) used in the WinApi.</summary>

public enum WindowMessageType : uint
{
/// <summary> Indicates no action. </summary>
WM_NULL,

/// <summary> Sent when a window is being created. </summary>
WM_CREATE,

/// <summary> Sent when a window is being destroyed. </summary>
WM_DESTROY,

/// <summary> Sent when a window is moved. </summary>
WM_MOVE,

/// <summary> Sent when a window is resized. </summary>
WM_SIZE,

/// <summary> Sent when a window is activated or deactivated. </summary>
WM_ACTIVATE,

/// <summary> Sent when a window gains focus. </summary>
WM_SETFOCUS,

/// <summary> Sent when a window loses focus. </summary>
WM_KILLFOCUS,

WM_SETTEXT = 12,

/// <summary> Sent when the system requests that a window should be painted. </summary>
WM_PAINT = 15,

/// <summary> Sent when a window is being closed. </summary>
WM_CLOSE = 16,

/// <summary> Posted when an application is to be terminated. </summary>
WM_QUIT = 18,

/// <summary> Sent when a non-system key is pressed. </summary>
WM_KEYDOWN = 256,

/// <summary> Sent when a non-system key is released. </summary>
WM_KEYUP = 257,

/// <summary> Sent when a character is entered. </summary>
WM_CHAR = 258,

/// <summary> Sent when the mouse moves. </summary>
WM_MOUSEMOVE = 512,

/// <summary> Sent when the left mouse button is pressed. </summary>
WM_LBUTTONDOWN = 513,

/// <summary> Sent when the left mouse button is released. </summary>
WM_LBUTTONUP = 514
}