using System;

/// <summary> Represents a SortPattern that Determine how to Sort Strings. </summary>

[Flags]

public enum StringSortPattern
{
Default,

/// <summary> LawnStrings should by Sorted Alphabetically by Ascending Order (A-Z) </summary>
OrderByAscending,

/// <summary> LawnStrings should by Sorted Alphabetically by Descending Order (Z-A) </summary>
OrderByDescending = 2,

/// <summary> LawnStrings shouls be Sorted by String Length, starting from the Smaller one in the Collection </summary>
OrderBySmallerLength = 4,

/// <summary> LawnStrings shouls be Sorted by String Length, starting from the Bigger one in the Collection </summary>
OrderByBiggerLength = 8,

/// <summary> LawnStrings shouls be Sorted Alphabetically and by Length (Ascending) </summary>
OrderByAscendingLength = OrderByAscending | OrderBySmallerLength,

/// <summary> LawnStrings shouls be Sorted Alphabetically and by Length (Descending) </summary>
OrderByDescendingLength = OrderByDescending | OrderByBiggerLength
}