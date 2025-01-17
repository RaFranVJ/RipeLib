using System.Runtime.Serialization;

/// <summary> Represents a Point in the Cartesian. </summary>

[DataContract]

public struct SexyPoint
{
/** <summary> Gets or Sets the X Coordinate. </summary>
<returns> The X Coordinate. </returns> */

[DataMember(Name="mX") ]

public int X{ get; set; }

/** <summary> Gets or Sets the Y Coordinate. </summary>
<returns> The Y Coordinate. </returns> */

[DataMember(Name="mY") ]

public int Y{ get; set; }

/// <summary> Creates a new Instance of the <c>SexyPoint</c> Class. </summary>

public SexyPoint(int x, int y)
{
X = x;
Y = y;
}

}