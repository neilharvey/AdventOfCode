namespace AdventOfCode.Y2021.Day10;

public class Solution : IPuzzleSolution
{
    private readonly char[] openBraces = new char[] { '(', '[', '{', '<' };
    private readonly char[] closeBraces = new char[] { ')', ']', '}', '>' };
    private readonly int[] autocompleteScores = new int[] { 1, 2, 3, 4 };
    private readonly int[] illegalScores = new int[] { 3, 57, 1197, 25137 };

    public long Part1(StreamReader reader)
    {
        var score = 0;
        while (reader.TryReadLine(out string line))
        {
            var stack = new Stack<char>();
            for (var i = 0; i < line.Length; i++)
            {
                var found = line[i];
                if (openBraces.Contains(found))
                {
                    stack.Push(found);
                    continue;
                }

                var opener = stack.Pop();
                var index = Array.IndexOf(openBraces, opener);
                var expected = closeBraces[index];
                if (expected != found)
                {
                    var illegal = Array.IndexOf(closeBraces, found);
                    score += illegalScores[illegal];
                    break;
                }
            }
        }

        return score;
    }

    public long Part2(StreamReader reader)
    {
        var scores = new List<long>();

        while (reader.TryReadLine(out string line))
        {
            var stack = new Stack<char>();

            for (var i = 0; i < line.Length; i++)
            {
                var found = line[i];
                if (openBraces.Contains(found))
                {
                    stack.Push(found);
                    continue;
                }
   
                var opener = stack.Pop();
                var index = Array.IndexOf(openBraces, opener);
                var expected = closeBraces[index];
                if (expected != found)
                {
                    stack.Clear();
                    break;
                }
            }

            if (stack.Any())
            {
                var score = 0L;
                while (stack.Any())
                {
                    var opener = stack.Pop();
                    var index = Array.IndexOf(openBraces, opener);
                    var points = autocompleteScores[index];
                    score = (score * 5) + points;
                }
                scores.Add(score);
            }
        }

        var sorted = scores.OrderBy(x => x).ToList();
        var mid = (sorted.Count - 1) / 2D;
        var median = (sorted[(int)mid] + sorted[(int)(mid + 0.5)]) / 2;
        return median;
    }
}
