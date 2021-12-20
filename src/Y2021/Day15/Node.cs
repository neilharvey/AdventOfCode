namespace AdventOfCode.Y2021.Day15
{
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
}
