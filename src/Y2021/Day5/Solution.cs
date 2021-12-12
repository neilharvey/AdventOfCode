namespace AdventOfCode.Y2021.Day5;

public class Solution : IPuzzleSolution
{
    public int Part1(StreamReader reader)
    {
        var lineSegments = GetLineSegments(reader)
            .Where(s => s.IsHorizontal || s.IsVertical);

        return CountIntersections(lineSegments);
    }

    public int Part2(StreamReader reader)
    {
        var lineSegments = GetLineSegments(reader);
        return CountIntersections(lineSegments);
    }

    private static List<LineSegment> GetLineSegments(StreamReader reader)
    {
        var lineSegments = new List<LineSegment>();
        while (reader.TryReadLine(out string line))
        {
            var lineSegment = LineSegment.Parse(line);
            lineSegments.Add(lineSegment);
        }

        return lineSegments;
    }

    private static int CountIntersections(IEnumerable<LineSegment> lineSegments)
    {
        return lineSegments.SelectMany(s => s.GetPoints())
            .GroupBy(p => p)
            .Where(g => g.Count() > 1)
            .Count();
    }
}