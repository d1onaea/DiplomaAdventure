using System;

public class PuzzleEvents
{
    public event Action<string> onPuzzleCompleted;

    public void PuzzleCompleted(string puzzleID)
    {
        onPuzzleCompleted?.Invoke(puzzleID);
    }

    public event Action<string> onCodeEntered;

    public void CodeEntered(string code)
    {
        onCodeEntered?.Invoke(code);
    }
}

