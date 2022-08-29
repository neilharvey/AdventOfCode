static long SimulateLanternfish(IEnumerable<int> initialState, int daysToSimulate)
{
    var fish = new long[9];
    foreach (var value in initialState)
    {
        fish[value]++;
    }

    for (var day = 0; day < daysToSimulate; day++)
    {
        var births = fish[0];
        for (var i = 1; i < 9; i++)
        {
            fish[i - 1] = fish[i];
        }
        fish[6] += births;
        fish[8] = births;
    }

    return fish.Sum();
}

var lines = File.ReadAllLines(args.First());
var state = lines[0].AsIntegers();
Console.WriteLine($"Part One: {SimulateLanternfish(state, 80)}");
Console.WriteLine($"Part Two: {SimulateLanternfish(state, 256)}");