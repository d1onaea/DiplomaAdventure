using System;
using UnityEngine;

public class InputEvents
{
    public event Action<Vector2> onMovePressed;
    public event Action onSubmitPressed;
    public event Action onInventoryTogglePressed;
    public event Action onDialogueContinuePressed;
    public event Action onMenuTogglePressed;
    public event Action onHighlightStarted;
    public event Action onHighlightEnded;
    public void MovePressed(Vector2 moveDir)
    {
        onMovePressed?.Invoke(moveDir);
    }

    public void SubmitPressed()
    {
        onSubmitPressed?.Invoke();
    }

    public void InventoryTogglePressed()
    {
        onInventoryTogglePressed?.Invoke();
    }
    public void DialogueContinuePressed()
    {
        onDialogueContinuePressed?.Invoke();
    }
    public void MenuTogglePressed()
    {
        onMenuTogglePressed?.Invoke();
    }

    public void HighlightStarted()
    {
        onHighlightStarted?.Invoke();
    }

    public void HighlightEnded()
    {
        onHighlightEnded?.Invoke();
    }

}
