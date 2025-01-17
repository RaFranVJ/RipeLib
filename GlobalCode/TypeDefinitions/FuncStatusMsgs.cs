/// <summary> Stores IDS that Indicate the Status of a Function. </summary>

public class FuncStatusMsgs
{
/** <summary> Gets or Sets the ID of the Message to Display when Processing an Action. </summary>
<returns> The ID for the Message indicating the Task is on Process. </returns> */

public string ExecutionOnProcess{ get; set; }

/** <summary> Gets or Sets the ID of the Message to Display when an Action is Completed with Success. </summary>
<returns> The ID for the Message indicating the Task was Completed Successfully. </returns> */

public string ExecutionSuccessful{ get; set; }

/** <summary> Gets or Sets the ID of the Message to Display when an Action was Failed to Complete. </summary>
<returns> The ID for the Message indicating the Task was Failed. </returns> */

public string ExecutionFaulted{ get; set; }

/** <summary> Gets or Sets a Boolean that Determines if Strings should be Printed directly instead of using Localization. </summary>
<returns> <true> if Direct Strings should be Used; false, otherwise. </returns> */

public bool UseDirectStrings{ get; set; }

/// <summary> Creates a new Instance of the <c>FuncStatusMsgs</c>. </summary>

public FuncStatusMsgs()
{
ExecutionOnProcess = "$ACTION_{ActionType}";
ExecutionSuccessful = "$RESULT_{ActionType}_SUCCESSFUL";

ExecutionFaulted = "$RESULT_{ActionType}_FAILED";
}

/** <summary> Creates a new Instance of the <c>FuncStatusMsgs</c> with the Specific IDS. </summary>

<param name = "onProcessID"> The ID of the Message to Display when the Function is on Process. </param>
<param name = "successID"> The ID of the Message to Display when the Function is Successfully Completed. </param>
<param name = "failID"> The ID of the Message to Display when the Function is Faulted. </param> */

public FuncStatusMsgs(string onProcessID, string successID, string failID)
{
ExecutionOnProcess = onProcessID;
ExecutionSuccessful = successID;

ExecutionFaulted = failID;
}

}