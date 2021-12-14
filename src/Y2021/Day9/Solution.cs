namespace AdventOfCode.Y2021.Day9;

public class Solution : IPuzzleSolution
{
    public long Part1(StreamReader reader)
    {
        string prev = null;
        string current = reader.ReadLine();
        string next = reader.ReadLine();

        var risk = 0;

        while (current != null)
        {
            for (var i = 0; i < current.Length; i++)
            {
                var up = prev == null || current[i] < prev[i];
                var down = next == null || current[i] < next[i];
                var left = i == 0 || current[i] < current[i - 1];
                var right = i == current.Length - 1 || current[i] < current[i + 1];

                if (up && down && left && right)
                {
                    risk += 1 + (current[i] - '0');
                }
            }

            prev = current;
            current = next;
            next = reader.ReadLine();
        }

        return risk;
    }

    public long Part2(StreamReader reader)
    {
        return 0;
    }
}
