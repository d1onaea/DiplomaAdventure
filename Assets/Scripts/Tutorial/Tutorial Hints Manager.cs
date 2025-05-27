using System.Collections.Generic;
using UnityEngine;

public class TutorialHintsManager : MonoBehaviour, IDataPersistence
{
    [Header("UI References")]
    [SerializeField] private GameObject altHintContainer;
    [SerializeField] private GameObject inventoryHintContainer;
    [SerializeField] private GameObject descriptionHintContainer;

    private bool altHintShown;
    private bool inventoryHintShown;
    private bool descriptionHintShown;
    private bool hasInteractedOnce;

    private bool dataLoaded = false;

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueFullyEnded += ShowHighlightHint;
        GameEventsManager.instance.inputEvents.onHighlightStarted += HideHighlightHint;
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed += HideInventoryHint;
        GameEventsManager.instance.inventoryEvents.onInventoryUpdated += OnInventoryUpdated;
        GameEventsManager.instance.uiEvents.onDescriptionOpened += HideDescriptionHint;
        GameEventsManager.instance.uiEvents.onInventoryOpened += OnInventoryOpened;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueFullyEnded -= ShowHighlightHint;
        GameEventsManager.instance.inputEvents.onHighlightStarted -= HideHighlightHint;
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= HideInventoryHint;
        GameEventsManager.instance.inventoryEvents.onInventoryUpdated -= OnInventoryUpdated;
        GameEventsManager.instance.uiEvents.onDescriptionOpened -= HideDescriptionHint;
        GameEventsManager.instance.uiEvents.onInventoryOpened -= OnInventoryOpened;


    }

    public void LoadData(GameData data)
    {
        altHintShown = data.altHintShown;
        inventoryHintShown = data.inventoryHintShown;
        hasInteractedOnce = data.hasInteractedOnce;
        descriptionHintShown = data.descriptionHintShown;
        dataLoaded = true;

        if (hasInteractedOnce && !altHintShown)
        {
            ShowHighlightHint();
        }

        TryShowInventoryHint();
    }

    public void SaveData(ref GameData data)
    {
        data.altHintShown = altHintShown;
        data.inventoryHintShown = inventoryHintShown;
        data.hasInteractedOnce = hasInteractedOnce;
        data.descriptionHintShown = descriptionHintShown;
    }

    private void ShowHighlightHint()
    {
        hasInteractedOnce = true;
        if (!altHintShown)
        {
            altHintContainer.SetActive(true);
            DisableMovement();
        }
    }

    private void HideHighlightHint()
    {
        if (!altHintShown)
        {
            altHintShown = true;
            altHintContainer.SetActive(false);
            EnableMovement();
        }
    }

    private void OnInventoryUpdated(List<InventoryItemData> items)
    {
        TryShowInventoryHint();
    }

    private void TryShowInventoryHint()
    {
        if (!dataLoaded)
        {
            return;
        }

        if (inventoryHintShown)
        {
            return;
        }

        List<InventoryItemData> items = InventoryManager.instance?.GetItems();
        if (items == null || items.Count == 0)
        {
            return;
        }

        inventoryHintContainer.SetActive(true);
        DisableMovement();
        GameEventsManager.instance.playerEvents.EnableInventoryToggle();
    }

    private void HideInventoryHint()
    {
        if (!inventoryHintContainer.activeSelf)
        {
            return;
        }
        ShowDescriptionHint();
        GameEventsManager.instance.playerEvents.DisableInventoryToggle();
        inventoryHintShown = true;
        inventoryHintContainer.SetActive(false);
    }
    private void OnInventoryOpened()
    {
        List<InventoryItemData> items = InventoryManager.instance?.GetItems();
        if (descriptionHintShown || items.Count == 0)
        {
            return;
        }

        ShowDescriptionHint();
    }

    private void ShowDescriptionHint()
    {
        descriptionHintContainer.SetActive(true);
    }
    private void HideDescriptionHint()
    {
        descriptionHintContainer.SetActive(false);
        descriptionHintShown = true;
    }
    private void DisableMovement()
    {
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        GameEventsManager.instance.playerEvents.DisableCameraControl();
        GameEventsManager.instance.playerEvents.DisablePlayerInteraction();
        GameEventsManager.instance.playerEvents.DisableInventoryToggle();
    }

    private void EnableMovement()
    {
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        GameEventsManager.instance.playerEvents.EnableCameraControl();
        GameEventsManager.instance.playerEvents.EnablePlayerInteraction();
        GameEventsManager.instance.playerEvents.EnableInventoryToggle();
    }
}
