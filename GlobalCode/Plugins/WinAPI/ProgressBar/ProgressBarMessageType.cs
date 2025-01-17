/// <summary> Defines the Progress Bar Messages (PBM) used in Windows API. </summary>

public enum ProgressBarMessageType : uint
{
/// <summary> Sets the range of a progress bar to 16-bit values. </summary>
PBM_SETRANGE = 1025, 

/// <summary> Sets the current position of the progress bar. </summary>
PBM_SETPOS = 1026, 

/// <summary> Advances the current position by a specified increment. </summary>
PBM_DELTAPOS = 1027, 

/// <summary> Sets the step increment value for the progress bar. </summary>
PBM_SETSTEP = 1028, 

/// <summary> Advances the position by the current step increment. </summary>
PBM_STEPIT = 1029, 

/// <summary> Retrieves the range limits of the progress bar. </summary>
PBM_GETRANGE = 1030, 

/// <summary> Retrieves the current position of the progress bar. </summary>
PBM_GETPOS = 1031, 

/// <summary> Sets the range of a progress bar to 32-bit values. </summary>
PBM_SETRANGE32 = 1032, 

/// <summary> Retrieves the current range and step increment of the progress bar. </summary>
PBM_GETRANGE32 = 1033, 

/// <summary> Enables or disables marquee mode. </summary>
PBM_SETMARQUEE = 1034, 

/// <summary> Sets the progress bar's color. </summary>
PBM_SETBKCOLOR = 1035, 

/// <summary> Sets the progress bar's foreground color. </summary>
PBM_SETCOLOR = 1036, 

/// <summary> Retrieves the progress bar's background color. </summary>
PBM_GETBKCOLOR = 1037, 

/// <summary> Retrieves the progress bar's foreground color. </summary>
PBM_GETCOLOR = 1038, 

/// <summary> Sets the progress bar's state (normal, error, or paused). </summary>
PBM_SETSTATE = 1039, 

/// <summary> Retrieves the progress bar's state. </summary>
PBM_GETSTATE = 1040 
}
