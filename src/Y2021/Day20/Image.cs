namespace AdventOfCode.Y2021.Day20
{
    public class Image
    {
        private readonly Dictionary<(int x, int y), bool> _pixels = new();

        public bool this[int x, int y]
        {
            get { return _pixels[(x, y)]; }
            set { 
                _pixels[(x,y)] = value;
            }
        }
    }
}
