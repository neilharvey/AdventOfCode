using Day4;

static List<Board> ReadBoards(string[] input)
{
    var boards = new List<Board>();

    // Each board starts with a blank line
    for (var i = 2; i < input.Length; i++)
    {
        var board = new Board();

        for (var row = 0; row < Board.Size; row++)
        {
            var rowNumbers = input[i].AsIntegers(" ");
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

static List<int> ReadNumbers(string[] input)
{
    return input[0]
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => Convert.ToInt32(x))
            .ToList();
}

static List<Board> PlayBingo(List<int> numbers, List<Board> boards)
{
    var winningOrder = new List<Board>();

    foreach (var number in numbers)
    {
        foreach (var board in boards)
        {
            if (!board.HasBingo())
            {
                board.SetSeen(number);
                if (board.HasBingo())
                {
                    winningOrder.Add(board);
                }
            }
        }
    }

    return winningOrder;
}

var input = File.ReadAllLines(args.First());
var numbers = ReadNumbers(input);
var boards = ReadBoards(input);

var winningOrder = PlayBingo(numbers, boards);

Console.WriteLine($"Part One: {winningOrder.First().Score()}");
Console.WriteLine($"Part Two: {winningOrder.Last().Score()}");