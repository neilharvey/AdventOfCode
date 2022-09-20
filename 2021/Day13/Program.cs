using Day13;

static long Part1(string[] lines)
{
    var dots = FoldOrigami(lines, singleFold: true);
    return dots.Count;
}


static long Part2(string[] lines)
{
    var dots = FoldOrigami(lines, singleFold: false);
    GetActivationCode(dots);
    return 0;
}

static void GetActivationCode(HashSet<Point> dots)
{
    var maxX = dots.Max(x => x.X);
    var maxY = dots.Max(x => x.Y);

    var grid = new bool[maxX + 1, maxY + 1];
    foreach (var dot in dots)
    {
        grid[dot.X, dot.Y] = true;
    }

    Console.ForegroundColor = ConsoleColor.Green;
    for (var y = 0; y <= maxY; y++)
    {
        for (var x = 0; x <= maxX; x++)
        {
            Console.Write(grid[x, y] ? "#" : " ");
        }
        Console.WriteLine();
    }
    Console.ResetColor();
}

static HashSet<Point> FoldOrigami(string[] lines, bool singleFold)
{
    var dots = new HashSet<Point>();
    var i = 0;

    for (; i < lines.Length; i++)
    {
        if (string.IsNullOrEmpty(lines[i]))
        {
            break;
        }

        var coords = lines[i].Split(",");
        dots.Add(new Point(Convert.ToInt32(coords[0]), Convert.ToInt32(coords[1])));
    }

    i++;

    for(; i< lines.Length; i++)
    { 
        var axis = lines[i][11];
        var value = Convert.ToInt32(lines[i][13..]);

        var dotsToMove = dots
            .Where(d => d.Y > value || axis == 'x')
            .Where(d => d.X > value || axis == 'y')
            .ToList();

        foreach (var dot in dotsToMove)
        {
            dots.Remove(dot);
            Point newPosition;
            if (axis == 'x')
            {
                newPosition = new Point(value - (dot.X - value), dot.Y);
            }
            else
            {
                newPosition = new Point(dot.X, value - (dot.Y - value));
            }
            var added = dots.Add(newPosition);
        }

        if (singleFold)
        {
            break;
        }
    }    

    return dots;
}

var lines = File.ReadAllLines(args[0]);
Console.WriteLine($"Part One: {Part1(lines)}");
Console.WriteLine($"Part Two:");
Part2(lines);