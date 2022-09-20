static long PilotSubmarine(string[] commands, bool verticalControlsAim)
{
    var horizontal = 0;
    var depth = 0;
    var aim = 0;

    foreach (var command in commands)
    {
        var separator = command.IndexOf(" ");
        var direction = command[..separator];
        var value = int.Parse(command[(separator + 1)..]);

        switch (direction)
        {
            case "forward":
                horizontal += value;
                if (verticalControlsAim)
                {
                    depth += (aim * value);
                }
                break;
            case "down":
                if (verticalControlsAim)
                {
                    aim += value;
                }
                else
                {
                    depth += value;
                }
                break;
            case "up":
                if (verticalControlsAim)
                {
                    aim -= value;
                }
                else
                {
                    depth -= value;
                }
                break;
        }
    }

    return horizontal * depth;
}

var commands = File.ReadAllLines(args.First());
Console.WriteLine($"Part One: {PilotSubmarine(commands, false)}");
Console.WriteLine($"Part Two: {PilotSubmarine(commands, true)}");