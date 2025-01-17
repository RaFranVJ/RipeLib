/// <summary> Serves as a Reference to Values that never Change on Code. </summary> 

public static class Constants
{
/// <summary> The Number of Milliseconds a Second has. </summary>

public const int MILLISECONDS = 1000;

/// <summary> The Number of Seconds a Minute has. </summary>

public const int SECONDS = 60;

/// <summary> The Number of Minutes an Hour has. </summary>

public const int MINUTES = 60;

/// <summary> The Value of one Kilobyte (1024 Bytes). </summary>

public static readonly long ONE_KILOBYTE = 1024;

/// <summary> The Value of one Megabyte (1024 Kilobytes). </summary>

public static readonly long ONE_MEGABYTE = ONE_KILOBYTE * 1024;

/// <summary> The Value of one Gigabyte (1024 Megabytes). </summary>

public static readonly long ONE_GIGABYTE = ONE_MEGABYTE * 1024;
}