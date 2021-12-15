namespace AdventOfCode;

public static class StreamReaderExtensions
{
    public static bool TryReadLine(this StreamReader reader, out string line)
    {
        line = reader.ReadLine()!;
        return line != null;
    }

    public static int[,] ReadMatrix(this StreamReader reader)
    {
        var lines = new List<string>();
        while (reader.TryReadLine(out string line))
        {
            lines.Add(line);
        }

        var matrix = new int[lines[0].Length, lines.Count];
        for (var row = 0; row < lines.Count; row++)
        {
            for (var col = 0; col < lines[row].Length; col++)
            {
                matrix[col, row] = lines[row][col] - '0';
            }
        }

        return matrix;
    }
}
