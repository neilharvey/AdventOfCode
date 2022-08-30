var lines = File.ReadAllLines(args.First());

var octopuses = new int[lines[0].Length, lines.Length];
for (var row = 0; row < lines.Length; row++)
{
    for (var col = 0; col < lines[row].Length; col++)
    {
        octopuses[col, row] = lines[row][col] - '0';
    }
}

var totalFlashes = 0;
var step = 0;
var isSimultaneous = false;

while (!isSimultaneous)
{
    step++;

    var flashesThisStep = 0;

    // First, the energy level of each octopus increases by 1.
    for (var x = 0; x < 10; x++)
    {
        for (var y = 0; y < 10; y++)
        {
            octopuses[x, y]++;
        }
    }

    // Then, any octopus with an energy level greater than 9 flashes.
    // This increases the energy level of all adjacent octopuses by 1, including octopuses that are diagonally adjacent.
    // If this causes an octopus to have an energy level greater than 9, it also flashes.
    // This process continues as long as new octopuses keep having their energy level increased beyond 9. (An octopus can only flash at most once per step.)
    bool hasFlashed;
    var flashes = new bool[10, 10];

    do
    {
        hasFlashed = false;

        for (var x = 0; x < 10; x++)
        {
            for (var y = 0; y < 10; y++)
            {
                if (octopuses[x, y] > 9 && !flashes[x, y])
                {
                    hasFlashed = true;
                    flashes[x, y] = true;
                    flashesThisStep++;

                    if (x > 0 && y > 0) { octopuses[x - 1, y - 1]++; }
                    if (y > 0) { octopuses[x, y - 1]++; }
                    if (x < 9 && y > 0) { octopuses[x + 1, y - 1]++; }
                    if (x < 9) { octopuses[x + 1, y]++; }
                    if (x < 9 && y < 9) { octopuses[x + 1, y + 1]++; }
                    if (y < 9) { octopuses[x, y + 1]++; }
                    if (x > 0 && y < 9) { octopuses[x - 1, y + 1]++; }
                    if (x > 0) { octopuses[x - 1, y]++; }
                }
            }
        }
    }
    while (hasFlashed);

    // Finally, any octopus that flashed during this step has its energy level set to 0, as it used all of its energy to flash.
    for (var x = 0; x < 10; x++)
    {
        for (var y = 0; y < 10; y++)
        {
            if (octopuses[x, y] > 9)
            {
                octopuses[x, y] = 0;
            }
        }
    }

    if (step <= 100)
    {
        totalFlashes += flashesThisStep;
    }

    if (flashesThisStep == 100)
    {
        isSimultaneous = true;
    }
}

Console.WriteLine($"Part One: {totalFlashes}");
Console.WriteLine($"Part Two: {step}");