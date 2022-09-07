using System;
using System.Collections.Generic;

static long FindAllPaths(string[] lines, Func<Cave, Path, bool> callback)
{
    var caves = new CaveSystem();

    foreach (var line in lines)
    {
        var nodes = line.Split('-');
        caves.AddConnection(nodes[0], nodes[1]);
    }

    var paths = Explore(caves.Start, new Path(), callback);

    return paths.Count;
}

static List<Path> Explore(Cave cave, Path path, Func<Cave, Path, bool> isVisited)
{
    path.Add(cave);

    var paths = new List<Path>();

    if (cave.IsEnd)
    {
        paths.Add(path);
    }
    else
    {
        foreach (var adj in cave.Adjacent)
        {
            if (!(adj.IsStart || isVisited(adj, path)))
            {
                var branch = new Path(path);
                paths.AddRange(Explore(adj, branch, isVisited));
            }
        }
    }

    return paths;
}

static bool IsSmallCaveVisitedOnce(Cave adj, Path path)
{
    return adj.IsSmall && path.Contains(adj);
}

static bool IsSmallCaveVisitedOnceAndNoOtherVisitedTwice(Cave adj, Path path)
{
    if (!adj.IsSmall)
    {
        return false;
    }

    if (!path.Contains(adj))
    {
        return false;
    }

    return path.AnySmallCaveVisitedTwice;
}

var lines = System.IO.File.ReadAllLines(args[0]);
Console.WriteLine($"Part One: {FindAllPaths(lines, IsSmallCaveVisitedOnce)}");
Console.WriteLine($"Part Two: {FindAllPaths(lines, IsSmallCaveVisitedOnceAndNoOtherVisitedTwice)}");