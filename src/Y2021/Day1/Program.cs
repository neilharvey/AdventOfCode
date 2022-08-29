long Part1(string[] lines)
{
    int? previousDepth = null;
    int currentDepth;
    var increases = 0;

    foreach (var line in lines)
    {
        currentDepth = int.Parse(line);
        if (previousDepth.HasValue && currentDepth > previousDepth)
        {
            increases++;
        }
        previousDepth = currentDepth;
    }

    return increases;
}

long Part2(string[] lines)
{
    var increases = 0;
    var window = new Queue<int>(3);

    for (var i = 0; i < 3; i++)
    {
        var depth = int.Parse(lines[i]);
        window.Enqueue(depth);
    }

    for (var i = 3; i < lines.Length; i++)
    {
        var line = lines[i];
        var previousDepth = window.Dequeue();
        var currentDepth = int.Parse(line);
        window.Enqueue(currentDepth);
        if (currentDepth > previousDepth)
        {
            increases++;
        }
    }

    return increases;
}

var lines = File.ReadAllLines(args.First());
Console.WriteLine($"Part One: {Part1(lines)}");
Console.WriteLine($"Part Two: {Part2(lines)}");