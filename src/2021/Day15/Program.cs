using Day15;

static IList<Node> AStar(Graph graph, Node start, Node goal)
{
    var frontier = new PriorityQueue<Node, int>();
    frontier.Enqueue(start, 0);

    var cameFrom = new Dictionary<Node, Node>();
    var bestCost = new Dictionary<Node, int>
    {
        { start, 0 }
    };

    var estimate = new Dictionary<Node, int>
    {
        { start, Heuristic(start, goal) }
    };

    while (frontier.Count > 0)
    {
        var current = frontier.Dequeue();
        if (current == goal)
        {
            return ReconstructPath(graph, cameFrom, current);
        }

        foreach (var neighbour in graph.GetNeighbours(current))
        {
            var cost = bestCost[current] + graph.Cost(neighbour);
            if (!bestCost.ContainsKey(neighbour) || cost < bestCost[neighbour])
            {
                bestCost[neighbour] = cost;
                cameFrom[neighbour] = current;
                estimate[neighbour] = cost + Heuristic(neighbour, goal);

                if (!frontier.UnorderedItems.Any(x => x.Element == neighbour))
                {
                    frontier.Enqueue(neighbour, estimate[neighbour]);
                }
            }
        }
    }

    throw new ApplicationException("Goal was never reached.");
}

static IList<Node> ReconstructPath(Graph graph, Dictionary<Node, Node> cameFrom, Node current)
{
    var path = new List<Node>
            {
                current
            };

    while (cameFrom.ContainsKey(current))
    {
        current = cameFrom[current];
        path.Add(current);
    }

    path.Reverse();
    return path;
}

static long TotalCost(Graph graph, IList<Node> path)
{
    return path.Skip(1).Select(x => graph.Cost(x)).Sum();
}

static int Heuristic(Node from, Node to)
{
    // Manhattan distance
    return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
}

static long TotalRisk(string[] lines, int scale)
{
    var graph = Graph.Load(lines, scale);
    var path = AStar(graph, graph.Start, graph.Goal);
    return TotalCost(graph, path);
}

var lines = File.ReadAllLines(args[0]);
Console.WriteLine($"Part One: {TotalRisk(lines, 1)}");
Console.WriteLine($"Part Two: {TotalRisk(lines, 5)}");