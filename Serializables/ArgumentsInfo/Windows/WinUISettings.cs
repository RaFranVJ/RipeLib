using RipeLib.Serializables.ArgumentsInfo;

public class WinUISettings : ParamGroupInfo
{

public UIComponentParams WindowUIConfig{ get; set; }

public ProgressBarConfig ProgressBarUIConfig{ get; set; }

public WinUISettings()
{
WindowUIConfig = new();
ProgressBarUIConfig = new();
}

}