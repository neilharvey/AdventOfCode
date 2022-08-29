namespace AdventOfCode.Y2021.Day25
{
    public class Solution : IPuzzleSolution
    {
        private const char Empty = '.';
        private const char East = '>';
        private const char South = 'v';

        public long Part1(StreamReader reader)
        {
            var cucumers = ReadCucumbers(reader);
            var step = 0;
            int movements;
            do
            {
                movements = 0;
                step++;
                movements += MoveEast(cucumers);
                movements += MoveSouth(cucumers);
            }
            while (movements != 0);
            return step;
        }

        public long Part2(StreamReader reader)
        {
            return 0;
        }

        private char[,] ReadCucumbers(StreamReader reader)
        {
            var lines = new List<string>();
            while (reader.TryReadLine(out string line))
            {
                lines.Add(line);
            }

            var matrix = new char[lines[0].Length, lines.Count];
            for (var row = 0; row < lines.Count; row++)
            {
                for (var col = 0; col < lines[row].Length; col++)
                {
                    matrix[col, row] = lines[row][col];
                }
            }

            return matrix;
        }

        private int MoveEast(char[,] cucumbers)
        {
            var movements = new List<(int x, int y)>();
            for (var x = 0; x < cucumbers.GetLength(0); x++)
            {
                for (var y = 0; y < cucumbers.GetLength(1); y++)
                {
                    if (cucumbers[x, y] == East && cucumbers[(x + 1) % cucumbers.GetLength(0), y] == Empty)
                    {
                        movements.Add((x, y));
                    }
                }
            }

            foreach (var movement in movements)
            {
                cucumbers[movement.x, movement.y] = Empty;
                cucumbers[(movement.x + 1) % cucumbers.GetLength(0), movement.y] = East;
            }

            return movements.Count();
        }

        private int MoveSouth(char[,] cucumbers)
        {
            var movements = new List<(int x, int y)>();
            for (var x = 0; x < cucumbers.GetLength(0); x++)
            {
                for (var y = 0; y < cucumbers.GetLength(1); y++)
                {
                    if (cucumbers[x, y] == South && cucumbers[x, (y + 1) % cucumbers.GetLength(1)] == Empty)
                    {
                        movements.Add((x, y));
                    }
                }
            }

            foreach (var movement in movements)
            {
                cucumbers[movement.x, movement.y] = Empty;
                cucumbers[movement.x, (movement.y + 1) % cucumbers.GetLength(1)] = South;
            }

            return movements.Count();
        }

        private void WriteCucumbers(char[,] cucumbers)
        {
            for (var y = 0; y < cucumbers.GetLength(1); y++)
            {
                for (var x = 0; x < cucumbers.GetLength(0); x++)
                {
                    Console.Write(cucumbers[x, y]);
                }

                Console.WriteLine();
            }
        }
    }
}