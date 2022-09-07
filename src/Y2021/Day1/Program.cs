static long CalculateDepthIncreases(string[] lines, int windowSize)
{
    var increases = 0;
    var window = new Queue<int>(3);

    for (var i = 0; i < windowSize; i++)
    {
        var depth = int.Parse(lines[i]);
        window.Enqueue(depth);
    }

    for (var i = windowSize; i < lines.Length; i++)
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
Console.WriteLine($"Part One: {CalculateDepthIncreases(lines, 1)}");
Console.WriteLine($"Part Two: {CalculateDepthIncreases(lines, 3)}");