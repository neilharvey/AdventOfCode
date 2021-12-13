namespace AdventOfCode.Y2021.Day7;

public class Solution : IPuzzleSolution
{
    public long Part1(StreamReader reader)
    {
        var positions = reader.ReadLine().AsIntegers();
        var sorted = positions.OrderBy(x => x).ToList();
        var mid = (positions.Count - 1) / 2D;
        var median = (sorted[(int)mid] + sorted[(int)(mid + 0.5)]) / 2;
        var fuel = positions.Select(x => Math.Abs(x - median)).Sum();
        return fuel;
    }

    public long Part2(StreamReader reader)
    {
        int fuel(IEnumerable<int> crabs, int position) => crabs
            .Select(x => Math.Abs(x - position))
            .Select(x => (x * (x + 1)) / 2)
            .Sum();

        var positions = reader.ReadLine().AsIntegers();
        var average = positions.Average();
        var floor = (int)Math.Floor(average);
        var ceiling = (int)Math.Ceiling(average);

        return Math.Min(
            fuel(positions, floor),
            fuel(positions, ceiling));
    }
}
