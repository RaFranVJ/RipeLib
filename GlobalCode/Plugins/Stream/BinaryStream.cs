using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RipeLib.Modules;
using RipeLib.Modules.Other;

/// <summary> Allows Stream Reading and Writing in a Binary Format. </summary>

public class BinaryStream : IDisposable
{
/// <summary> The Buffers of the <c>BinaryStream</c>. </summary>

protected readonly byte[] buffers;

/// <summary> Specifies how the Stream should be Handled after being Opened. </summary>

public bool LeaveOpened = false;

/** <summary> Checks if the BaseStream is Actually a MemoryStream. </summary>
<returns> <b>true</b> if the BaseStream is a MemoryStream; otherwise, <b>false</b>. </returns> */

public bool IsMemoryStream => BaseStream is MemoryStream;

/** <summary> Gets of Sets the BaseStream of the <c>BinaryStream</c>. </summary>
<returns> The Base Stream. </returns> */

public Stream BaseStream{ get; set; }

/** <summary> Gets or Sets the Length in Bytes of the <c>BinaryStream</c>. </summary>
<returns> The Length of the Stream. </returns> */

public long Length{ get => BaseStream.Length; set => BaseStream.SetLength(value); }

/** <summary> Gets or Sets the Position of the <c>BinaryStream</c>. </summary>
<returns> The Position of the Stream. </returns> */

public long Position{ get => BaseStream.Position; set => BaseStream.Position = value; }

/// <summary> Creates a new Instance of the <c>BinaryStream</c> Class. </summary>

public BinaryStream(bool leaveOpen = false) : this(new MemoryStream(), leaveOpen)
{
}

/** <summary> Creates a new Instance of the <c>BinaryStream</c> Class with the given Stream. </summary>

<param name = "sourceStream"> The Stream where the Instance will be Created from. </param> */

public BinaryStream(Stream sourceStream, bool leaveOpen = false)
{
BaseStream = sourceStream;
LeaveOpened = leaveOpen;

buffers = new byte[16];
}

/** <summary> Creates a new Instance of the <c>BinaryStream</c> Class with the given Buffers. </summary>

<param name = "sourceBuffers"> The Buffers where the Instance will be Created from. </param> */

public BinaryStream(byte[] sourceBuffers, bool leaveOpen = false) : this(new MemoryStream(sourceBuffers), leaveOpen)
{
}

/** <summary> Creates a new Instance of the <c>BinaryStream</c> Class with the specific Location and opening Mode. </summary>

<param name = "targetPath"> The Path where the BinaryStream will be Created. </param>
<param name = "openingMode"> The Opening Mode of the Stream. </param> */

public BinaryStream(string targetPath, FileMode openingMode, bool leaveOpen = false) : 
this(FileManager.OpenFile(targetPath, openingMode), leaveOpen)
{
}

/** <summary> Checks if the Provided data Encoding is Set or not. </summary>

<param name = "sourceEncoding"> The Encoding to be Analized. </param>

<returns> The Validated Encoding. </returns> */

private static void CheckEncoding(ref Encoding sourceEncoding) => sourceEncoding ??= Encoding.UTF8;

/** <summary> Checks if the Provided Order is Set or not. </summary>

<param name = "sourceOrder"> The Order to be Analized. </param>
<param name = "isForStrings"> A Boolean that Determines if the Endian must be used for Strings Encoding. </param>

<returns> The Validated Order. </returns> */

private static void CheckEndian(ref Endian sourceOrder, bool isForStrings = false)
{

if(isForStrings)
sourceOrder = (sourceOrder == Endian.Default) ? Endian.BigEndian : sourceOrder;

else
sourceOrder = (sourceOrder == Endian.Default) ? Endian.LittleEndian : sourceOrder;

}

/** <summary> Flips the provided Bytes in case their Order is LitteEndian. </summary>

<param name = "sourceBytes"> The Bytes to be Sorted. </param>
<param name = "endian"> The Order of the Data. </param> */

private static void SortEndianBytes(ref byte[] sourceBytes, Endian endian)
{

if(endian != Endian.LittleEndian)
return;

Array.Reverse(sourceBytes);
}

/// <summary> Closes the Stream and Releases all the Resources consumed by it. </summary>

public virtual void Close() => Dispose(true);

/** <summary> Reads a 8-bits Integer and Compares it with the given one. </summary>

<param name = "expectedValue"> The Value to be Expected. </param> */

public void CompareByte<E>(byte expectedValue) where E : GenericValueMismatchException<byte>
{
GenericValueComparisson<byte, E>(Position, ReadByte(), expectedValue);
}

/** <summary> Reads a 8-bits signed Integer and Compares it with the given one. </summary>

<param name = "expectedValue"> The Value to be Expected. </param> */
public void CompareSByte<E>(sbyte expectedValue) where E : GenericValueMismatchException<sbyte>
{
GenericValueComparisson<sbyte, E>(Position, ReadSByte(), expectedValue);
}

/** <summary> Reads a 16-bits Integer and Compares it with the given one. </summary>

<param name = "expectedValue"> The Value to be Expected. </param>
<param name = "endian"> The Order of the Data. </param> */

public void CompareShort<E>(short expectedValue, Endian endian = default) 
where E : GenericValueMismatchException<short>
{
GenericValueComparisson<short, E>(Position, ReadShort(endian), expectedValue);
}

/** <summary> Reads a 16-bits unsigned Integer and Compares it with the given one. </summary>

<param name = "expectedValue"> The Value to be Expected. </param>
<param name = "endian"> The Order of the Data. </param> */

public void CompareUShort<E>(ushort expectedValue, Endian endian = default) 
where E : GenericValueMismatchException<ushort>
{
GenericValueComparisson<ushort, E>(Position, ReadUShort(endian), expectedValue);
}

/** <summary> Reads a 32-bits Integer and Compares it with the given one. </summary>

<param name = "expectedValue"> The Value to be Expected. </param>
<param name = "endian"> The Order of the Data. </param> */

public void CompareInt<E>(int expectedValue, Endian endian = default)
where E : GenericValueMismatchException<int>
{
GenericValueComparisson<int, E>(Position, ReadInt(endian), expectedValue);
}

/** <summary> Reads a 32-bits unsigned Integer and Compares it with the given one. </summary>

<param name = "expectedValue"> The Value to be Expected. </param>
<param name = "endian"> The Order of the Data. </param> */

public void CompareUInt<E>(uint expectedValue, Endian endian = default) 
where E : GenericValueMismatchException<uint>
{
GenericValueComparisson<uint, E>(Position, ReadUInt(endian), expectedValue);
}

/** <summary> Reads a 64-bits Integer and Compares it with the given one. </summary>

<param name = "expectedValue"> The Value to be Expected. </param>
<param name = "endian"> The Order of the Data. </param> */

public void CompareLong<E>(long expectedValue, Endian endian = default) 
where E : GenericValueMismatchException<long>
{
GenericValueComparisson<long, E>(Position, ReadLong(endian), expectedValue);
}

/** <summary> Reads a 64-bits unsigned Integer and Compares it with the given one. </summary>

<param name = "expectedValue"> The Value to be Expected. </param>
<param name = "endian"> The Order of the Data. </param> */

public void CompareULong<E>(ulong expectedValue, Endian endian = default)
where E : GenericValueMismatchException<ulong>
{
GenericValueComparisson<ulong, E>(Position, ReadULong(endian), expectedValue);
}

/** <summary> Reads an Array of Bytes and Compares it with the given one. </summary>

<param name = "expectedBytes"> The Bytes to be Expected. </param> */

public void CompareBytes<E>(byte[] expectedBytes, Endian endian = default)
where E : GenericValueMismatchException<byte[]>
{
long pos = Position;

byte[] inputBytes = ReadBytes(expectedBytes.Length, endian);

if(!inputBytes.SequenceEqual(expectedBytes) )
{
var error = (E)Activator.CreateInstance(typeof(E), pos, inputBytes, expectedBytes);

throw error;
}

}

/** <summary> Reads a String and Compares it with the given one as an Expected Value. </summary>

<param name = "expectedBytes"> The Bytes to be Expected. </param>
<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param> */

public void CompareString<E>(string expectedString, Encoding encoding = default, Endian endian = default)
where E : GenericValueMismatchException<string>
{
long pos = Position;

string inputString = ReadString(expectedString.Length, encoding, endian);

GenericValueComparisson<string, E>(pos, inputString, expectedString);
}

/** <summary> Copies the specified Stream to this Instance. </summary>

<param name = "sourceStream"> The Stream to Copy. </param> */

public void CopyTo(Stream sourceStream) => BaseStream.CopyTo(sourceStream);

/** <summary> Creates a new BinaryStream on the specific Location and with the specific Opening Mode. </summary>

<param name = "targetPath"> The Path where the Stream will be Created. </param>

<returns> The Stream that was Created. </returns> */

public static BinaryStream Create(string targetPath, FileMode openingMode) => new(targetPath, openingMode);

/// <summary> Releases all the Resources consumed by the Stream. </summary>

public void Dispose() => Dispose(true);

/** <summary> Releases all the Resources consumed by the Stream. </summary>

<param name ="disposing"> Determines if all the Resources should be Discarded. </param> */

protected virtual void Dispose(bool disposing)
{

if(disposing)
{

if(LeaveOpened)
BaseStream.Flush();

else
BaseStream.Close();

}

}

/** <summary> Fills the Buffer of the current BinaryStream with the given amount of Bytes. </summary>

<param name = "bytesCount"> The Number of Bytes to Fill. </param>> */

protected void FillBuffer(int bytesCount)
{
int totalBytes = 0;
int bytesRead;

if(bytesCount == 1)
{
bytesRead = BaseStream.ReadByte();

if(bytesRead == -1)
throw new IOException("Reached end of File");

buffers[0] = (byte)bytesRead;
return;
}

do
{
bytesRead = BaseStream.Read(buffers, totalBytes, bytesCount - totalBytes);

if(bytesCount == 0)
throw new IOException("The File is Empty");

totalBytes += bytesRead;
}

while(totalBytes < bytesCount);
}

// Flush

public void Flush() => BaseStream.Flush();

/** <summary> Makes a Comparisson between two Values of the same Type. </summary>
<remarks> If the Value read is different from the Value expected, this method throws an Exception. </remarks>

<param name = "sourceValue"> The Value to be Compared. </param>
<param name = "expectedValue"> The Value expected. </param> */

private static void GenericValueComparisson<T, E>(long pos, T sourceValue, T expectedValue)
where E : GenericValueMismatchException<T>
{

if(!sourceValue.Equals(expectedValue) )
{
var error = (E)Activator.CreateInstance(typeof(E), pos, sourceValue, expectedValue);

throw error;
}

}

// Get Output Stream (FileStream or MemoryStream)

public static BinaryStream GetOutputStream(string outputPath = null, int bufferSize = -1)
{
bool useFilePath = !string.IsNullOrEmpty(outputPath);

bufferSize = useFilePath && bufferSize <= 0 ? 4096 : bufferSize;

return useFilePath ? OpenWrite(outputPath, bufferSize) : new(true);
}

/** <summary> Reads a String until Zero (0x00 in Bytes) is Reached, and then, moves back to the Initial Offset. </summary>

<param name = "offset"> The Offset where the Data Starts. </param>
<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param>

<returns> The String Obtained. </returns> */

public string GetStringUntilZero(long offset, Encoding encoding = default, Endian endian = default)
{
long initialOffset = BaseStream.Position;
BaseStream.Seek(offset, SeekOrigin.Begin);

string inputString = ReadStringUntilZero(encoding, endian);
BaseStream.Seek(initialOffset, SeekOrigin.Begin);

return inputString;
}

/** <summary> Opens a BinaryStream on the specific Location. </summary>

<param name = "targetPath"> The Path where the BinaryStream to be Opened is Located. </param>

<returns> The BinaryStream that was Opened. </returns> */

public static BinaryStream Open(string targetPath) => new(targetPath, FileMode.Open, true);

/** <summary> Opens a BinaryStream for Reading. </summary>

<param name = "targetPath"> The Path where the BinaryStream to be Opened is Located. </param>

<returns> The BinaryStream that was Opened. </returns> */

public static BinaryStream OpenRead(string targetPath, int bufferSize = 4096)
{
return new(FileManager.OpenRead(targetPath, bufferSize), true);
}

/** <summary> Opens a BinaryStream for Writing. </summary>

<param name = "targetPath"> The Path where the BinaryStream to be Opened is Located. </param>

<returns> The BinaryStream that was Opened. </returns> */

public static BinaryStream OpenWrite(string targetPath, int bufferSize = 4096)
{
return new(FileManager.OpenWrite(targetPath, bufferSize), true);
}

/** <summary> Reads a 8-bits Integer from a BinaryStream and then goes back in the Stream. </summary>

<returns> The Byte that was Read. </returns> */

public byte PeekByte()
{
byte inputByte = ReadByte();

Position--;

return inputByte;
}

/** <summary> Reads an unsigned 16-bits Integer from a BinaryStream and then goes back in the Stream. </summary>

<returns> The UShort that was Read. </returns> */

public ushort PeekUShort(Endian endian = default)
{
ushort inputValue = ReadUShort(endian);

Position -= 2;

return inputValue;
}

/** <summary> Reads a 32-bits Integer from a BinaryStream and then goes back in the Stream. </summary>

<param name = "endian"> The Order of the Data. </param>

<returns> The Byte that was Read. </returns> */

public int PeekInt(Endian endian = default)
{
int inputValue = ReadInt(endian);

Position -= 4;

return inputValue;
}

/** <summary> Reads a String from a BinaryStream and then goes back in the Stream. </summary>

<param name = "stringLength"> The Length of the String. </param>
<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param>

<returns> The Byte that was Read. </returns> */

public string PeekString(long stringLength = -1, Encoding encoding = default, Endian endian = default)
{
string inputString = ReadString(stringLength, encoding, endian);

Position -= stringLength < 0 ? inputString.Length : stringLength;

return inputString;
}

/** <summary> Reads the Buffers from a BinaryStream. </summary>

<param name = "targetBuffers"> The Buffers to be Read. </param>

<returns> The Number of Bytes read from the Memory Buffers. </returns> */

public int Read(Span<byte> targetBuffers) => BaseStream.Read(targetBuffers);

/** <summary> Reads the Buffers from a BinaryStream. </summary>

<param name = "targetBuffers"> The Buffers to be Read. </param>
<param name = "offset"> The Offset where the Data Starts. </param>
<param name = "blockSize"> The Number of Bytes to be Read. </param>

<returns> The Number of Bytes read from the Memory Buffers. </returns> */

public int Read(byte[] targetBuffers, int offset, int blockSize) => BaseStream.Read(targetBuffers, offset, blockSize);

/** <summary> Reads a Boolean from a BinaryStream. </summary>

<returns> The Boolean that was Read. </returns> */

public bool ReadBool()
{
FillBuffer(1);

return buffers[0] != 0;
}

/** <summary> Reads a Unicode Character from a BinaryStream. </summary>

<param name = "endian"> The Order of the Data. </param>

<returns> The Char that was Read. </returns> */

public char ReadChar(Endian endian = default) => (char)ReadUShort(endian);

/** <summary> Reads a 8-bits Integer from a BinaryStream. </summary>

<returns> The Byte that was Read. </returns> */

public byte ReadByte()
{
FillBuffer(1);

return buffers[0];
}

/** <summary> Reads a 8-bits signed Integer from a BinaryStream. </summary>

<returns> The SByte that was Read. </returns> */

public sbyte ReadSByte()
{
FillBuffer(1);

return (sbyte)buffers[0];
}

/** <summary> Reads a 16-bits Integer from a BinaryStream. </summary>

<param name = "endian"> The Order of the Data. </param>

<returns> The Short that was Read. </returns> */

public short ReadShort(Endian endian = default)
{
CheckEndian(ref endian);
FillBuffer(2);

if(endian == Endian.BigEndian)
return (short)(buffers[1] | (buffers[0] << 8) );

return (short)(buffers[0] | (buffers[1] << 8) );
}

/** <summary> Reads a 16-bits unsigned Integer from a BinaryStream. </summary>

<param name = "endian"> The Order of the Data. </param>

<returns> The UShort that was Read. </returns> */

public ushort ReadUShort(Endian endian = default)
{
CheckEndian(ref endian);
FillBuffer(2);

if(endian == Endian.BigEndian)
return (ushort)(buffers[1] | (buffers[0] << 8) );

return (ushort)(buffers[0] | (buffers[1] << 8) );
}

/** <summary> Reads a 24-bits Integer from a BinaryStream. </summary>

<param name = "endian"> The Order of the Data. </param>

<returns> The Triple-Byte that was Read. </returns> */

public int ReadTripleByte(Endian endian = default)
{
uint inputValue = ReadUTripleByte(endian);

if( (inputValue & 0x800000) != 0) 
inputValue |= 0xff000000;
	
return (int)inputValue;
}

/** <summary> Reads a 24-bits unsigned Integer from a BinaryStream. </summary>

<param name = "endian"> The Order of the Data. </param>

<returns> The unsigned Triple-Byte that was Read. </returns> */

public uint ReadUTripleByte(Endian endian = default)
{
CheckEndian(ref endian);
FillBuffer(3);

if(endian == Endian.BigEndian)
return (uint)(buffers[2] | (buffers[1] << 8) | (buffers[0] << 16) );

return (uint)(buffers[0] | (buffers[1] << 8) | (buffers[2] << 16) );
}

/** <summary> Reads a 32-bits Integer from a BinaryStream. </summary>

<param name = "endian"> The Order of the Data. </param>

<returns> The Integer that was Read. </returns> */

public int ReadInt(Endian endian = default)
{
CheckEndian(ref endian);
FillBuffer(4);

if(endian == Endian.BigEndian)
return buffers[3] | (buffers[2] << 8) | (buffers[1] << 16) | (buffers[0] << 24);

return buffers[0] | (buffers[1] << 8) | (buffers[2] << 16) | (buffers[3] << 24);
}

/** <summary> Reads an 32-bits unsigned Integer from a BinaryStream. </summary>

<param name = "endian"> The Order of the Data. </param>

<returns> The unsigned Integer that was Read. </returns> */

public uint ReadUInt(Endian endian = default)
{
CheckEndian(ref endian);
FillBuffer(4);

if(endian == Endian.BigEndian)
return (uint)(buffers[3] | (buffers[2] << 8) | (buffers[1] << 16) | (buffers[0] << 24) );

return (uint)(buffers[0] | (buffers[1] << 8) | (buffers[2] << 16) | (buffers[3] << 24) );
}

/** <summary> Reads a 64-bits Integer from a BinaryStream. </summary>

<param name = "endian"> The Order of the Data. </param>

<returns> The Long that was Read. </returns> */

public long ReadLong(Endian endian = default)
{
CheckEndian(ref endian);
FillBuffer(8);

if(endian == Endian.BigEndian)
return (long)( ( ( (ulong)(uint)(buffers[3] | (buffers[2] << 8) | (buffers[1] << 16) | (buffers[0] << 24) ) ) << 32) | ( (uint)(buffers[7] | (buffers[6] << 8) | (buffers[5] << 16) | (buffers[4] << 24) ) ) );

return (long)( ( ( (ulong)(uint)(buffers[4] | (buffers[5] << 8) | (buffers[6] << 16) | (buffers[7] << 24) ) ) << 32) | ( (uint)(buffers[0] | (buffers[1] << 8) | (buffers[2] << 16) | (buffers[3] << 24) ) ) );
}

/** <summary> Reads a 64-bits unsigned Integer from a BinaryStream. </summary>

<param name = "endian"> The Order of the Data. </param>

<returns> The ULong that was Read. </returns> */

public ulong ReadULong(Endian endian = default)
{
CheckEndian(ref endian);
FillBuffer(8);

if(endian == Endian.BigEndian)
return ( ( (ulong)(uint)(buffers[3] | (buffers[2] << 8) | (buffers[1] << 16) | (buffers[0] << 24) ) ) << 32) | ( (uint)(buffers[7] | (buffers[6] << 8) | (buffers[5] << 16) | (buffers[4] << 24) ) );

return ( ( (ulong)(uint)(buffers[4] | (buffers[5] << 8) | (buffers[6] << 16) | (buffers[7] << 24) ) ) << 32) | ( (uint)(buffers[0] | (buffers[1] << 8) | (buffers[2] << 16) | (buffers[3] << 24) ) );
}

/** <summary> Reads a 128-bits Integer from a BinaryStream. </summary>
<param name = "endian"> The Order of the Data. </param>
<returns> The Int128 that was Read. </returns> */

public Int128 ReadInt128(Endian endian = default)
{
CheckEndian(ref endian);
FillBuffer(16);

ulong high;
ulong low;

if(endian == Endian.BigEndian)
{
high = (ulong)(buffers[7] | (buffers[6] << 8) | (buffers[5] << 16) | (buffers[4] << 24) |
(buffers[3] << 32) | (buffers[2] << 40) | (buffers[1] << 48) | (buffers[0] << 56) );

low = (ulong)(buffers[15] | (buffers[14] << 8) | (buffers[13] << 16) | (buffers[12] << 24) |
buffers[11] << 32 | (buffers[10] << 40) | (buffers[9] << 48) | (buffers[8] << 56) );
        
}

else
{
low = (ulong) ( buffers[0] | (buffers[1] << 8) | (buffers[2] << 16) | (buffers[3] << 24) |
buffers[4] << 32 | (buffers[5] << 40) | (buffers[6] << 48) | (buffers[7] << 56)) ;

high = (ulong)(buffers[8] | (buffers[9] << 8) | (buffers[10] << 16) | (buffers[11] << 24) |
(buffers[12] << 32) | (buffers[13] << 40) | (buffers[14] << 48) | (buffers[15] << 56) );

}

return new(high, low);
}

/** <summary> Reads a VarInt (32-bits variant Integer) from a BinaryStream. </summary>

<returns> The VarInt read represented as a 32-bits Value. </returns> */

public int ReadVarInt()
{
int varInt = 0;
int integerBase = 0;

byte inputValue;

do
{

if(integerBase == 35)
throw new Exception("Not a 32-bits Integer");

inputValue = ReadByte();
varInt |= (inputValue & 0x7F) << integerBase;

integerBase += 7;
}

while( (inputValue & 0x80) != 0);
	
return varInt;
}

/** <summary> Reads an unsigned VarInt (32-bits variant Integer) from a BinaryStream. </summary>

<returns> The VarInt read represented as an unsigned 32-bits Value. </returns> */

public uint ReadUVarInt() => (uint)ReadVarInt();

/** <summary> Reads a VarLong (64-bits variant Integer) from a BinaryStream. </summary>

<returns> The VarInt read represented as a 64-bits Value. </returns> */

public long ReadVarLong()
{
long varInt = 0;
int integerBase = 0;

byte inputValue;

do
{

if(integerBase == 70)
throw new Exception("Not a 64-bits Integer");

inputValue = ReadByte();
varInt |= ( (long)(inputValue & 0x7F) ) << integerBase;

integerBase += 7;
}

while( (inputValue & 0x80) != 0);

return varInt;
}

/** <summary> Reads an unsigned VarLong (64-bits variant Integer) from a BinaryStream. </summary>

<returns> The VarInt read represented as an unsigned 64-bits Value. </returns> */

public ulong ReadUVarLong() => (ulong)ReadVarLong();

/** <summary> Reads a VarInt from a BinaryStream as a ZigZag Integer. </summary>

<returns> The ZigZag Int that was Read. </returns> */

public int ReadZigZagInt()
{
uint inputValue = ReadUVarInt();

if( (inputValue & 0b1) == 0)
return (int)(inputValue >> 1);

return -(int)( (inputValue + 1) >> 1);
}

/** <summary> Reads a VarInt from a BinaryStream as an unsigned ZigZag Integer. </summary>

<returns> The unsigned ZigZag Int that was Read. </returns> */

public uint ReadUZigZagInt() => (uint)ReadZigZagInt();

/** <summary> Reads a VarInt from a BinaryStream as a ZigZag Long. </summary>

<returns> The ZigZag Long that was Read. </returns> */

public long ReadZigZagLong()
{
ulong inputValue = ReadUVarLong();

if( (inputValue & 0b1) == 0)
return (long)(inputValue >> 1);

return -(long)( (inputValue + 1) >> 1);
}

/** <summary> Reads a VarInt from a BinaryStream as an unsigned ZigZag Long. </summary>

<returns> The unsigned ZigZag Long that was Read. </returns> */

public ulong ReadUZigZagLong() => (ulong)ReadZigZagLong();

/** <summary> Reads a 32-bits Float-point from a BinaryStream. </summary>

<param name = "endian"> The Order of the Data. </param>

<returns> The Float Value that was Read. </returns> */

public float ReadFloat(Endian endian = default)
{
byte[] inputBytes = BitConverter.GetBytes(ReadUInt(endian) );

return BitConverter.ToSingle(inputBytes, 0);
}

/** <summary> Reads a 64-bits Float-point from a BinaryStream. </summary>

<param name = "endian"> The Order of the Data. </param>

<returns> The Double Value that was Read. </returns> */

public double ReadDouble(Endian endian = default)
{
byte[] inputBytes = BitConverter.GetBytes(ReadULong(endian) );

return BitConverter.ToDouble(inputBytes, 0);
}

/** <summary> Reads an Array of Bytes from a BinaryStream. </summary>

<param name = "bytesCount"> The Number of Bytes to Read. </param>
<param name = "endian"> The Order of the Data. </param>

<returns> The Array of Bytes that was Read. </returns> */

public byte[] ReadBytes(long bytesCount = -1, Endian endian = default)
{
bytesCount = (bytesCount < 0) ? Length : bytesCount;

byte[] inputBytes;

if(bytesCount == 0)
inputBytes = Array.Empty<byte>();

else
{
inputBytes = new byte[bytesCount];			
int totalBytes = 0;

do
{
int bytesRead = BaseStream.Read(inputBytes, totalBytes, (int)bytesCount);

if(bytesRead == 0)
break;

totalBytes += bytesRead;
bytesCount -= bytesRead;
}

while(bytesCount > 0);

if(totalBytes != inputBytes.Length)
{
byte[] fillingBuffer = new byte[totalBytes];
Array.Copy(inputBytes, 0, fillingBuffer, 0, fillingBuffer.Length);

inputBytes = fillingBuffer;
}

}

SortEndianBytes(ref inputBytes, endian);

return inputBytes;
}

/** <summary> Reads a Base64 String from a BinaryStream and Decodes it. </summary>

<param name = "stringLength"> The Length of the String. </param>
<param name = "isWebSafe"> Determines if the String read should be treated as Base64 WebSafe. </param>

<returns> The String Decoded as Bytes. </returns> */

public byte[] ReadBase64(bool isWebSafe, long stringLength = -1)
{
return Base64StringParser.DecodeString(ReadString(stringLength), isWebSafe);
}

/** <summary> Reads a String from a BinaryStream. </summary>

<param name = "stringLength"> The Length of the String. </param>
<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param>

<returns> The String that was Read. </returns> */

public string ReadString(long stringLength = -1, Encoding encoding = default, Endian endian = default)
{
CheckEncoding(ref encoding);
CheckEndian(ref endian, true);

return encoding.GetString(ReadBytes(stringLength, endian) );
}

/** <summary> Reads a String from a BinaryStream along with its Length as an unsigned 8-bits Integer. </summary>

<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param>

<returns> The String that was Read. </returns> */

public string ReadStringByByteLength(Encoding encoding = default, Endian endian = default)
{
return ReadString(ReadByte(), encoding, endian);
}

/** <summary> Reads a String from a BinaryStream along with its Length as an unsigned 16-bits Integer. </summary>

<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param>

<returns> The String that was Read. </returns> */

public string ReadStringByUShortLength(Encoding encoding = default, Endian endian = default)
{
return ReadString(ReadUShort(endian), encoding, endian);
}

/** <summary> Reads a String from a BinaryStream along with its Length as an unsigned 32-bits Integer. </summary>

<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param>

<returns> The String that was Read. </returns> */

public string ReadStringByIntLength(Encoding encoding = default, Endian endian = default)
{
return ReadString(ReadInt(endian), encoding, endian);
}

/** <summary> Reads a String from a BinaryStream along with its Length as a 32-bits Variant Header. </summary>

<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param>

<returns> The String that was Read. </returns> */

public string ReadStringByVarIntLength(Encoding encoding = default, Endian endian = default)
{
return ReadString(ReadVarInt(), encoding, endian);
}

/** <summary> Reads a String from a BinaryStream until Zero (0x00 in Bytes) is Reached. </summary>

<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param>

<returns> The String that was Read. </returns> */

public string ReadStringUntilZero(Encoding encoding = default, Endian endian = default)
{
CheckEncoding(ref encoding);
CheckEndian(ref endian, true);

List<byte> bytesList = new();
byte inputByte;

while(true)
{

if( (inputByte = ReadByte() ) == 0x0)
break;

bytesList.Add(inputByte);
}

byte[] bytesRead = bytesList.ToArray();
SortEndianBytes(ref bytesRead, endian);

return encoding.GetString(bytesRead);
}

/** <summary> Sets the Position within the Current BinaryStream. </summary>

<param name = "offset"> The Offset to be Set. </param>
<param name = "seekOrigin"> The Origin of the Seek. </param> */

public void Seek(long offset, SeekOrigin seekOrigin) => BaseStream.Seek(offset, seekOrigin);

/** <summary> Sets the Length of the Current BinaryStream. </summary>

<param name = "length"> The Length in Bytes. </param> */

public void SetLength(long length) => BaseStream.SetLength(length);

/** <summary> Writes the specified Buffers into a BinaryStream. </summary>
<param name = "targetBuffers"> The Buffers to be Written. </param> */

public void Write(Span<byte> targetBuffers) => BaseStream.Write(targetBuffers);

/** <summary> Writes the specified Buffers into a BinaryStream. </summary>

<param name = "targetBuffers"> The Buffers to be Written. </param>
<param name = "offset"> The Offset where the Data Starts. </param>
<param name = "blockSize"> The Number of Bytes to be Written. </param> */

public void Write(byte[] targetBuffers, int offset, int blockSize) => BaseStream.Write(targetBuffers, offset, blockSize);

/** <summary> Writes a Sequence of Bytes as a Base64 String to a BinaryStream. </summary>

<param name = "data"> The Data to Write. </param>
<param name = "isWebSafe"> Determines if the Base64 String should be Encoded as WebSafe. </param> */

public void WriteBase64(byte[] data, bool isWebSafe)
{
WriteString(Base64StringParser.EncodeBytes(data, isWebSafe) );
}

/** <summary> Writes the specified Boolean into a BinaryStream. </summary>

<param name = "targetValue"> The Bool to be Written. </param> */

public void WriteBool(bool targetValue)
{
buffers[0] = (byte)(targetValue ? 1u : 0u);

BaseStream.Write(buffers, 0, 1);
}

/** <summary> Writes the specified Unicode Character into a BinaryStream. </summary>

<param name = "targetValue"> The Char to be Written. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteChar(char targetValue, Endian endian = default) => WriteUShort(targetValue, endian);

/** <summary> Writes the specified 8-bits Integer into a BinaryStream. </summary>

<param name = "targetValue"> The Byte to be Written. </param> */

public void WriteByte(byte targetValue) => BaseStream.WriteByte(targetValue);

/** <summary> Writes the specified 8-bits signed Integer into a BinaryStream. </summary>

<param name = "targetValue"> The SByte to be Written. </param> */

public void WriteSByte(sbyte targetValue) => BaseStream.WriteByte( (byte)targetValue);

/** <summary> Writes the specified 16-bits Integer into a BinaryStream. </summary>

<param name = "targetValue"> The Short to be Written. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteShort(short targetValue, Endian endian = default)
{
CheckEndian(ref endian);

if(endian == Endian.BigEndian)
{
buffers[1] = (byte)targetValue;
buffers[0] = (byte)(targetValue >> 8);
}

else
{
buffers[0] = (byte)targetValue;
buffers[1] = (byte)(targetValue >> 8);
}

BaseStream.Write(buffers, 0, 2);
}

/** <summary> Writes the specified 16-bits unsigned Integer into a BinaryStream. </summary>

<param name = "targetValue"> The UShort to be Written. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteUShort(ushort targetValue, Endian endian = default)
{
CheckEndian(ref endian);

if(endian == Endian.BigEndian)
{
buffers[1] = (byte)targetValue;
buffers[0] = (byte)(targetValue >> 8);
}

else
{
buffers[0] = (byte)targetValue;
buffers[1] = (byte)(targetValue >> 8);
}

BaseStream.Write(buffers, 0, 2);
}

/** <summary> Writes the specified 24-bits Integer into a BinaryStream. </summary>

<param name = "targetValue"> The Triple-Byte to be Written. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteTripleByte(int targetValue, Endian endian = default)
{
CheckEndian(ref endian);

if(endian == Endian.BigEndian)
{
buffers[2] = (byte)targetValue;
buffers[1] = (byte)(targetValue >> 8);
buffers[0] = (byte)(targetValue >> 16);
}

else
{
buffers[0] = (byte)targetValue;
buffers[1] = (byte)(targetValue >> 8);
buffers[2] = (byte)(targetValue >> 16);
}

BaseStream.Write(buffers, 0, 3);
}

/** <summary> Writes the specified 24-bits unsigned Integer into a BinaryStream. </summary>

<param name = "targetValue"> The unsigned Triple-Byte to be Written. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteUTripleByte(uint targetValue, Endian endian = default)
{
CheckEndian(ref endian);

if(endian == Endian.BigEndian)
{
buffers[2] = (byte)targetValue;
buffers[1] = (byte)(targetValue >> 8);
buffers[0] = (byte)(targetValue >> 16);
}

else
{
buffers[0] = (byte)targetValue;
buffers[1] = (byte)(targetValue >> 8);
buffers[2] = (byte)(targetValue >> 16);
}

BaseStream.Write(buffers, 0, 3);
}

/** <summary> Writes the specified 32-bits Integer into a BinaryStream. </summary>

<param name = "targetValue"> The Integer to be Written. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteInt(int targetValue, Endian endian = default)
{
CheckEndian(ref endian);

if(endian == Endian.BigEndian)
{
buffers[3] = (byte)targetValue;
buffers[2] = (byte)(targetValue >> 8);
buffers[1] = (byte)(targetValue >> 16);
buffers[0] = (byte)(targetValue >> 24);
}

else
{
buffers[0] = (byte)targetValue;
buffers[1] = (byte)(targetValue >> 8);
buffers[2] = (byte)(targetValue >> 16);
buffers[3] = (byte)(targetValue >> 24);
}

BaseStream.Write(buffers, 0, 4);
}

/** <summary> Writes the specified 32-bits unsigned Integer into a BinaryStream. </summary>

<param name = "targetValue"> The unsigned Integer to be Written. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteUInt(uint targetValue, Endian endian = default)
{
CheckEndian(ref endian);

if(endian == Endian.BigEndian)
{
buffers[3] = (byte)targetValue;
buffers[2] = (byte)(targetValue >> 8);
buffers[1] = (byte)(targetValue >> 16);
buffers[0] = (byte)(targetValue >> 24);
}

else
{
buffers[0] = (byte)targetValue;
buffers[1] = (byte)(targetValue >> 8);
buffers[2] = (byte)(targetValue >> 16);
buffers[3] = (byte)(targetValue >> 24);
}

BaseStream.Write(buffers, 0, 4);
}

/** <summary> Writes the specified 64-bits Integer into a BinaryStream. </summary>

<param name = "targetValue"> The Long to be Written. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteLong(long targetValue, Endian endian = default)
{
CheckEndian(ref endian);

if(endian == Endian.BigEndian)
{
buffers[7] = (byte)targetValue;
buffers[6] = (byte)(targetValue >> 8);
buffers[5] = (byte)(targetValue >> 16);
buffers[4] = (byte)(targetValue >> 24);
buffers[3] = (byte)(targetValue >> 32);
buffers[2] = (byte)(targetValue >> 40);
buffers[1] = (byte)(targetValue >> 48);
buffers[0] = (byte)(targetValue >> 56);
}

else
{
buffers[0] = (byte)targetValue;
buffers[1] = (byte)(targetValue >> 8);
buffers[2] = (byte)(targetValue >> 16);
buffers[3] = (byte)(targetValue >> 24);
buffers[4] = (byte)(targetValue >> 32);
buffers[5] = (byte)(targetValue >> 40);
buffers[6] = (byte)(targetValue >> 48);
buffers[7] = (byte)(targetValue >> 56);
}

BaseStream.Write(buffers, 0, 8);
}

/** <summary> Writes the specified 64-bits unsigned Integer into a BinaryStream. </summary>

<param name = "targetValue"> The ULong to be Written. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteULong(ulong targetValue, Endian endian = default)
{
CheckEndian(ref endian);

if(endian == Endian.BigEndian)
{
buffers[7] = (byte)targetValue;
buffers[6] = (byte)(targetValue >> 8);
buffers[5] = (byte)(targetValue >> 16);
buffers[4] = (byte)(targetValue >> 24);
buffers[3] = (byte)(targetValue >> 32);
buffers[2] = (byte)(targetValue >> 40);
buffers[1] = (byte)(targetValue >> 48);
buffers[0] = (byte)(targetValue >> 56);
}

else
{
buffers[0] = (byte)targetValue;
buffers[1] = (byte)(targetValue >> 8);
buffers[2] = (byte)(targetValue >> 16);
buffers[3] = (byte)(targetValue >> 24);
buffers[4] = (byte)(targetValue >> 32);
buffers[5] = (byte)(targetValue >> 40);
buffers[6] = (byte)(targetValue >> 48);
buffers[7] = (byte)(targetValue >> 56);
}

BaseStream.Write(buffers, 0, 8);
}

/** <summary> Writes the specified 128-bits Integer into a BinaryStream. </summary>

<param name = "targetValue"> The Int128 to be Written. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteInt128(Int128 targetValue, Endian endian = default)
{
CheckEndian(ref endian);

if(endian == Endian.BigEndian)
{
buffers[15] = (byte)targetValue;
buffers[14] = (byte)(targetValue >> 8);
buffers[13] = (byte)(targetValue >> 16);
buffers[12] = (byte)(targetValue >> 24);
buffers[11] = (byte)(targetValue >> 32);
buffers[10] = (byte)(targetValue >> 40);
buffers[9] = (byte)(targetValue >> 48);
buffers[8] = (byte)(targetValue >> 56);
buffers[7] = (byte)(targetValue >> 64);
buffers[6] = (byte)(targetValue >> 72);
buffers[5] = (byte)(targetValue >> 80);
buffers[4] = (byte)(targetValue >> 88);
buffers[3] = (byte)(targetValue >> 96);
buffers[2] = (byte)(targetValue >> 104);
buffers[1] = (byte)(targetValue >> 112);
buffers[0] = (byte)(targetValue >> 120);
}

else
{
buffers[0] = (byte)targetValue;
buffers[1] = (byte)(targetValue >> 8);
buffers[2] = (byte)(targetValue >> 16);
buffers[3] = (byte)(targetValue >> 24);
buffers[4] = (byte)(targetValue >> 32);
buffers[5] = (byte)(targetValue >> 40);
buffers[6] = (byte)(targetValue >> 48);
buffers[7] = (byte)(targetValue >> 56);
buffers[8] = (byte)(targetValue >> 64);
buffers[9] = (byte)(targetValue >> 72);
buffers[10] = (byte)(targetValue >> 80);
buffers[11] = (byte)(targetValue >> 88);
buffers[12] = (byte)(targetValue >> 96);
buffers[13] = (byte)(targetValue >> 104);
buffers[14] = (byte)(targetValue >> 112);
buffers[15] = (byte)(targetValue >> 120);
}

BaseStream.Write(buffers, 0, 16);
}

/** <summary> Writes the specified VarInt (32-bits variant Integer) into a BinaryStream. </summary>

<param name = "targetValue"> The VarInt to be Written. </param> */

public void WriteVarInt(int targetValue)
{
uint outputValue;

for(outputValue = (uint)targetValue; outputValue >= 128; outputValue >>= 7)
WriteByte( (byte)(outputValue | 0x80) );

WriteByte( (byte)outputValue);
}

/** <summary> Writes the specified UVarInt (32-bits variant Integer) into a BinaryStream. </summary>

<param name = "targetValue"> The unsigned VarInt to be Written. </param> */

public void WriteUVarInt(uint targetValue)
{
uint outputValue;

for(outputValue = targetValue; outputValue >= 128; outputValue >>= 7)
WriteByte( (byte)(outputValue | 0x80) );

WriteByte( (byte)outputValue);
}

/** <summary> Writes the specified VarLong (64-bits variant Integer) into a BinaryStream. </summary>

<param name = "targetValue"> The VarLong to be Written. </param> */

public void WriteVarLong(long targetValue)
{
ulong outputValue;

for(outputValue = (ulong)targetValue; outputValue >= 128; outputValue >>= 7)
WriteByte( (byte)(outputValue | 0x80) );

WriteByte( (byte)outputValue);
}

/** <summary> Writes the specified UVarLong (64-bits variant Integer) into a BinaryStream. </summary>

<param name = "targetValue"> The unsigned VarLong to be Written. </param> */

public void WriteUVarLong(ulong targetValue)
{
ulong outputValue;

for(outputValue = targetValue; outputValue >= 128; outputValue >>= 7)
WriteByte( (byte)(outputValue | 0x80) );

WriteByte( (byte)outputValue);
}

/** <summary> Writes the specified ZigZag 32-bits Integer into a BinaryStream. </summary>

<param name = "targetValue"> The ZigZag Int to be Written. </param> */

public void WriteZigZagInt(int targetValue) => WriteVarInt( (targetValue << 1) ^ (targetValue >> 31) );

/** <summary> Writes the specified ZigZag 64-bits Integer into a BinaryStream. </summary>

<param name = "targetValue"> The ZigZag Long to be Written. </param> */

public void WriteZigZagLong(long targetValue) => WriteVarLong( (targetValue << 1) ^ (targetValue >> 63) );

/** <summary> Writes the specified 32-bits Floating-point into a BinaryStream. </summary>

<param name = "targetValue"> The ZigZag Long to be Written. </param>

<param name = "endian"> The Order of the Data. </param> */

public void WriteFloat(float targetValue, Endian endian = default)
{
CheckEndian(ref endian);

uint outputValue = BitConverter.ToUInt32(BitConverter.GetBytes(targetValue), 0);

if(endian == Endian.BigEndian)
{
buffers[3] = (byte)outputValue;
buffers[2] = (byte)(outputValue >> 8);
buffers[1] = (byte)(outputValue >> 16);
buffers[0] = (byte)(outputValue >> 24);
}

else
{
buffers[0] = (byte)outputValue;
buffers[1] = (byte)(outputValue >> 8);
buffers[2] = (byte)(outputValue >> 16);
buffers[3] = (byte)(outputValue >> 24);
}

BaseStream.Write(buffers, 0, 4);
}

/** <summary> Writes the specified 64-bits Floating-point into a BinaryStream. </summary>

<param name = "targetValue"> The Double to be Written. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteDouble(double targetValue, Endian endian = default)
{
CheckEndian(ref endian);

ulong outputValue = BitConverter.ToUInt64(BitConverter.GetBytes(targetValue), 0);

if(endian == Endian.BigEndian)
{
buffers[7] = (byte)outputValue;
buffers[6] = (byte)(outputValue >> 8);
buffers[5] = (byte)(outputValue >> 16);
buffers[4] = (byte)(outputValue >> 24);
buffers[3] = (byte)(outputValue >> 32);
buffers[2] = (byte)(outputValue >> 40);
buffers[1] = (byte)(outputValue >> 48);
buffers[0] = (byte)(outputValue >> 56);
}

else
{
buffers[0] = (byte)outputValue;
buffers[1] = (byte)(outputValue >> 8);
buffers[2] = (byte)(outputValue >> 16);
buffers[3] = (byte)(outputValue >> 24);
buffers[4] = (byte)(outputValue >> 32);
buffers[5] = (byte)(outputValue >> 40);
buffers[6] = (byte)(outputValue >> 48);
buffers[7] = (byte)(outputValue >> 56);
}

BaseStream.Write(buffers, 0, 8);
}

/** <summary> Writes the specified String into a BinaryStream. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteString(string targetStr, Encoding encoding = default, Endian endian = default)
{
CheckEncoding(ref encoding);
CheckEndian(ref endian, true);

if(targetStr == null)
return;

byte[] outputBytes = encoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endian);

BaseStream.Write(outputBytes, 0, outputBytes.Length);
}

/** <summary> Writes the specified String within its specific Length into a BinaryStream. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "stringLength"> The Length of the String. </param>
<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteString(string targetStr, int stringLength, Encoding encoding = default, Endian endian = default)
{

if(stringLength <= 0 || targetStr == null)
return;

CheckEncoding(ref encoding);
CheckEndian(ref endian, true);

byte[] fillingBuffer = new byte[stringLength];
byte[] outputBytes = encoding.GetBytes(targetStr);

SortEndianBytes(ref outputBytes, endian);

if(outputBytes.Length >= stringLength)
Array.Copy(outputBytes, 0, fillingBuffer, 0, stringLength);

else
Array.Copy(outputBytes, 0, fillingBuffer, 0, fillingBuffer.Length);

BaseStream.Write(fillingBuffer, 0, stringLength);
}

/** <summary> Writes the specified String into a BinaryStream. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteStringAsFourBytes(string targetStr, Encoding encoding = default, Endian endian = default)
{
CheckEncoding(ref encoding);
CheckEndian(ref endian, true);

if(targetStr == null)
return;

byte[] outputBytes = new byte[targetStr.Length * 4 + 4];

for(int i = 0; i < targetStr.Length; i++)
outputBytes[i * 4] = (byte)targetStr[i];

SortEndianBytes(ref outputBytes, endian);

BaseStream.Write(outputBytes, 0, outputBytes.Length);
}

/** <summary> Writes the specified String into a BinaryStream along with its Length as a 8-bits Integer. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteStringByByteLength(string targetStr, Encoding encoding = default, Endian endian = default)
{
CheckEncoding(ref encoding);
CheckEndian(ref endian, true);

if(string.IsNullOrEmpty(targetStr) )
{
WriteByte(0x00);
return;
}

byte[] outputBytes = encoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endian);

int outputBytesCount = outputBytes.Length;
WriteByte( (byte)outputBytesCount);

BaseStream.Write(outputBytes, 0, outputBytesCount);
}

/** <summary> Writes the specified String into a BinaryStream along with its Length as an unsigned 16-bits Integer. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteStringByUShortLength(string targetStr, Encoding encoding = default, Endian endian = default)
{
CheckEncoding(ref encoding);
CheckEndian(ref endian, true);

if(string.IsNullOrEmpty(targetStr) )
{
WriteUShort(0);
return;
}

byte[] outputBytes = encoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endian);

int outputBytesCount = outputBytes.Length;
WriteUShort( (ushort)outputBytesCount, endian);

BaseStream.Write(outputBytes, 0, outputBytesCount);
}

/** <summary> Writes the specified String into a BinaryStream along with its Length as a 32-bits Integer. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteStringByIntLength(string targetStr, Encoding encoding = default, Endian endian = default)
{
CheckEncoding(ref encoding);
CheckEndian(ref endian, true);

if(string.IsNullOrEmpty(targetStr) )
{
WriteInt(0);
return;
}

byte[] outputBytes = encoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endian);

int outputBytesCount = outputBytes.Length;
WriteInt(outputBytesCount, endian);

BaseStream.Write(outputBytes, 0, outputBytesCount);
}

/** <summary> Writes the specified String into a BinaryStream along with its Length a 32-bits variant Integer. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteStringByVarIntLength(string targetStr, Encoding encoding = default, Endian endian = default)
{
CheckEncoding(ref encoding);
CheckEndian(ref endian, true);

if(string.IsNullOrEmpty(targetStr) )
{
WriteVarInt(0);
return;
}

byte[] outputBytes = encoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endian);

int outputBytesCount = outputBytes.Length;
WriteVarInt(outputBytesCount);

BaseStream.Write(outputBytes, 0, outputBytesCount);
}

/** <summary> Writes the specified String into a BinaryStream until Zero (0x00 in Bytes) is Reached. </summary>

<param name = "targetStr"> The String to be Written. </param>
<param name = "encoding"> The Data Encoding. </param>
<param name = "endian"> The Order of the Data. </param> */

public void WriteStringUntilZero(string targetStr, Encoding encoding = default, Endian endian = default)
{
CheckEncoding(ref encoding);
CheckEndian(ref endian, true);

if(string.IsNullOrEmpty(targetStr) )
{
WriteByte(0x00);
return;
}

byte[] outputBytes = encoding.GetBytes(targetStr);
SortEndianBytes(ref outputBytes, endian);

BaseStream.Write(outputBytes, 0, outputBytes.Length);
WriteByte(0x00);
}

// Write Padding Bytes to Stream

public void WritePadding(int bytesCount, byte paddingByte = 0x00)
{

if(bytesCount <= 0)
return;

for(int i = 0; i < bytesCount; i++)
WriteByte(paddingByte);

}

public static implicit operator Stream(BinaryStream a) => a.BaseStream;
}