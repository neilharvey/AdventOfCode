namespace AdventOfCode.Y2021.Day12
{
    public class Solution : IPuzzleSolution
    {
        private delegate bool IsVisitedCallback(Cave cave, List<Cave> path);

        public long Part1(StreamReader reader)
            => FindAllPaths(reader, IsSmallCaveVisitedOnce);

        public long Part2(StreamReader reader)
            => FindAllPaths(reader, IsSmallCaveVisitedOnceAndNoOtherVisitedTwice);

        private static long FindAllPaths(StreamReader reader, IsVisitedCallback callback)
        {
            var caves = new CaveSystem();

            while (reader.TryReadLine(out string line))
            {
                var nodes = line.Split('-');
                caves.AddConnection(nodes[0], nodes[1]);
            }

            var paths = Explore(caves.Start, new List<Cave>(), callback);

            return paths.Count;
        }

        private static List<List<Cave>> Explore(Cave cave, List<Cave> path, IsVisitedCallback isVisited)
        {
            path.Add(cave);

            var paths = new List<List<Cave>>();

            if (cave.IsEnd)
            {
                paths.Add(path);
            }
            else
            {
                foreach (var adj in cave.Adjacent)
                {
                    if (!(adj.IsStart || isVisited(adj, path)))
                    {
                        var branch = new List<Cave>(path);
                        paths.AddRange(Explore(adj, branch, isVisited));
                    }
                }
            }

            return paths;
        }

        private static bool IsSmallCaveVisitedOnce(Cave adj, List<Cave> path)
        {
            return adj.IsSmall && path.Contains(adj);
        }

        private static bool IsSmallCaveVisitedOnceAndNoOtherVisitedTwice(Cave adj, List<Cave> path)
        {
            if (!adj.IsSmall)
            {
                return false;
            }

            if (!path.Contains(adj))
            {
                return false;
            }

            var anySmallCaveVisitedTwice = path
                .Where(x => x.IsSmall)
                .GroupBy(x => x.Label)
                .Any(g => g.Count() > 1);

            return anySmallCaveVisitedTwice;
        }
    }
}
