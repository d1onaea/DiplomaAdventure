using System;
using System.Collections.Generic;

public class InventoryEvents
{
    public event Action<InventoryItemData> onItemPickedUp;
    public event Action<List<InventoryItemData>> onInventoryUpdated;
    public event Action<InventoryItemData> onItemSelectedFromInventory;
    public void ItemPickedUp(InventoryItemData item)
    {
        onItemPickedUp?.Invoke(item);
    }

    public void InventoryUpdated(List<InventoryItemData> items)
    {
        onInventoryUpdated?.Invoke(items);
    }

    public void ItemSelectedFromInventory(InventoryItemData selectedItem)
    {
        onItemSelectedFromInventory?.Invoke(selectedItem);
    }
}
