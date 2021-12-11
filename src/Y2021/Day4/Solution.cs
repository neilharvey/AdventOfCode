using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Y2021.Day4;
public class Solution : IPuzzleSolution
{
    public int Part1(StreamReader reader)
    {
        var numbers = reader.ReadLine().ToListOfInt();
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

    public int Part2(StreamReader reader)
    {
        var numbers = reader.ReadLine().ToListOfInt();
        var boards = GetBoards(reader);
        var lastWinner = boards[0];

        foreach (var number in numbers)
        {
            foreach(var board in boards)
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
                var rowNumbers = reader.ReadLine().ToListOfInt(" ");
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
