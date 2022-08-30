const char Empty = '.';
const char East = '>';
const char South = 'v';

int MoveEast(char[,] cucumbers)
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

int MoveSouth(char[,] cucumbers)
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

var lines = File.ReadAllLines(args.First());
var cucumbers = new char[lines[0].Length, lines.Length];
for (var row = 0; row < lines.Length; row++)
{
    for (var col = 0; col < lines[row].Length; col++)
    {
        cucumbers[col, row] = lines[row][col];
    }
}

var step = 0;
int movements;
do
{
    movements = 0;
    step++;
    movements += MoveEast(cucumbers);
    movements += MoveSouth(cucumbers);
}
while (movements != 0);

Console.WriteLine($"Part One: {step}");
Console.WriteLine($"Part Two: ???");