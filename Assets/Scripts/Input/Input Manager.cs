using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            GameEventsManager.instance.inputEvents.MovePressed(context.ReadValue<Vector2>());
        }
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.instance.inputEvents.SubmitPressed();
        }
    }

    public void InventoryTogglePressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.instance.inputEvents.InventoryTogglePressed();
        }
    }

    public void DialoguePressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.instance.inputEvents.DialogueContinuePressed();
        }
    }

    public void MenuTogglePressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.instance.inputEvents.MenuTogglePressed();
        }
    }

    public void HighlightHeld(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameEventsManager.instance.inputEvents.HighlightStarted();
        }
        else if (context.canceled)
        {
            GameEventsManager.instance.inputEvents.HighlightEnded();
        }
    }
}

