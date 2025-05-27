using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemButton : MonoBehaviour, IPointerClickHandler
{
    [Header("UI References")]
    [SerializeField] private Image itemIconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;

    private InventoryItemData itemData;

    [Header("Item Description UI")]
    private GameObject itemDescriptionUIContainer;
    private Image descriptionIconImage;
    private TextMeshProUGUI descriptionNameText;
    private TextMeshProUGUI descriptionText;
    private Button closeButton;

    private bool canChoose = false;

    public void Setup(InventoryItemData item, GameObject uiContainer, Image iconImage, TextMeshProUGUI nameText, TextMeshProUGUI description, Button closeBtn, bool allowSelection = false)
    {
        itemData = item;

        itemIconImage.sprite = item.itemIcon;
        itemNameText.text = item.itemName;

        itemDescriptionUIContainer = uiContainer;
        descriptionIconImage = iconImage;
        descriptionNameText = nameText;
        descriptionText = description;
        closeButton = closeBtn;

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseDescriptionPanel);
        }

        canChoose = allowSelection;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ShowDescriptionPanel();
        }
        else if (eventData.button == PointerEventData.InputButton.Left && canChoose)
        {
            SelectItem();
        }
    }

    private void SelectItem()
    {
        GameEventsManager.instance.inventoryEvents.ItemSelectedFromInventory(itemData);
        GameEventsManager.instance.dialogueEvents.DisableSelectionMode();
        canChoose = false;
    }

    private void ShowDescriptionPanel()
    {
        if (itemDescriptionUIContainer != null)
        {
            descriptionIconImage.sprite = itemData.itemIcon;
            descriptionNameText.text = itemData.itemName;
            descriptionText.text = itemData.itemDescription;
            itemDescriptionUIContainer.SetActive(true);
            GameEventsManager.instance.uiEvents.DescriptionOpened();
            GameEventsManager.instance.playerEvents.DisableInventoryToggle();
        }
    }

    private void CloseDescriptionPanel()
    {
        if (itemDescriptionUIContainer != null)
        {
            itemDescriptionUIContainer.SetActive(false);
            GameEventsManager.instance.playerEvents.EnableInventoryToggle();
        }
    }
}
