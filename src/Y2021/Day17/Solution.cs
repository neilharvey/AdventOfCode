using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Y2021.Day17
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
        {
            var re = new Regex(@"x=(\d+)..(\d+), y=([-]\d+)..([-]\d+)");
            var line = reader.ReadLine();
            var match = re.Matches(line)[0];
            var minX = Convert.ToInt32(match.Groups[1].Value);
            var maxX = Convert.ToInt32(match.Groups[2].Value);
            var minY = Convert.ToInt32(match.Groups[3].Value);
            var maxY = Convert.ToInt32(match.Groups[4].Value);

            var velocityY = Math.Abs(minY) - 1;
            var maxHeight = (velocityY * (velocityY + 1)) / 2;
            return maxHeight;
        }

        public long Part2(StreamReader reader)
        {
            throw new NotImplementedException();
        }


    }
}
