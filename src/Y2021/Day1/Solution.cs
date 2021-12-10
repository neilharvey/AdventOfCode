namespace AdventOfCode.Y2021.Day1;

public class Solution : IPuzzleSolution
{
    public DateOnly PuzzleDate => new(2021, 12, 1);

    public int Part1(StreamReader reader)
    {
        int? previousDepth = null;
        int currentDepth;
        int increases = 0;

        while (reader.TryReadLine(out string line))
        {
            currentDepth = int.Parse(line);
            if(previousDepth.HasValue && currentDepth > previousDepth)
            {
                increases++;
            }
            previousDepth = currentDepth;
        }

        return increases;
    }

    public int Part2(StreamReader reader)
    {
        var increases = 0;
        var window = new Queue<int>(3);
        for(var i=0; i<3; i++)
        {
            var depth = int.Parse(reader.ReadLine()!);
            window.Enqueue(depth);
        }

        while (reader.TryReadLine(out string line))
        {
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
}