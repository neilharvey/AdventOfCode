static long Part1(int[,] heightmap)
{
    var lowpoints = FindLowPoints(heightmap);

    return lowpoints
        .Select(t => 1 + heightmap[t.x, t.y])
        .Sum();
}

static long Part2(int[,] heightmap)
{
    var lowpoints = FindLowPoints(heightmap);

    var basinSizes = new List<int>();
    var basins = new int[heightmap.GetLength(0), heightmap.GetLength(1)];

    foreach (var lowpoint in lowpoints)
    {
        var basinSize = 0;
        var queue = new Queue<(int, int)>();
        queue.Enqueue(lowpoint);
        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();
            if (heightmap[x, y] < 9 && basins[x, y] == 0)
            {
                basinSize++;
                basins[x, y] = 1;
                if (x > 0)
                {
                    queue.Enqueue((x - 1, y));
                }
                if (x < heightmap.GetLength(0) - 1)
                {
                    queue.Enqueue((x + 1, y));
                }
                if (y > 0)
                {
                    queue.Enqueue((x, y - 1));
                }
                if (y < heightmap.GetLength(1) - 1)
                {
                    queue.Enqueue((x, y + 1));
                }
            }
        }

        basinSizes.Add(basinSize);
    }

    var largest = basinSizes
        .OrderByDescending(x => x)
        .ToList();

    return largest[0] * largest[1] * largest[2];
}

static List<(int x, int y)> FindLowPoints(int[,] heightmap)
{
    var lowpoints = new List<(int, int)>();

    for (var x = 0; x < heightmap.GetLength(0); x++)
    {
        for (var y = 0; y < heightmap.GetLength(1); y++)
        {
            var up = x == 0 || heightmap[x, y] < heightmap[x - 1, y];
            var down = x == heightmap.GetLength(0) - 1 || heightmap[x, y] < heightmap[x + 1, y];
            var left = y == 0 || heightmap[x, y] < heightmap[x, y - 1];
            var right = y == heightmap.GetLength(1) - 1 || heightmap[x, y] < heightmap[x, y + 1];

            if (up && down && left && right)
            {
                lowpoints.Add((x, y));
            }
        }
    }

    return lowpoints;
}

static int[,] ReadHeightmap(string[] lines)
{
    var heightmap = new int[lines[0].Length, lines.Length];
    for (var row = 0; row < lines.Length; row++)
    {
        for (var col = 0; col < lines[row].Length; col++)
        {
            heightmap[col, row] = lines[row][col] - '0';
        }
    }

    return heightmap;
}

var lines = File.ReadAllLines(args.First());
var heightmap = ReadHeightmap(lines);
Console.WriteLine($"Part One: {Part1(heightmap)}");
Console.WriteLine($"Part Two: {Part2(heightmap)}");