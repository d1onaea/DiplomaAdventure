using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
    public static InventoryManager instance { get; private set; }


    private readonly List<InventoryItemData> items = new();

    private void OnEnable()
    {
        GameEventsManager.instance.inventoryEvents.onItemPickedUp += AddItem;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inventoryEvents.onItemPickedUp -= AddItem;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Inventory Manager in the scene.");
        }
        instance = this;
    }

    private void AddItem(InventoryItemData item)
    {
        items.Add(item);
        GameEventsManager.instance.inventoryEvents.InventoryUpdated(items);
    }

    public void RemoveItem(InventoryItemData item)
    {
        if (items.Contains(item))
        {
            _ = items.Remove(item);
            GameEventsManager.instance.inventoryEvents.InventoryUpdated(items);
        }
    }

    public List<InventoryItemData> GetItems()
    {
        return items;
    }

    public void LoadData(GameData data)
    {
        items.Clear();

        foreach (string itemName in data.inventoryItemNames)
        {
            InventoryItemData item = Resources.Load<InventoryItemData>("Inventory/" + itemName);
            if (item != null)
            {
                items.Add(item);
            }
            else
            {
                Debug.LogWarning("Item not found Resources: " + itemName);
            }
        }

        GameEventsManager.instance.inventoryEvents.InventoryUpdated(items);
    }

    public void SaveData(ref GameData data)
    {
        data.inventoryItemNames.Clear();

        foreach (InventoryItemData item in items)
        {
            if (item != null)
            {
                data.inventoryItemNames.Add(item.name);
            }
            else
            {
                Debug.LogWarning("Null item while saving inventory");
            }
        }
    }
}
