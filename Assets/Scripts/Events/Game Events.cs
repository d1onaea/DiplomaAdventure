using System;

public class GameEvents
{
    public event Action<bool> onGameEnded;
    public event Action<int> onPlayerMadeMistake;

    public void EndGame(bool won)
    {
        onGameEnded?.Invoke(won);
    }

    public void PlayerMadeMistake(int mistakeCount)
    {
        onPlayerMadeMistake?.Invoke(mistakeCount);
    }
}
