using System.Text.RegularExpressions;

namespace AdventOfCode.Y2021.Day22
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
            => CalculateLitCubes(reader, 50);

        public long Part2(StreamReader reader)
            => CalculateLitCubes(reader, default);

        private static long CalculateLitCubes(StreamReader reader, int? region)
        {
            var instructions = ReadRebootInstructions(reader);

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

        private static long Volume(Cuboid cuboid)
        {
            // Add one to each dimension because measurements are inclusive.
            // e.g. (on x=10..12,y=10..12,z=10..12) turns on a 3x3x3 cuboid
            return 1L * (cuboid.MaxX - cuboid.MinX + 1) * (cuboid.MaxY - cuboid.MinY + 1) * (cuboid.MaxZ - cuboid.MinZ + 1);
        }

        private static (bool state, Cuboid cuboid) Intersect(Cuboid a, Cuboid b, bool currentState)
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

        public static List<(bool state, Cuboid cuboid)> ReadRebootInstructions(StreamReader reader)
        {
            var steps = new List<(bool, Cuboid)>();

            while (reader.TryReadLine(out string line))
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
    }
}
