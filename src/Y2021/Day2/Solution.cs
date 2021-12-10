namespace AdventOfCode.Y2021.Day2;

public class Solution : IPuzzleSolution
{
    public DateOnly PuzzleDate => new(2021, 12, 2);

    public int Part1(StreamReader reader)
    {
        var horizontal = 0;
        var depth = 0;

        while(reader.TryReadLine(out string line))
        {
            var separator = line.IndexOf(" ");
            var command = line[..separator];
            var value = int.Parse(line[(separator + 1)..]);
            switch(command)
            {
                case "forward" : horizontal += value; break;
                case "down" : depth += value; break;
                case "up" : depth -= value; break;
            }
        }

        return horizontal * depth;
    }

    public int Part2(StreamReader reader)
    {
        var horizontal = 0;
        var depth = 0;
        var aim = 0;

        while(reader.TryReadLine(out string line))
        {
            var separator = line.IndexOf(" ");
            var command = line[..separator];
            var value = int.Parse(line[(separator + 1)..]);
            switch(command)
            {
                case "forward": 
                    horizontal += value; 
                    depth += (aim * value);
                break;
                case "down": 
                    aim += value; 
                    break;
                case "up": 
                    aim -= value; 
                    break;
            }
        }

        return horizontal * depth;
    }
}