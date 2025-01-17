using System.Runtime.Serialization;

/// <summary> Represents a Rectangle. </summary>

[DataContract]

public struct SexyRect
{
/** <summary> Gets or Sets the X Coordinate of the Rectangle. </summary>
<returns> The X Coordinate. </returns> */

[DataMember(Name="mX") ]

public int X{ get; set; }

/** <summary> Gets or Sets the Y Coordinate of the Rectangle. </summary>
<returns> The Y Coordinate. </returns> */

[DataMember(Name="mY") ]

public int Y{ get; set; }

/** <summary> Gets or Sets the Rectangle Width. </summary>
<returns> The Rectangle Width. </returns> */

[DataMember(Name="mWidth") ]

public int Width{ get; set; }

/** <summary> Gets or Sets the Rectangle Height. </summary>
<returns> The Rectangle Height. </returns> */

[DataMember(Name="mHeight") ]

public int Height{ get; set; }

/// <summary> Creates a new Instance of the <c>SexyRect</c> Class. </summary>

public SexyRect(int x, int y, int w, int h)
{
X = x;
Y = y;

Width = w;
Height = h;
}

}