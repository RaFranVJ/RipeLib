/// <summary> Represents some case-sentitive Options used for Comparing Strings. </summary>

public enum StringCaseHandling
{
/// <summary> String Case will be Ignored </summary>
OrdinalIgnoreCase,

/// <summary> String Case won't be Ignored </summary>
Ordinal,

/// <summary> String Case will follow the CurrentCulture rules </summary>
CurrentCulture,

/// <summary> String Case will be Ignored, following the CurrentCulture rules </summary>
CurrentCultureIgnoreCase,

/// <summary> String Case will follow the InvariantCulture rules </summary>
InvariantCulture,

/// <summary> String Case will be Ignored, following the InvariantCulture rules </summary>
InvariantCultureIgnoreCase
}