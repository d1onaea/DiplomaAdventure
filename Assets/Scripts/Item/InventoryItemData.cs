using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Item")]
public class InventoryItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    [TextArea] public string itemDescription;
    public string itemInkKnot;

    public ItemTag tag;
}

public enum ItemTag
{
    clue,
    neutral,
    junk
}
