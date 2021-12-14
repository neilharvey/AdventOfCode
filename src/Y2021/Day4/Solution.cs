namespace AdventOfCode.Y2021.Day4;

public class Solution : IPuzzleSolution
{
    public long Part1(StreamReader reader)
    {
        var numbers = reader.ReadLine().AsIntegers();
        var boards = GetBoards(reader);

        foreach (var number in numbers)
        {
            foreach (var board in boards)
            {
                board.SetSeen(number);
                if (board.HasBingo())
                {
                    return board.Score();
                }
            }
        }

        return 0;
    }

    public long Part2(StreamReader reader)
    {
        var numbers = reader.ReadLine().AsIntegers();
        var boards = GetBoards(reader);
        var lastWinner = boards[0];

        foreach (var number in numbers)
        {
            foreach (var board in boards)
            {
                if (!board.HasBingo())
                {
                    board.SetSeen(number);
                    if (board.HasBingo())
                    {
                        lastWinner = board;
                    }
                }
            }
        }

        return lastWinner.Score();
    }

    private static List<Board> GetBoards(StreamReader reader)
    {
        var boards = new List<Board>();

        // Each board starts with a blank line
        while (reader.TryReadLine(out string _))
        {
            var board = new Board();

            for (var row = 0; row < Board.Size; row++)
            {
                var rowNumbers = reader.ReadLine().AsIntegers(" ");
                for (var col = 0; col < Board.Size; col++)
                {
                    board[(row * Board.Size) + col] = rowNumbers[col];
                }
            }

            boards.Add(board);
        }

        return boards;
    }
}
