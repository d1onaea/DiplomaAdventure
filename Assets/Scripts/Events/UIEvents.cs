using System;

public class UIEvents
{
    public event Action<IInteractable> onShowInteractUI;
    public event Action onHideInteractUI;
    public event Action onOpenSafeCodeUI;
    public event Action onDescriptionOpened;
    public event Action onInventoryOpened;
    public event Action onNinaPosterOpened;
    public void ShowInteractUI(IInteractable interactable)
    {
        onShowInteractUI?.Invoke(interactable);
    }

    public void HideInteractUI()
    {
        onHideInteractUI?.Invoke();
    }
    public void OpenSafeCodeUI()
    {
        onOpenSafeCodeUI?.Invoke();
    }
    public void DescriptionOpened()
    {
        onDescriptionOpened?.Invoke();
    }
    public void InventoryOpened()
    {
        onInventoryOpened?.Invoke();
    }
    public void OpenNinaPoster()
    {
        onNinaPosterOpened?.Invoke();
    }
}
