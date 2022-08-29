long Part1(string[] lines)
{
    var horizontal = 0;
    var depth = 0;

    foreach(var line in lines)
    {
        var separator = line.IndexOf(" ");
        var command = line[..separator];
        var value = int.Parse(line[(separator + 1)..]);
        switch (command)
        {
            case "forward": horizontal += value; break;
            case "down": depth += value; break;
            case "up": depth -= value; break;
        }
    }

    return horizontal * depth;
}

long Part2(string[] lines)
{
    var horizontal = 0;
    var depth = 0;
    var aim = 0;

    foreach(var line in lines)
    {
        var separator = line.IndexOf(" ");
        var command = line[..separator];
        var value = int.Parse(line[(separator + 1)..]);
        switch (command)
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

var lines = File.ReadAllLines(args.First());
Console.WriteLine($"Part One: {Part1(lines)}");
Console.WriteLine($"Part Two: {Part2(lines)}");