namespace Y2021.Day1;

public class Solver
{
    public int Part1(StreamReader reader)
    {
        int? previousDepth = null;
        int currentDepth;
        int increases = 0;
        var line = reader.ReadLine();
        while(line != null)
        {
            currentDepth = int.Parse(line);
            if(previousDepth.HasValue && currentDepth > previousDepth)
            {
                increases++;
            }
            previousDepth = currentDepth;
            line = reader.ReadLine();
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

        var line = reader.ReadLine();
        while (line != null)
        {
            var previousDepth = window.Dequeue();
            var currentDepth = int.Parse(line);
            window.Enqueue(currentDepth);
            line = reader.ReadLine();
            if (currentDepth > previousDepth)
            {
                increases++;
            }
        }

        return increases;
    }
}