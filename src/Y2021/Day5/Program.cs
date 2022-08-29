
static long Part1(string[] lines)
{
    var lineSegments = GetLineSegments(lines)
        .Where(s => s.IsHorizontal || s.IsVertical);

    return CountIntersections(lineSegments);
}

static long Part2(string[] lines)
{
    var lineSegments = GetLineSegments(lines);
    return CountIntersections(lineSegments);
}

static List<LineSegment> GetLineSegments(string[] lines)
{
    var lineSegments = new List<LineSegment>();
    foreach(var line in lines)
    {
        var lineSegment = LineSegment.Parse(line);
        lineSegments.Add(lineSegment);
    }

    return lineSegments;
}

static int CountIntersections(IEnumerable<LineSegment> lineSegments)
{
    return lineSegments.SelectMany(s => s.GetPoints())
        .GroupBy(p => p)
        .Where(g => g.Count() > 1)
        .Count();
}

var lines = File.ReadAllLines(args.First());
Console.WriteLine($"Part One: {Part1(lines)}");
Console.WriteLine($"Part Two: {Part2(lines)}");