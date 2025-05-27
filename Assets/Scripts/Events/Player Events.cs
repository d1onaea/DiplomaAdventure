using System;

public class PlayerEvents
{
    public event Action onDisablePlayerMovement;
    public event Action onEnablePlayerMovement;

    public event Action onDisableCameraControl;
    public event Action onEnableCameraControl;

    public event Action onDisablePlayerInteraction;
    public event Action onEnablePlayerInteraction;

    public event Action onDisableInventoryToggle;
    public event Action onEnableInventoryToggle;
    public void DisablePlayerMovement()
    {
        onDisablePlayerMovement?.Invoke();
    }

    public void EnablePlayerMovement()
    {
        onEnablePlayerMovement?.Invoke();
    }

    public void DisableCameraControl()
    {
        onDisableCameraControl?.Invoke();
    }

    public void EnableCameraControl()
    {
        onEnableCameraControl?.Invoke();
    }

    public void DisablePlayerInteraction()
    {
        onDisablePlayerInteraction?.Invoke();
    }

    public void EnablePlayerInteraction()
    {
        onEnablePlayerInteraction?.Invoke();
    }

    public void DisableInventoryToggle()
    {
        onDisableInventoryToggle?.Invoke();
    }

    public void EnableInventoryToggle()
    {
        onEnableInventoryToggle?.Invoke();
    }
}
