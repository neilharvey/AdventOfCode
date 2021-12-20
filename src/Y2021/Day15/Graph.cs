namespace AdventOfCode.Y2021.Day15
{
    public class Graph
    {
        private readonly int[,] _data;

        private Graph(int[,] data, int scale)
        {
            _data = data;
            Width = data.GetLength(0) * scale;
            Height = data.GetLength(1) * scale;
        }

        public Node Start => new(0, 0);

        public Node Goal => new(Width - 1, Height - 1);

        public int Width { get; }

        public int Height { get; }

        public IList<Node> GetNeighbours(Node node)
        {
            var neighbours = new List<Node>();

            if (node.X > 0)
                neighbours.Add(new Node(node.X - 1, node.Y));
            if (node.Y > 0)
                neighbours.Add(new Node(node.X, node.Y - 1));
            if (node.X < Width - 1)
                neighbours.Add(new Node(node.X + 1, node.Y));
            if (node.Y < Height - 1)
                neighbours.Add(new Node(node.X, node.Y + 1));

            return neighbours;
        }

        public int Cost(Node node)
        {
            var xIncrease = node.X / _data.GetLength(0);
            var yIncrease = node.Y / _data.GetLength(1);
            var x = node.X - (_data.GetLength(0) * xIncrease);
            var y = node.Y - (_data.GetLength(1) * yIncrease);
            var cost = (_data[x, y] + xIncrease + yIncrease) % 9;
            return cost == 0 ? 9 : cost;
        }

        public static Graph FromStream(StreamReader stream, int scale = 1)
        {
            var data = stream.ReadMatrix();
            return new Graph(data, scale);
        }
    }
}
