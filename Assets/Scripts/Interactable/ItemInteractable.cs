using UnityEngine;

public class ItemInteractable : MonoBehaviour, IInteractable, IDataPersistence
{
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private bool collected = false;
    private Outline outline;
    public InventoryItemData ItemData => itemData;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onHighlightStarted += ShowHighlight;
        GameEventsManager.instance.inputEvents.onHighlightEnded += HideHighlight;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.inputEvents.onHighlightStarted -= ShowHighlight;
        GameEventsManager.instance.inputEvents.onHighlightEnded -= HideHighlight;
    }

    private void ShowHighlight()
    {
        if (outline != null)
        {
            outline.enabled = true;
        }
    }
    public void Init(InventoryItemData data)
    {
        itemData = data;
    }
    private void HideHighlight()
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    public string GetInteractText()
    {
        return "Collect " + itemData.name;
    }

    public void Interact()
    {
        collected = true;
        GameEventsManager.instance.inventoryEvents.ItemPickedUp(itemData);
        Destroy(gameObject);
    }

    public void LoadData(GameData data)
    {
        _ = data.itemsCollected.TryGetValue(itemData.name, out collected);
        if (collected)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.itemsCollected.ContainsKey(itemData.name))
        {
            _ = data.itemsCollected.Remove(itemData.name);
        }
        data.itemsCollected.Add(itemData.name, collected);
    }
}
