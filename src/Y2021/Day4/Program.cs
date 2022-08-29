using System.IO;

static long Part1(string[] lines)
{
    var numbers = lines[0].AsIntegers();
    var boards = GetBoards(lines);

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

static long Part2(string[] lines)
{
    var numbers = lines[0].AsIntegers();
    var boards = GetBoards(lines);
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

static List<Board> GetBoards(string[] lines)
{
    var boards = new List<Board>();

    // Each board starts with a blank line
    for (var i = 2; i < lines.Length; i++)
    {
        var board = new Board();

        for (var row = 0; row < Board.Size; row++)
        {
            var rowNumbers = lines[i].AsIntegers(" ");
            for (var col = 0; col < Board.Size; col++)
            {
                board[(row * Board.Size) + col] = rowNumbers[col];
            }

            i++;
        }

        boards.Add(board);
    }

    return boards;
}

var lines = File.ReadAllLines(args.First());
Console.WriteLine($"Part One: {Part1(lines)}");
Console.WriteLine($"Part Two: {Part2(lines)}");