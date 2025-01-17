using System;
using RipeLib.Serializables.ArgumentsInfo;

public class ProgressBarTextConfig : ParamGroupInfo
{

public string Title{ get; set; }

public string Body{ get; set; }

public ProgressFormat DisplayFormat{ get; set; }

public IntPtr ParentWindowPtr{ get; set; }

public ProgressBarTextConfig()
{
Title = GetDefaultTitle();
Body = GetDefaultMsg();

DisplayFormat = ProgressFormat.Full;
}

public ProgressBarTextConfig(string title, string body)
{
Title = title;
Body = body;

DisplayFormat = ProgressFormat.Full;
}

public ProgressBarTextConfig(string title, string body, IntPtr parent)
{
Title = title;
Body = body;

DisplayFormat = ProgressFormat.Full;
ParentWindowPtr = parent;
}

public ProgressBarTextConfig(string title, string body, ProgressFormat displayFmt, IntPtr parent)
{
Title = title;
Body = body;

DisplayFormat = displayFmt;
ParentWindowPtr = parent;
}

// Create New

public static ProgressBarTextConfig CreateNew(bool reportProgress, string title, string body,
ProgressFormat displayFmt = ProgressFormat.Full, IntPtr p = default)
{

if(!reportProgress)
return null;

return new(title, body, displayFmt, p);
}

public static string GetDefaultTitle() => "Task in Progress";

public static string GetDefaultMsg() => "Processing request, please wait";
}