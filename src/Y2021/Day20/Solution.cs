using System.Text;

namespace AdventOfCode.Y2021.Day20
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
            => EnhanceImage(reader, 2);

        public long Part2(StreamReader reader)
            => EnhanceImage(reader, 50);

        private static int EnhanceImage(StreamReader reader, int steps)
        {
            var algorithm = reader.ReadLine();
            _ = reader.ReadLine(); // blank
            var image = ReadImage(reader);
            var infinitePixelState = false;

            for (var i = 0; i < steps; i++)
            {
                image = ApplyEnhancementAlgorithm(image, algorithm, infinitePixelState);

                // If the infinite pixels are off and the algorith flips an empty square to on, or they are all on
                // and the algorithm flips a full square to off then we need to flip the state of the infinite pixels.
                if ((algorithm.First() == '#' && infinitePixelState == false) || (algorithm.Last() != '#' && infinitePixelState == true))
                {
                    infinitePixelState = !infinitePixelState;
                }
            }

            return image.Count;
        }

        private static HashSet<(int, int)> ApplyEnhancementAlgorithm(HashSet<(int x, int y)> image, string algorithm, bool infinitePixelState)
        {
            var minx = image.MinBy(p => p.x).x;
            var miny = image.MinBy(p => p.y).y;
            var maxx = image.MaxBy(p => p.x).x;
            var maxy = image.MaxBy(p => p.y).y;

            var enhanced = new HashSet<(int x, int y)>();

            for (var y = miny - 1; y <= maxy + 1; y++)
            {
                for (var x = minx - 1; x <= maxx + 1; x++)
                {
                    var bit = 8;
                    var index = 0;

                    for (var dy = y - 1; dy <= y + 1; dy++)
                    {
                        for (var dx = x - 1; dx <= x + 1; dx++)
                        {
                            // We model the infinite space around the image separately, either all pixels are on or off
                            // So when checking an out of bound pixel use the special 'infinite state' instead.
                            if (image.Contains((dx, dy)) || (infinitePixelState && (dx < minx || dy < miny || dx > maxx || dy > maxy)))
                            {
                                index += (int)Math.Pow(2, bit);
                            }

                            bit--;
                        }
                    }

                    if (algorithm[index] == '#')
                    {
                        enhanced.Add((x, y));
                    }
                }
            }

            return enhanced;
        }

        private static HashSet<(int, int)> ReadImage(StreamReader reader)
        {
            var image = new HashSet<(int, int)>();
            var y = 0;

            while (reader.TryReadLine(out string line))
            {
                for (var x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                    {
                        image.Add((x, y));
                    }
                }

                y++;
            }

            return image;
        }
    }
}
