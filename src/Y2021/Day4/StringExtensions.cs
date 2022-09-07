namespace Day4;

public static class StringExtensions
{
    public static IReadOnlyList<int> AsIntegers(this string value, string separator = ",")
    {
        if (string.IsNullOrEmpty(value))
        {
            return new List<int>();
        }
        else
        {
            return value
                .Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Convert.ToInt32(x))
                .ToList();
        }
    }
}
