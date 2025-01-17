using System;

public class GenericValueMismatchException<T>(long pos, T source, T expected, string message = null) : 
Exception(message ?? GetErrorMsg(pos, source, expected) )
{
public long Position{ get; } = pos;

public T ValueRead{ get; } = source;

// Get Error Msg

private static string GetErrorMsg(long pos, T source, T expected)
{
var type = typeof(T);

string displayA;
string displayB;

if(type.IsArray && type == typeof(byte[] ) )
{
displayA = BitConverter.ToString(source as byte[] ).Replace("-", " ");
displayB = BitConverter.ToString(expected as byte[] ).Replace("-", " ");
}

else if(type.IsArray && type != typeof(byte[] ) )
{
displayA = string.Join(", ", source);
displayB = string.Join(", ", expected);
}

else
{
displayA = $"{source}";
displayB = $"{expected}";
}

return $"The \"{type.Name}\" read ({displayA}) differs from the expected Value ({displayB})\nFound at {pos}";
}

}