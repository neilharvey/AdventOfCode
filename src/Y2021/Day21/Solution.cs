namespace AdventOfCode.Y2021.Day21
{
    public class Solution : IPuzzleSolution
    {
        public long Part1(StreamReader reader)
        {
            var game = ReadStartPositions(reader);

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

        public long Part2(StreamReader reader)
        {
            var start = ReadStartPositions(reader);
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

        private static GameState ReadStartPositions(StreamReader reader)
        {
            var player1 = Convert.ToInt32(reader.ReadLine()[28..]);
            var player2 = Convert.ToInt32(reader.ReadLine()[28..]);
            return new GameState(player1, 0, player2, 0);
        }
    }
}
