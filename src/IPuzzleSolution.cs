namespace AdventOfCode
{
    public interface IPuzzleSolution
    {
        DateOnly PuzzleDate { get; }

        int Part1(StreamReader reader);

        int Part2(StreamReader reader);
    }
}
