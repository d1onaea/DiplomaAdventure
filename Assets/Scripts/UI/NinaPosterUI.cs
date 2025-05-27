using UnityEngine;
using UnityEngine.UI;

public class NinaPosterUI : MonoBehaviour
{
    [SerializeField] private GameObject uiContainer;
    [SerializeField] private Button closeButton;
    private bool isOpen = false;
    private void Awake()
    {
        GameEventsManager.instance.uiEvents.onNinaPosterOpened += Show;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.uiEvents.onNinaPosterOpened -= Show;
    }

    private void Start()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(Hide);
        }

        uiContainer.SetActive(false);
    }

    private void Show()
    {
        if (isOpen)
        {
            return;
        }
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        isOpen = true;
        uiContainer.SetActive(true);
        uiContainer.transform.SetAsLastSibling();
        Debug.Log("Nina poster opened");

        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        GameEventsManager.instance.playerEvents.DisableCameraControl();
        GameEventsManager.instance.playerEvents.DisablePlayerInteraction();
        GameEventsManager.instance.playerEvents.DisableInventoryToggle();
    }

    private void Hide()
    {
        if (!isOpen)
        {
            return;
        }

        isOpen = false;
        uiContainer.SetActive(false);
        Debug.Log("Nina poster closed");

        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        GameEventsManager.instance.playerEvents.EnableCameraControl();
        GameEventsManager.instance.playerEvents.EnablePlayerInteraction();
        GameEventsManager.instance.playerEvents.EnableInventoryToggle();
    }
}
