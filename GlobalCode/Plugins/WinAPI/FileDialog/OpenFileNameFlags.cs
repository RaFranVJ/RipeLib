using System;

[Flags]

/// <summary> Flags that Specified how the OpenFileName (OFN) Dialog should be Displayed. </summary>

public enum OpenFileNameFlags : uint
{
/// <summary> Read Only check box will be selected initially when the dialog box is created </summary>
OFN_READONLY = 1,

/// <summary> Prompt before overwriting an existing file. </summary>
OFN_OVERWRITEPROMPT = 2,

/// <summary> Hide the "Read-only" checkbox in the dialog. </summary>
OFN_HIDEREADONLY = 4,

/// <summary> Don't change the current directory. </summary>
OFN_NOCHANGEDIR = 8,

/// <summary> Show the "Help" button in the dialog. </summary>
OFN_SHOWHELP = 16,

/// <summary> Enable hook for customizing the dialog. </summary>
OFN_ENABLEHOOK = 32,

/// <summary> Enable template for customizing the dialog box. </summary>
OFN_ENABLETEMPLATE = 64,

/// <summary> <c>hInstance</c> identifies a data block that contains a preloaded dialog box template </summary>
OFN_ENABLETEMPLATEHANDLE = 128,

/// <summary> Allow multiple file selections. </summary>
OFN_ALLOWMULTISELECT = 512,

/// <summary> User typed a file name extension that differs from the one specified by <c>lpstrDefExt</c> </summary>
OFN_EXTENSIONDIFFERENT = 1024,

/// <summary> Path must exist. </summary>
OFN_PATHMUSTEXIST = 2048,

/// <summary> File must exist. </summary>
OFN_FILEMUSTEXIST = 4096,

/// <summary> File will be Created if it does not Exists. </summary>
OFN_CREATEPROMPT = 8192,

/// <summary> Specifies ta call to the OpenFile function failed because of a network sharing violation </summary>
OFN_SHAREAWARE = 16384,

/// <summary> Returned file does not have the Read Only check box selected </summary>
OFN_NOREADONLYRETURN = 32768,

/// <summary> Application-defined data to be passed to a hook procedure. </summary>
OFN_NOVALIDATE = 65536,

/// <summary> Disable the "Network" button. </summary>
OFN_NONETWORKBUTTON = 131072,

/// <summary> Causes the dialog box to use short file names </summary>
OFN_NOLONGNAMES = 262144,

/// <summary> Use the modern explorer dialog interface. </summary>
OFN_EXPLORER = 524288,

/// <summary> Do not resolve shortcuts (no symlinks). </summary>
OFN_NODEREFERENCELINKS = 1048576,

/// <summary> Allow long file names (longer than 260 characters). </summary>
OFN_LONGNAMES = 2097152,

/// <summary> Dialog Box will Send Notification Messages. </summary>
OFN_ENABLEINCLUDENOTIFY = 4194304,

/// <summary> Enables the Explorer-style dialog box to be resized using either the mouse or the keyboard. </summary>
OFN_ENABLESIZING = 8388608,

/// <summary> File won't be Added to Recent List. </summary> 
OFN_DONTADDTORECENT = 33554432,

/// <summary> Forces the showing of system and hidden files. </summary> 
OFN_FORCESHOWHIDDEN = 268435456
}