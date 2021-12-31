using System.Text;

namespace AdventOfCode.Y2021.Day20
{
    public class Image
    {
        private readonly Dictionary<(int x, int y), bool> _pixels = new();

        public int MinX => _pixels.Min(p => p.Key.x);

        public int MaxX => _pixels.Max(p => p.Key.y);

        public int MinY => _pixels.Min(p => p.Key.y);

        public int MaxY => _pixels.Max(p => p.Key.y);

        public bool this[int x, int y]
        {
            get
            {
                if (_pixels.TryGetValue((x, y), out bool value))
                {
                    return value;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                _pixels[(x, y)] = value;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var y = _pixels.Min(p => p.Key.y); y <= _pixels.Max(p => p.Key.y); y++)
            {
                for (var x = _pixels.Min(p => p.Key.x); x <= _pixels.Max(p => p.Key.x); x++)
                {
                    sb.Append(_pixels[(x, y)] ? '#' : '.');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
