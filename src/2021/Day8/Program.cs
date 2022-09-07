static long Part1(string[] lines)
{
    var total = 0;

    foreach(var line in lines)
    {
        total += line
            .Split('|')
            .ElementAt(1)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(signal => signal.Length)
            .Where(length => new int[] { 2, 4, 3, 7 }.Contains(length))
            .Count();
    }

    return total;
}

static long Part2(string[] lines)
{
    var total = 0;

    foreach(var line in lines)
    {
        var patterns = line
            .Split('|')
            .SelectMany(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(x => new string(x.OrderBy(c => c).ToArray()))
            .ToList();

        var map = GetSegmentMap(patterns);

        var output = patterns
           .Skip(patterns.Count - 4)
           .Take(4)
           .Select(x => map[x])
           .ToList();

        var value = (1000 * output[0]) + (100 * output[1]) + (10 * output[2]) + output[3];
        total += value;
    }

    return total;
}

static Dictionary<string, int> GetSegmentMap(List<string> patterns)
{
    var patternsByLength = patterns
        .OrderBy(x => x.Length)
        .ToList();

    var map = new string[10];

    foreach (var pattern in patternsByLength)
    {
        switch (pattern.Length)
        {
            case 2:
                map[1] = pattern;
                break;
            case 3:
                map[7] = pattern;
                break;
            case 4:
                map[4] = pattern;
                break;
            case 5: // 2, 3 or 5
                if (pattern.Intersect(map[7]).Count() == 3)
                {
                    map[3] = pattern;
                }
                else if (pattern.Intersect(map[4]).Count() == 2)
                {
                    map[2] = pattern;
                }
                else // 5 intersect 4 = 3
                {
                    map[5] = pattern;
                }
                break;
            case 6: // 0, 6, or 9
                if (pattern.Intersect(map[4]).Count() == 4)
                {
                    map[9] = pattern;
                }
                else if (pattern.Intersect(map[7]).Count() == 3)
                {
                    map[0] = pattern;
                }
                else
                {
                    map[6] = pattern;
                }
                break;
            case 7:
                map[8] = pattern;
                break;
        }
    }

    return map
        .Select((x, i) => new { Value = i, Pattern = x })
        //.Where(x => x.Pattern != null)
        .ToDictionary(x => x.Pattern, x => x.Value);
}

var lines = File.ReadAllLines(args.First());
Console.WriteLine($"Part One: {Part1(lines)}");
Console.WriteLine($"Part Two: {Part2(lines)}");