namespace Y2021.Day2;

public class Solver
{
    public int Part1(StreamReader reader)
    {
        var horizontal = 0;
        var depth = 0;
        var line = reader.ReadLine();
        while(line != null)
        {
            var separator = line.IndexOf(" ");
            var command = line.Substring(0, separator);
            var value = int.Parse(line.Substring(separator + 1));
            switch(command)
            {
                case "forward" : horizontal += value; break;
                case "down" : depth += value; break;
                case "up" : depth -= value; break;
            }

            line = reader.ReadLine();
        }

        return horizontal * depth;
    }

    public int Part2(StreamReader reader)
    {
        var horizontal = 0;
        var depth = 0;
        var aim = 0;
        var line = reader.ReadLine();
        while(line != null)
        {
            var separator = line.IndexOf(" ");
            var command = line.Substring(0, separator);
            var value = int.Parse(line.Substring(separator + 1));
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

            line = reader.ReadLine();
        }

        return horizontal * depth;
    }
}