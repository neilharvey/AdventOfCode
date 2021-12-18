namespace AdventOfCode.Y2021.Day12
{
    public class CaveSystem
    {
        private readonly Dictionary<string, Cave> _caves = new();

        public Cave Start { get; private set; }

        public void AddConnection(string from, string to)
        {
            if (!_caves.TryGetValue(from, out Cave caveFrom))
            {
                caveFrom = new Cave(from);
                _caves.Add(from, caveFrom);
            }

            if (!_caves.TryGetValue(to, out Cave caveTo))
            {
                caveTo = new Cave(to);
                _caves.Add(to, caveTo);
            }

            caveFrom.Adjacent.Add(caveTo);
            caveTo.Adjacent.Add(caveFrom);

            if (caveFrom.IsStart)
            {
                Start = caveFrom;
            }
        }
    }
}
