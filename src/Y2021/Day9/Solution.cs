namespace AdventOfCode.Y2021.Day9;

public class Solution : IPuzzleSolution
{
    private delegate long Calculation(int[,] heightmap, List<(int x, int y)> lowpoints);

    public long Part1(StreamReader reader)
    {
        var heightmap = reader.ReadMatrix();
        var lowpoints = FindLowPoints(heightmap);

        return lowpoints
            .Select(t => 1 + heightmap[t.x, t.y])
            .Sum();
    }

    public long Part2(StreamReader reader)
    {
        var heightmap = reader.ReadMatrix();
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

    private static List<(int x, int y)> FindLowPoints(int[,] heightmap)
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
}
