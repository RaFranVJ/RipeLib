namespace RipeLib.Serializables.ArgumentsInfo
{
/// <summary> Stores Info about a Class that allows Data Processing in Blocks. </summary>

public class DataBlockInfo : ParamGroupInfo
{
/// <summary> The allowed Buffer Size. </summary>

protected Limit<int> BufferSizeRange{ get; set; } = new()
{
MinValue = (int)Constants.ONE_KILOBYTE / 2,
MaxValue = (int)Constants.ONE_KILOBYTE * 4
};

/** <summary> Gets or Sets the Size in Bytes of a Buffer when Processing Data. </summary>
<returns> The Buffer Size. </returns> */

public int BufferSizeForIOTasks{ get; set; }

/// <summary> Creates a new Instance of the <c>DataBlockInfo</c>. </summary>

public DataBlockInfo()
{
BufferSizeForIOTasks = BufferSizeRange.MinValue;
}

///<summary> Checks each nullable Field of the <c>DataBlockInfo</c> Instance and Validates it, in case it's <c>null</c>. </summary>

public override void CheckForNullFields() => BufferSizeForIOTasks = BufferSizeRange.CheckParamRange(BufferSizeForIOTasks);
}

}