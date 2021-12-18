namespace AdventOfCode.Y2021.Day12
{
    public class Path
    {
        private readonly List<Cave> _caves = new();
        private readonly Dictionary<string, int> _timesVisited = new();

        public Path() { }

        public Path(Path path)
        {
            _caves = new List<Cave>(path._caves);
            _timesVisited = new Dictionary<string, int>(path._timesVisited);
            AnySmallCaveVisitedTwice = path.AnySmallCaveVisitedTwice;
        }

        public bool AnySmallCaveVisitedTwice { get; private set; }

        public void Add(Cave cave)
        {
            _caves.Add(cave);
            if (Contains(cave) && cave.IsSmall)
            {
                AnySmallCaveVisitedTwice = true;
            }

            SetVisited(cave);
        }

        public bool Contains(Cave cave)
        {
            return _timesVisited.ContainsKey(cave.Label);
        }

        private void SetVisited(Cave cave)
        {
            if (_timesVisited.ContainsKey(cave.Label))
            {
                _timesVisited[cave.Label]++;
            }
            else
            {
                _timesVisited.Add(cave.Label, 1);  
            }
        }

        public override string ToString()
        {
            return string.Join(",", _caves);
        }
    }
}
