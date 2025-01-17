using System;
using System.Runtime.InteropServices;
using System.Text;

/** <summary> Represents the <c>OPENFILENAME</c> structure used in the Windows API. </summary>

<remarks> This allows to Select Files through Windows File Prompt. </remarks> **/

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1) ]

public struct OPENFILENAME
{
/// <summary> The size of the structure in bytes.  </summary>
public uint dwLength = (uint)Marshal.SizeOf(typeof(OPENFILENAME) );

/// <summary> Handle to the owner window for the dialog box (Null for Console Apps). </summary>
public IntPtr hwndOwner;

/// <summary> Instance handle for the application (not commonly used). </summary>
public IntPtr hInstance;

/// <summary> The filter for the file types to display. </summary>
public string lpstrFilter;

/// <summary> Buffer to hold the selected file path. </summary>
public StringBuilder lpstrFile = new(260);

/// <summary> The maximum size, in characters, of the lpstrFile buffer. </summary>
public int nMaxFile = 260;

/// <summary> Buffer for the file title (file name without path). </summary>
public StringBuilder lpstrFileTitle = new(260);

/// <summary> The maximum size, in characters, of the lpstrFileTitle buffer. </summary>
public int nMaxFileTitle = 260;

/// <summary> The initial directory for the dialog </summary>
public string lpstrInitialDir;

/// <summary> The title of the dialog box. </summary>
public string lpstrTitle;

/// <summary> Flags to control the dialog box's behavior. </summary>
public uint Flags;

/// <summary> Offset to the file name in lpstrFile. </summary>
public ushort nFileOffset;

/// <summary> Offset to the file extension in lpstrFile. </summary>
public ushort nFileExtension;

/// <summary> Default file extension (without the dot). </summary>
public string lpstrDefExt;

/// <summary> Application-defined data to be passed to a hook procedure. </summary>
public IntPtr lCustData;

/// <summary> Pointer to a hook procedure for customizing the dialog (if OFN_ENABLEHOOK flag is set).  </summary>
public IntPtr lpfnHook;

/// <summary> Template for a custom dialog box (if OFN_ENABLETEMPLATE flag is set). </summary>
public string lpTemplateName;

/// <summary> Reserved; should be set to null. </summary>
public IntPtr pvReserved;

/// <summary> Used for extended flags; not commonly used. </summary>
public int FlagsEx;

// ctor

public OPENFILENAME(IntPtr owner, string filter, string title)
{
hwndOwner = owner;
lpstrFilter = filter;

lpstrTitle = title;
}

}