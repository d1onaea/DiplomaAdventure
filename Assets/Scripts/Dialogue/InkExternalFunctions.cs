using Ink.Runtime;
using System.Collections.Generic;

public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("ChooseItem", () => ChooseItem());
    }
    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("ChooseItem");
    }

    private void ChooseItem()
    {
        List<InventoryItemData> items = InventoryManager.instance.GetItems();
        if (items.Count != 0)
        {
            GameEventsManager.instance.dialogueEvents.ChooseItem();
        }
        else
        {
            GameEventsManager.instance.dialogueEvents.QueueDialogue("empty_inventory");
        }

    }
}
