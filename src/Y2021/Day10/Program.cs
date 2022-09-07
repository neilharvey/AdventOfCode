char[] openBraces = new char[] { '(', '[', '{', '<' };
char[] closeBraces = new char[] { ')', ']', '}', '>' };
int[] autocompleteScores = new int[] { 1, 2, 3, 4 };
int[] illegalScores = new int[] { 3, 57, 1197, 25137 };

long Part1(string[] lines)
{
    var score = 0;
    foreach (var line in lines)
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

long Part2(string[] lines)
{
    var scores = new List<long>();

    foreach (var line in lines)
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

    return scores.Median();
}

var lines = File.ReadAllLines(args.First());
Console.WriteLine($"Part One: {Part1(lines)}");
Console.WriteLine($"Part Two: {Part2(lines)}");