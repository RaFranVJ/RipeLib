using System;

public static class StrComparerPlugin
{
// Get Str Conparer

public static StringComparer GetStringComparer(StringCaseHandling caseHandling)
{

return caseHandling switch
{
StringCaseHandling.Ordinal => StringComparer.Ordinal,
StringCaseHandling.CurrentCulture => StringComparer.CurrentCulture,
StringCaseHandling.CurrentCultureIgnoreCase => StringComparer.CurrentCultureIgnoreCase,
StringCaseHandling.InvariantCulture => StringComparer.InvariantCulture,
StringCaseHandling.InvariantCultureIgnoreCase => StringComparer.InvariantCultureIgnoreCase,
_ => StringComparer.OrdinalIgnoreCase
};

}

}