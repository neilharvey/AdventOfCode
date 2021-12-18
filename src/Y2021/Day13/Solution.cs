using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2021.Day13
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
        {
            var dots = new HashSet<Point>();

            while (reader.TryReadLine(out string line))
            {
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                var coords = line.AsIntegers();
                dots.Add(new Point(coords[0], coords[1]));
            }

            while (reader.TryReadLine(out string line))
            {
                var axis = line[11];
                var value = Convert.ToInt32(line[13..]);

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

                break; // part 1
            }

            return dots.Count;
        }

        public long Part2(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
