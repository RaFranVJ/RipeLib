using RipeLib.Serializables.ArgumentsInfo;

public class UIComponentParams : ParamGroupInfo
{

public WindowStyle Style{ get; set; }

public SexyRect Dimensions{ get; set; }

public ExtendedWindowStyle ExtendedStyle{ get; set; }

public string ClassName{ get; set; }

// ctor

public UIComponentParams()
{
Style = GetDefaultStyle();
Dimensions = GetDefaultDimensions();

ClassName = "static";
}

// WindowStyle

public static WindowStyle GetDefaultStyle()
{
	
return WindowStyle.WS_OVERLAPPED | WindowStyle.WS_CAPTION |
WindowStyle.WS_SYSMENU | WindowStyle.WS_MINIMIZE| WindowStyle.WS_VISIBLE;

}

// Window Dimensions

public static SexyRect GetDefaultDimensions() => new(150, 150, 600, 300);
}