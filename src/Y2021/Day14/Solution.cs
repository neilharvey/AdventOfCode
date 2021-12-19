using System.Text;

namespace AdventOfCode.Y2021.Day14
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
        {
            var template = reader.ReadLine();
            reader.ReadLine();
            var rules = new Dictionary<string, char>();
            while (reader.TryReadLine(out string line))
            {
                var pair = line.Split(" -> ");
                rules.Add(pair[0], pair[1][0]);
            }

            var sb = new StringBuilder(template);
            for (var step = 1; step <= 10; step++)
            {
                var insertions = new List<(int, char)>();

                for (var i = 0; i < sb.Length - 1; i++)
                {
                    var token = sb.ToString().Substring(i, 2);
                    if (rules.ContainsKey(token))
                    {
                        insertions.Add((i + 1, rules[token]));
                    }
                }

                for(var i = 0; i< insertions.Count; i++)
                {
                    var insertion = insertions[i];
                    sb.Insert(insertion.Item1 + i, insertion.Item2);
                }
            }

            var result = sb
                .ToString()
                .GroupBy(x => x)
                .Select(x => new { Element = x.Key, Count = x.LongCount() })
                .OrderBy(x => x.Count)
                .Select(x => x.Count)
                .ToList();

            return result[^1] - result[0];
        }

        public long Part2(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
