namespace Day15;

public record struct Node
{
    public Node(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }

    public int Y { get; }
}