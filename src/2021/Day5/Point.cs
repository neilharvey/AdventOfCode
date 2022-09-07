public struct Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }

    public int Y { get; }

    public static Point Parse(string value)
    {
        var parts = value.Split(',');
        return new Point(int.Parse(parts[0]), int.Parse(parts[1]));
    }

    public static bool operator ==(Point x, Point y)
    {
        return x.X == y.X && x.Y == y.Y;
    }

    public static bool operator !=(Point x, Point y)
    {
        return !(x == y);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj is not Point point)
        {
            return false;
        }

        return this == point;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}
