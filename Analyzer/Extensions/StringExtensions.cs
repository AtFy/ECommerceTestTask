namespace Lib.Analyzer.Extensions;

public static class StringExtensions
{
    public static List<string> AsList(this string initial, char divider = ',') => initial.Split(divider).ToList();
}