using System;

/// <summary> Flags that Specify how the MessageBox (MB) should be Displayed. </summary>

[Flags]

public enum MessageBoxFlags : uint
{
/// <summary> The message box contains one button: <b>OK</b> </summary>
MB_OK,

/// <summary> The message box contains two buttons: <b>OK</b> and <b>CANCEL</b> </summary>
MB_OKCANCEL,

/// <summary> The message box contains three buttons: <b>ABORT</b>, <b>RETRY</b> and <b>IGNORE</b> </summary>
MB_ABORTRETRYIGNORE,

/// <summary> The message box contains three buttons: <b>YES</b>, <b>NO</b> and <b>CANCEL</b> </summary>
MB_YESNOCANCEL,

/// <summary> The message box contains two buttons: <b>YES</b> and <b>NO</b> </summary>
MB_YESNO,

/// <summary> The message box contains two buttons: <b>RETRY</b> and <b>CANCEL</b> </summary>
MB_RETRYCANCEL,

/// <summary> The message box contains three buttons: <b>CANCEL</b>, <b>TRY AGAIN</b> and <b>CONTINUE</b> </summary>
MB_CANCELTRYCONTINUE,

/// <summary> A stop-sign icon appears in the message box </summary>
MB_ICONERROR = 16,

/// <summary> A question-mark icon appears in the message box </summary>
MB_ICONQUESTION = 32,

/// <summary> An exclamation-point icon appears in the message box </summary>
MB_ICONWARNING = 48,

/// <summary> An icon consisting of a lowercase letter i in a circle appears in the message box </summary>
MB_ICONINFO = 64,

/// <summary> The second button is the default button </summary>
MB_DEFBUTTON2 = 256,

/// <summary> The third button is the default button </summary>
MB_DEFBUTTON3 = 512,

/// <summary> The fourth button is the default button </summary>
MB_DEFBUTTON4 = 768,

/** <summary> User must respond to the message box before continuing work </summary>
<remarks> Message box has the <b>WS_EX_TOPMOST</b> style </remarks> **/

MB_SYSTEMMODAL = 4096,

/** <summary> User must respond to the message box before continuing work </summary>
<remarks> All the top-level windows belonging to the current thread are disabled if the <c>hWnd</c> is <c>null</c> </remarks> **/

MB_TASKMODAL = 8192,

/// <summary> Adds a <b>HELP</b> button to the message box </summary>
MB_HELP = 16384,

/// <summary> The message box becomes the foreground window </summary>
MB_SETFOREGROUND = 65536,

/// <summary> Same as desktop of the interactive window station </summary>
MB_DEFAULT_DESKTOP_ONLY = 131072,

/// <summary> The message box is created with the <b>WS_EX_TOPMOST</b> window style </summary>
MB_TOPMOST = 262144,

/// <summary> The text is right-justified </summary>
MB_RIGHT = 524288,

/// <summary> Displays message and caption text using right-to-left reading order </summary>
MB_RTLREADING = 1048576,

/// <summary> The caller is a service notifying the user of an event </summary>
MB_SERVICE_NOTIFICATION = 2097152
}