using System.Text.RegularExpressions;

namespace AdventOfCode.Y2021.Day17
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
        {
            var target = GetTargetArea(reader);
            var velocityY = Math.Abs(target.MinY) - 1;
            var maxHeight = (velocityY * (velocityY + 1)) / 2;
            return maxHeight;
        }

        public long Part2(StreamReader reader)
        {
            var target = GetTargetArea(reader);
            var vminX = (int)(Math.Sqrt(2 * target.MinX));
            var vmaxX = target.MaxX;
            var vminY = target.MinY;
            var vmaxY = -1 * target.MinY;

            var velocities = new List<(int x, int y)>();
            for (var x = vminX; x <= vmaxX; x++)
            {
                for (var y = vminY; y <= vmaxY; y++)
                {
                    if (IsHit(x, y, target))
                    {
                        velocities.Add((x, y));
                    }
                }
            }

            return velocities.Count;
        }

        private static bool IsHit(int velocityX, int velocityY, TargetArea target)
        {
            (int x, int y) position = (0, 0);

            while (position.x <= target.MaxX && position.y >= target.MinY)
            {
                position = (position.x + velocityX, position.y + velocityY);

                velocityY--;
                if (velocityX > 0)
                {
                    velocityX--;
                }

                if (position.x >= target.MinX && position.x <= target.MaxX && position.y >= target.MinY && position.y <= target.MaxY)
                {
                    return true;
                }
            }

            return false;
        }

        private static TargetArea GetTargetArea(StreamReader reader)
        {
            var re = new Regex(@"x=(\d+)..(\d+), y=([-]\d+)..([-]\d+)");
            var line = reader.ReadLine();
            var match = re.Matches(line)[0];
            var minX = Convert.ToInt32(match.Groups[1].Value);
            var maxX = Convert.ToInt32(match.Groups[2].Value);
            var minY = Convert.ToInt32(match.Groups[3].Value);
            var maxY = Convert.ToInt32(match.Groups[4].Value);

            return new TargetArea(minX, maxX, minY, maxY);
        }
    }
}