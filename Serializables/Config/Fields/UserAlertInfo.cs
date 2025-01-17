using System;

namespace RipeLib.Serializables.Config.Fields
{
/// <summary> Groups Info related to User Alerts displayed on Task Completion. </summary>

public class UserAlertInfo : ConfigField
{
/** <summary> Gets or Sets a Boolean that Determines if Status Msgs should be Displayed on Script Execution. </summary>
<returns> <b>true</b> if Enabled; otherwise, <b>false</b>. </returns> */

public bool DisplayStatusMsgs{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if a Sound Alert should be Played when a Task is Successfully Completed. </summary>
<returns> <b>true</b> if Enabled; otherwise, <b>false</b>. </returns> */

public bool NotifyTaskCompletion{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if a Sound Alert should be Played when a Warning is Displayed. </summary>
<returns> <b>true</b> if Enabled; otherwise, <b>false</b>. </returns> */

public bool NotifyWarnings{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if a Sound Alert should be Played when an Exception is Raised. </summary>
<returns> <b>true</b> if Enabled; otherwise, <b>false</b>. </returns> */

public bool NotifyErrors{ get; set; }

/// <summary> Creates a new Instance of the <c>UserAlertInfo</c>. </summary>

public UserAlertInfo()
{
DisplayStatusMsgs = true;
NotifyTaskCompletion = true;

NotifyErrors = true;
}

}

}