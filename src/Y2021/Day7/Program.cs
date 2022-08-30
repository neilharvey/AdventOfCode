static long Part1(IReadOnlyList<int> positions)
{
    var sorted = positions.OrderBy(x => x).ToList();
    var mid = (positions.Count - 1) / 2D;
    var median = (sorted[(int)mid] + sorted[(int)(mid + 0.5)]) / 2;
    var fuel = positions.Select(x => Math.Abs(x - median)).Sum();
    return fuel;
}

static long Part2(IReadOnlyList<int> positions)
{
    int fuel(IEnumerable<int> crabs, int position) => crabs
        .Select(x => Math.Abs(x - position))
        .Select(x => (x * (x + 1)) / 2)
        .Sum();

    var average = positions.Average();
    var floor = (int)Math.Floor(average);
    var ceiling = (int)Math.Ceiling(average);

    return Math.Min(
        fuel(positions, floor),
        fuel(positions, ceiling));
}

var lines = File.ReadAllLines(args.First());
var positions = lines[0].AsIntegers();
Console.WriteLine($"Part One: {Part1(positions)}");
Console.WriteLine($"Part Two: {Part2(positions)}");