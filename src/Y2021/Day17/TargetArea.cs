using System;

namespace AdventOfCode.Y2021.Day17;

public readonly record struct TargetArea
{
    public TargetArea(int minX, int maxX, int minY, int maxY)
    {
        MinX = minX;
        MinY = minY;
        MaxX = maxX;
        MaxY = maxY;
    }

    public int MinX { get; }

    public int MaxX { get; }

    public int MinY { get; }

    public int MaxY { get; }
}
