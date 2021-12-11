namespace AdventOfCode
{
    public static class StringExtensions
    {
        public static IList<int> ToListOfInt(this string value, string separator = ",")
        {
            if (string.IsNullOrEmpty(value))
            {
                return new List<int>();
            }
            else
            {
                return value.Split(separator, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
            }
        }
    }
}
