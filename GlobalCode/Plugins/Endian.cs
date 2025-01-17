/// <summary> The Endian Encodings used when parsing Binary Data. </summary>

public enum Endian
{
/// <summary> Data won't use Endian Encoding. </summary>
Default,

/// <summary> Data should be Parsed with Little Endian. </summary>
LittleEndian,

/// <summary> Data should be Parsed with Big Endian. </summary>
BigEndian
}
