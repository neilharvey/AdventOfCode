namespace AdventOfCode.Y2021.Day12
{
    public class Solution : IPuzzleSolution
    {
        private delegate bool IsVisitedCallback(Cave cave, Path path);

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

            var paths = Explore(caves.Start, new Path(), callback);

            return paths.Count;
        }

        private static List<Path> Explore(Cave cave, Path path, IsVisitedCallback isVisited)
        {
            path.Add(cave);

            var paths = new List<Path>();

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
                        var branch = new Path(path);
                        paths.AddRange(Explore(adj, branch, isVisited));
                    }
                }
            }

            return paths;
        }

        private static bool IsSmallCaveVisitedOnce(Cave adj, Path path)
        {
            return adj.IsSmall && path.Contains(adj);
        }

        private static bool IsSmallCaveVisitedOnceAndNoOtherVisitedTwice(Cave adj, Path path)
        {
            if (!adj.IsSmall)
            {
                return false;
            }

            if (!path.Contains(adj))
            {
                return false;
            }

            return path.AnySmallCaveVisitedTwice;
        }
    }
}
