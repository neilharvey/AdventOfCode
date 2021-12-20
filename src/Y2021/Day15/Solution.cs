using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2021.Day15
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
        {
            var map = reader.ReadMatrix();



            return 0;
        }

        public long Part2(StreamReader reader)
        {
            throw new NotImplementedException();
        }

        //function reconstruct_path(cameFrom, current)
        //    total_path := {current}
        //    while current in cameFrom.Keys:
        //        current := cameFrom[current]
        //        total_path.prepend(current)
        //    return total_path

        //public long AStar(int[,] map)
        //{
        //    var width = map.GetLength(0);
        //    var height = map.GetLength(1);
        //    // The set of discovered nodes that may need to be (re-)expanded.
        //    // Initially, only the start node is known.
        //    // This is usually implemented as a min-heap or priority queue rather than a hash-set.
        //    var openSet = new PriorityQueue<(int x, int y), int>();
        //    openSet.Enqueue((0, 0), 0);

        //    // For node n, cameFrom[n] is the node immediately preceding it on the cheapest path from start
        //    // to n currently known.
        //    var cameFrom = new int[width, height];

        //    // For node n, gScore[n] is the cost of the cheapest path from start to n currently known.
        //    var gScore = new int[width, height];
        //    gScore[0, 0] = 0;

        //    // For node n, fScore[n] := gScore[n] + h(n). fScore[n] represents our current best guess as to
        //    // how short a path from start to finish can be if it goes through n.
        //    var fScore = new int[width, height];
        //    fScore[0, 0] = h(0, 0);

        //    //    while openSet is not empty
        //    //        // This operation can occur in O(1) time if openSet is a min-heap or a priority queue
        //    //        current := the node in openSet having the lowest fScore[] value
        //    //        if current = goal
        //    //            return reconstruct_path(cameFrom, current)

        //    //        openSet.Remove(current)
        //    //        for each neighbor of current
        //    //            // d(current,neighbor) is the weight of the edge from current to neighbor
        //    //            // tentative_gScore is the distance from start to the neighbor through current
        //    //            tentative_gScore := gScore[current] + d(current, neighbor)
        //    //            if tentative_gScore < gScore[neighbor]
        //    //                // This path to neighbor is better than any previous one. Record it!
        //    //                cameFrom[neighbor] := current
        //    //                gScore[neighbor] := tentative_gScore
        //    //                fScore[neighbor] := tentative_gScore + h(neighbor)
        //    //                if neighbor not in openSet
        //    //                    openSet.add(neighbor)

        //    //    // Open set is empty but goal was never reached
        //    //    return failure
        //}

        //private int h(int x, int y)
        //{
        //    return 0;
        //}
    }
}
