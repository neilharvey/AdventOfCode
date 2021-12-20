namespace AdventOfCode.Y2021.Day15
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
        {
            var graph = Graph.FromStream(reader);
            var path = AStar(graph, graph.Start, graph.Goal);
            return TotalCost(graph, path);
        }

        public long Part2(StreamReader reader)
        {
            var graph = Graph.FromStream(reader, scale: 5);
            var path = AStar(graph, graph.Start, graph.Goal);
            return TotalCost(graph, path);
        }

        public static IList<Node> AStar(Graph graph, Node start, Node goal)
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

        private static IList<Node> ReconstructPath(Graph graph, Dictionary<Node, Node> cameFrom, Node current)
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

        private static long TotalCost(Graph graph, IList<Node> path)
        {
            return path.Skip(1).Select(x => graph.Cost(x)).Sum();
        }

        private static int Heuristic(Node from, Node to)
        {
            // Manhattan distance
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }
    }
}