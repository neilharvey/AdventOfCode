namespace AdventOfCode.Y2021.Day10
{
    public static class MathExtensions
    {
        public static long Median(this IEnumerable<long> values)
        {
            var sorted = values.OrderBy(x => x).ToList();
            var mid = (sorted.Count - 1) / 2D;
            var median = (sorted[(int)mid] + sorted[(int)(mid + 0.5)]) / 2;
            return median;
        }
    }
}
