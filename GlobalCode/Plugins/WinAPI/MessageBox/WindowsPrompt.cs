using System;
using System.Runtime.InteropServices;

/// <summary> Allows the Display of MessageBoxs in Windows Apps (including Consoles too) </summary>

public class WindowsPrompt
{

private IntPtr msgBox;

// Imported from Windows API

[DllImport("user32.dll", SetLastError = true) ]

private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

/** <summary> Displays a Windows MessageBox with the specified title, body, and flags. </summary>

<param name="title">The title of the MessageBox.</param>
<param name="body">The body of the MessageBox.</param>
<param name="flags">The flags determining the style and behavior of the MessageBox.</param>
<param name="hWnd">The handle of the parent window (optional).</param> */

public WindowsPrompt(string title, string body, MessageBoxFlags flags, IntPtr parent = default)
{

if(!PlatformHelper.IsWindows)
return;

title = string.IsNullOrEmpty(title) ? "<Missing Title>" : title;
body = string.IsNullOrEmpty(body) ? "<Missing Body>" : body;

msgBox = MessageBox(parent, body, title, (uint)flags);
}

public void Destroy()
{

if(msgBox == IntPtr.Zero)
return;

WindowGUI.Destroy(msgBox);

msgBox = default;
}

}