using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private TextMeshProUGUI emptyInventoryText;
    [SerializeField] private GameObject chooseItemContainer;

    [Header("Item UI References")]
    [SerializeField] private Transform inventoryItemsParent;
    [SerializeField] private GameObject inventoryItemButtonPrefab;

    [Header("Item Description UI References")]
    [SerializeField] private GameObject itemDescriptionUIContainer;
    [SerializeField] private Image descriptionIconImage;
    [SerializeField] private TextMeshProUGUI descriptionNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button closeButton;


    private bool isVisible = false;
    private bool inventoryToggleDisabled;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed += ToggleInventory;
        GameEventsManager.instance.inventoryEvents.onInventoryUpdated += UpdateInventoryUI;

        GameEventsManager.instance.playerEvents.onDisableInventoryToggle += DisableInventoryToggle;
        GameEventsManager.instance.playerEvents.onEnableInventoryToggle += EnableInventoryToggle;

        GameEventsManager.instance.dialogueEvents.onChooseItem += OpenInventoryForSelection;
        GameEventsManager.instance.inventoryEvents.onItemSelectedFromInventory += HandleItemSelected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onInventoryTogglePressed -= ToggleInventory;
        GameEventsManager.instance.inventoryEvents.onInventoryUpdated -= UpdateInventoryUI;

        GameEventsManager.instance.playerEvents.onDisableInventoryToggle += DisableInventoryToggle;
        GameEventsManager.instance.playerEvents.onEnableInventoryToggle += EnableInventoryToggle;

        GameEventsManager.instance.dialogueEvents.onChooseItem -= OpenInventoryForSelection;
        GameEventsManager.instance.inventoryEvents.onItemSelectedFromInventory -= HandleItemSelected;
    }



    public void DisableInventoryToggle()
    {
        inventoryToggleDisabled = true;
    }

    public void EnableInventoryToggle()
    {
        inventoryToggleDisabled = false;
    }

    private void HandleItemSelected(InventoryItemData selectedItem)
    {
        InventoryManager.instance.RemoveItem(selectedItem);
        GameEventsManager.instance.dialogueEvents.EnterDialogue(selectedItem.itemInkKnot);
        inventoryContainer.SetActive(false);
        chooseItemContainer.SetActive(false);
    }

    private void OpenInventoryForSelection()
    {
        inventoryContainer.SetActive(true);
        chooseItemContainer.SetActive(true);
        RefreshInventory(true);
    }


    private void ToggleInventory()
    {
        if (inventoryToggleDisabled)
        {
            return;
        }
        isVisible = !isVisible;
        inventoryContainer.SetActive(isVisible);
        if (isVisible)
        {
            RefreshInventory(false);
            GameEventsManager.instance.playerEvents.DisablePlayerMovement();
            GameEventsManager.instance.playerEvents.DisableCameraControl();
        }
        else
        {
            GameEventsManager.instance.playerEvents.EnablePlayerMovement();
            GameEventsManager.instance.playerEvents.EnableCameraControl();
            GameEventsManager.instance.playerEvents.EnableInventoryToggle();
        }
    }

    private void RefreshInventory(bool allowSelection)
    {
        foreach (Transform child in inventoryItemsParent)
        {
            Destroy(child.gameObject);
        }

        List<InventoryItemData> items = InventoryManager.instance.GetItems();

        emptyInventoryText.gameObject.SetActive(items.Count == 0);

        foreach (InventoryItemData item in items)
        {
            GameObject buttonGO = Instantiate(inventoryItemButtonPrefab, inventoryItemsParent);
            InventoryItemButton button = buttonGO.GetComponent<InventoryItemButton>();

            button.Setup(item, itemDescriptionUIContainer, descriptionIconImage, descriptionNameText, descriptionText, closeButton, allowSelection);
        }
    }


    private void UpdateInventoryUI(List<InventoryItemData> items)
    {
        if (isVisible)
        {
            RefreshInventory(false);
        }
    }
}
