namespace Day21;

public readonly record struct GameState
{
    public GameState(int player1Space, int player1Score, int player2Space, int player2Score)
    {
        Player1Space = player1Space;
        Player1Score = player1Score;
        Player2Space = player2Space;
        Player2Score = player2Score;
    }

    public int Player1Space { get; }

    public int Player1Score { get; }

    public int Player2Space { get; }

    public int Player2Score { get; }

    public GameState Move(int player, int spaces)
    {
        if (player == 0)
        {
            var space = 1 + (Player1Space + (spaces - 1)) % 10;
            var score = Player1Score + space;
            return new GameState(space, score, Player2Space, Player2Score);
        }
        else
        {
            var space = 1 + (Player2Space + (spaces - 1)) % 10;
            var score = Player2Score + space;
            return new GameState(Player1Space, Player1Score, space, score);
        }
    }

    public bool IsFinished(int winningScore)
    {
        return Player1Score >= winningScore || Player2Score >= winningScore;
    }
}