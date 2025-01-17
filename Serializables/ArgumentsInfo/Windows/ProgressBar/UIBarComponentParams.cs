public class UIBarComponentParams : UIComponentParams
{
// ctor

public UIBarComponentParams(bool isText)
{
Style = GetDefaultStyle();
Dimensions = GetDefaultDimensions();

ClassName = isText ? "static" : "msctls_progress32";
}

public static new WindowStyle GetDefaultStyle() => WindowStyle.WS_CHILD | WindowStyle.WS_VISIBLE;

public static SexyRect GetDefaultDimensions(bool isText) => isText ? new(15, 60, 390, 15) : new(15, 100, 390, 20);
}