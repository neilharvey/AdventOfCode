namespace AdventOfCode.Y2021.Day15
{
    public static class Util
    {
        public static void PrintGraph(Graph graph, IList<Node> path)
        {
            for (var y = 0; y < graph.Height; y++)
            {
                for (var x = 0; x < graph.Width; x++)
                {
                    var node = new Node(x, y);
                    if (path.Contains(node))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write(graph.Cost(node));
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
