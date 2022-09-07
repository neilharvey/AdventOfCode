using Day21;

static long Part1(string[] lines)
{
    var game = ReadStartPositions(lines);

    var turn = 0;
    var dice = 6;

    while (!game.IsFinished(1000))
    {
        var player = turn % 2;
        game = game.Move(player, dice);
        dice += 9;
        turn++;
    }

    return Math.Min(game.Player1Score, game.Player2Score) * (turn * 3);
}

static long Part2(string[] lines)
{
    var start = ReadStartPositions(lines);
    var gamesInProgress = new Dictionary<GameState, long>
    {
        { start, 1 }
    };

    var distribution = new Dictionary<int, int>
    {
        { 3, 1 },
        { 4, 3 },
        { 5, 6 },
        { 6, 7 },
        { 7, 6 },
        { 8, 3 },
        { 9, 1 }
    };

    var scores = new long[] { 0, 0 };
    var player = 0;

    while (gamesInProgress.Any())
    {
        var newStates = new Dictionary<GameState, long>();
        foreach (var game in gamesInProgress.Keys)
        {
            foreach (var roll in distribution.Keys)
            {
                var newState = game.Move(player, roll);
                var amount = distribution[roll] * gamesInProgress[game];
                if (newState.IsFinished(21))
                {
                    scores[player] += amount;
                }
                else
                {
                    newStates.TryAdd(newState, 0);
                    newStates[newState] += amount;
                }
            }
        }

        gamesInProgress = newStates;
        player = 1 - player;
    }

    return scores.Max();
}

static GameState ReadStartPositions(string[] lines)
{
    var player1 = Convert.ToInt32(lines[0][28..]);
    var player2 = Convert.ToInt32(lines[1][28..]);
    return new GameState(player1, 0, player2, 0);
}

var lines = File.ReadAllLines(args[0]);
Console.WriteLine($"Part One {Part1(lines)}");
Console.WriteLine($"Part Two {Part2(lines)}");