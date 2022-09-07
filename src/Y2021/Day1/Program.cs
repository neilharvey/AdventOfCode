static long CalculateDepthIncreases(string[] report, int windowSize)
{
    var increases = 0;
    var window = new Queue<int>(3);

    for (var i = 0; i < windowSize; i++)
    {
        var depth = int.Parse(report[i]);
        window.Enqueue(depth);
    }

    for (var i = windowSize; i < report.Length; i++)
    {
        var previousDepth = window.Dequeue();
        var currentDepth = int.Parse(report[i]);
        window.Enqueue(currentDepth);
        if (currentDepth > previousDepth)
        {
            increases++;
        }
    }

    return increases;
}

var report = File.ReadAllLines(args.First());
Console.WriteLine($"Part One: {CalculateDepthIncreases(report, 1)}");
Console.WriteLine($"Part Two: {CalculateDepthIncreases(report, 3)}");