namespace AdventOfCode.Y2021.Day6
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
            => SimulateLanternfish(reader, 80);

        public long Part2(StreamReader reader)
            => SimulateLanternfish(reader, 256);
    
        private static long SimulateLanternfish(StreamReader reader, int daysToSimulate)
        {
            var initialState = reader.ReadLine().AsIntegers();
            var fish = new long[9];
            foreach (var value in initialState)
            {
                fish[value]++;
            }

            for (var day = 0; day < daysToSimulate; day++)
            {
                var births = fish[0];
                for (var i = 1; i < 9; i++)
                {
                    fish[i - 1] = fish[i];
                }
                fish[6] += births;
                fish[8] = births;
            }

            return fish.Sum();
        }
    }
}
