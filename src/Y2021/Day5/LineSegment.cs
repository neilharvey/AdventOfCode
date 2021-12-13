namespace AdventOfCode.Y2021.Day5;

public struct LineSegment
{
    public LineSegment(Point start, Point end)
    {
        Start = start;
        End = end;
    }

    public Point Start { get; }

    public Point End { get; }

    public bool IsHorizontal => Start.Y == End.Y;

    public bool IsVertical => Start.X == End.X;

    public IEnumerable<Point> GetPoints()
    {
        var x = Start.X;
        var y = Start.Y;

        var dx = End.X.CompareTo(Start.X);
        var dy = End.Y.CompareTo(Start.Y);

        var points = new List<Point>();
        while (x != End.X || y != End.Y)
        {
            points.Add(new Point(x, y));
            x += dx;
            y += dy;
        }
        points.Add(End);

        return points;
    }

    public static LineSegment Parse(string line)
    {
        // string containing x1,y1 -> x2,y2
        var parts = line.Split(' ');
        var start = Point.Parse(parts[0]);
        var end = Point.Parse(parts[2]);
        return new LineSegment(start, end);
    }

    public override string ToString()
    {
        return $"{Start} -> {End}";
    }
}
