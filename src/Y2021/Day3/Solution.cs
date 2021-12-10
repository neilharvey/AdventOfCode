namespace AdventOfCode.Y2021.Day3
{
    public class Solution : IPuzzleSolution
    {
        public int Part1(StreamReader reader)
        {
            var diagnostics = ReadLines(reader);
            var gammaMask = BitMask(diagnostics, most: true);
            var gammaRate = Convert.ToInt32(gammaMask, 2);
            var epsilonMask = BitMask(diagnostics, most: false);
            var episonRate = Convert.ToInt32(epsilonMask, 2);

            return gammaRate * episonRate;
        }

        public int Part2(StreamReader reader)
        {
            var diagnostics = ReadLines(reader);
            var oxygenRatingMask = FilterDiagnostics(diagnostics, most: true);
            var oxygenRating = Convert.ToInt32(oxygenRatingMask, 2);
            var scrubberRatingMask = FilterDiagnostics(diagnostics, most: false);
            var scrubberRating = Convert.ToInt32(scrubberRatingMask, 2);

            return oxygenRating * scrubberRating;
        }

        private static string FilterDiagnostics(IList<string> diagnostics, bool most)
        {
            var size = diagnostics[0].Length;
            var index = 0;

            var filter = new List<string>(diagnostics);
            while (index < size && filter.Count > 1)
            {
                var mask = BitMask(diagnostics, most);
                filter = filter.Where(l => l[index] == mask[index]).ToList();
                Console.WriteLine($"{mask} [{index}] {mask[index]} {filter.Count} remaining most {most}");
                index++;
            }

            return filter[0];
        }

        private static IList<string> ReadLines(StreamReader reader)
        {
            var lines = new List<string>();
            while (reader.TryReadLine(out string line))
            {
                lines.Add(line);
            }

            return lines;
        }

        private static string BitMask(IList<string> lines, bool most)
        {
            var size = lines[0].Length;
            var ones = new int[size];

            foreach (var line in lines)
            {
                for (var i = 0; i < size; i++)
                {
                    ones[i] += line[i] - '0';
                }
            }

            var half = lines.Count / 2D;
            var tieBreak = most ? 1 : 0;
            var m = string.Join("", ones.Select(x => x == half ? tieBreak : x > half ? 1 : 0));
            var l = string.Join("", ones.Select(x => x == half ? tieBreak : x > half ? 0 : 1));
            return most ? m : l;
        }
    }
}
