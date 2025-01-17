public class ProgressBarConfig : ProgressBarTextConfig
{
public UIComponentParams MainWindowParams{ get; set; }

public UIBarComponentParams ProgressBarParams{ get; set; }

public UIBarComponentParams ProgressTextParams{ get; set; }

public ProgressBarConfig()
{
MainWindowParams = new();
ProgressBarParams = new(false);

ProgressTextParams = new(true);
}

}