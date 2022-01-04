namespace AdventOfCode.Y2021.Day21
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
        {
            var line1 = reader.ReadLine();
            var line2 = reader.ReadLine();
            var spaces = new int[2];
            spaces[0] = Convert.ToInt32(line1[28..]);
            spaces[1] = Convert.ToInt32(line2[28..]);

            var turn = 0;
            var rolls = 0;
            var d = 6;
            var scores = new int[2];
            while (scores[0] < 1000 && scores[1] < 1000)
            {
                var player = turn % 2;
                spaces[player] = 1 + (spaces[player] + (d - 1)) % 10;
                scores[player] += spaces[player];
               d += 9;
                turn++;
                rolls += 3;
            }

            return Math.Min(scores[0], scores[1]) * rolls;
        }

        public long Part2(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
