using System.Text.RegularExpressions;
using Day22;

static long CalculateLitCubes(string[] lines, int? region)
{
    var instructions = ReadRebootInstructions(lines);

    if (region.HasValue)
    {
        instructions = instructions
            .Where(x => x.cuboid.MinX >= -region && x.cuboid.MaxX <= region)
            .Where(x => x.cuboid.MinY >= -region && x.cuboid.MaxY <= region)
            .Where(x => x.cuboid.MinZ >= -region && x.cuboid.MaxZ <= region)
            .ToList();
    }

    var cuboids = new List<(bool state, Cuboid cuboid)>();

    foreach (var instruction in instructions)
    {
        var intersections = cuboids
            .Select(x => Intersect(instruction.cuboid, x.cuboid, currentState: x.state))
            .Where(i => i.cuboid.MinX <= i.cuboid.MaxX)
            .Where(i => i.cuboid.MinY <= i.cuboid.MaxY)
            .Where(i => i.cuboid.MinZ <= i.cuboid.MaxZ)
            .ToList();

        cuboids.AddRange(intersections);

        if (instruction.state)
        {
            cuboids.Add(instruction);
        }
    }

    return cuboids.Sum(x => Volume(x.cuboid) * (x.state ? 1 : -1));
}

static long Volume(Cuboid cuboid)
{
    // Add one to each dimension because measurements are inclusive.
    // e.g. (on x=10..12,y=10..12,z=10..12) turns on a 3x3x3 cuboid
    return 1L * (cuboid.MaxX - cuboid.MinX + 1) * (cuboid.MaxY - cuboid.MinY + 1) * (cuboid.MaxZ - cuboid.MinZ + 1);
}

static (bool state, Cuboid cuboid) Intersect(Cuboid a, Cuboid b, bool currentState)
{
    var intersection = new Cuboid(
        Math.Max(a.MinX, b.MinX),
        Math.Min(a.MaxX, b.MaxX),
        Math.Max(a.MinY, b.MinY),
        Math.Min(a.MaxY, b.MaxY),
        Math.Max(a.MinZ, b.MinZ),
        Math.Min(a.MaxZ, b.MaxZ));

    return (!currentState, intersection);
}

static List<(bool state, Cuboid cuboid)> ReadRebootInstructions(string[] lines)
{
    var steps = new List<(bool, Cuboid)>();

    foreach(var line in lines)
    {
        var match = Regex.Match(line, @"(\w+) x=(-?\d+)..(-?\d+),y=(-?\d+)..(-?\d+),z=(-?\d+)..(-?\d+)");
        var state = match.Groups[1].Value == "on";
        var minX = Convert.ToInt32(match.Groups[2].Value);
        var maxX = Convert.ToInt32(match.Groups[3].Value);
        var minY = Convert.ToInt32(match.Groups[4].Value);
        var maxY = Convert.ToInt32(match.Groups[5].Value);
        var minZ = Convert.ToInt32(match.Groups[6].Value);
        var maxZ = Convert.ToInt32(match.Groups[7].Value);
        steps.Add((state, new Cuboid(minX, maxX, minY, maxY, minZ, maxZ)));
    }

    return steps;
}

var lines = File.ReadAllLines(args[0]);
Console.WriteLine($"Part One: {CalculateLitCubes(lines, 50)}");
Console.WriteLine($"Part Two: {CalculateLitCubes(lines, default)}");