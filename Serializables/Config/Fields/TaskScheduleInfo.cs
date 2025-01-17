namespace RipeLib.Serializables.Config.Fields
{
/// <summary> Groups Info related to how to Schedule Tasks. </summary>

public class TaskScheduleInfo : ConfigField
{
/** <summary> Gets or Sets a Boolean that Determines if Multiple Tasks are Allowed. </summary>
<returns> <b>true</b> if Multi-Tasking is allowed; otherwise, <b>false</b>. </returns> */

public bool AllowMultiTasking{ get; set; }

/** <summary> Gets or Sets the Maximum amount of Tasks allowed in the List. </summary>

<remarks> This Field is Set to <b>1</b> by Default, since Multi-Tasking is Disabled </remarks>

<returns> The Tasks Count. </returns> */

public int MaxTasksCount{ get; set; }

/// <summary> Creates a new Instance of the <c>TaskScheduleInfo</c>. </summary>

public TaskScheduleInfo()
{
MaxTasksCount = 1;
}

}

}