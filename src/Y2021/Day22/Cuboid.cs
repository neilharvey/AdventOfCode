namespace AdventOfCode.Y2021.Day22
{
    public readonly record struct Cuboid
    {
        public Cuboid(int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
            MinZ = minZ;
            MaxZ = maxZ;
        }

        public int MinX { get; }

        public int MaxX { get; }

        public int MinY { get; }

        public int MaxY { get; }

        public int MinZ { get; }

        public int MaxZ { get; }

        public override string ToString()
        {
            return $"x={MinX}..{MaxX},y={MinY}..{MaxY},z={MinZ}..{MaxZ}";
        }
    }
}
