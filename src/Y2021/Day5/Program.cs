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

var input = File.ReadAllLines(args.First());
var lineSegments = GetLineSegments(input);

Console.WriteLine($"Part One: {CountIntersections(lineSegments.Where(s => s.IsHorizontal || s.IsVertical))}");
Console.WriteLine($"Part Two: {CountIntersections(lineSegments)}");