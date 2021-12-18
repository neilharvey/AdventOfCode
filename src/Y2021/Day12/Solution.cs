namespace AdventOfCode.Y2021.Day12
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
        {
            var caves = ReadCaveSystem(reader);
            var paths = Explore1(caves.Start, new List<Cave>());
            return paths.Count;
        }

        public long Part2(StreamReader reader)
        {
            //var caves = ReadCaveSystem(reader);
            //var paths = Explore2(caves.Start, new List<Cave>(), new Dictionary<Cave, int>());
            //return paths.Count;
            throw new NotImplementedException();
        }

        private static CaveSystem ReadCaveSystem(StreamReader reader)
        {
            var caves = new CaveSystem();

            while (reader.TryReadLine(out string line))
            {
                var nodes = line.Split('-');
                caves.AddConnection(nodes[0], nodes[1]);
            }

            return caves;
        }

        // possible memoization optimisation here -?
        private List<List<Cave>> Explore1(Cave cave, List<Cave> path)
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
                    if (!IsVisited(adj, path))
                    {
                        var branch = new List<Cave>(path);
                        paths.AddRange(Explore1(adj, branch));
                    }
                }
            }

            return paths;
        }

        private static bool IsVisited(Cave adj, List<Cave> path)
        {
            return adj.IsStart || (adj.IsSmall && path.Contains(adj));
        }
    }
}
