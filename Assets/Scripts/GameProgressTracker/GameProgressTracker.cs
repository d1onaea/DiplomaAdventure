using UnityEngine;

public class GameProgressTracker : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int maxJunkItems = 3;
    [SerializeField] private int requiredClueItems = 5;
    private int clueCount = 0;
    private int junkCount = 0;

    private void OnEnable()
    {
        GameEventsManager.instance.inventoryEvents.onItemSelectedFromInventory += HandleItemUse;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inventoryEvents.onItemSelectedFromInventory -= HandleItemUse;
    }



    private void HandleItemUse(InventoryItemData item)
    {
        switch (item.tag)
        {
            case ItemTag.junk:
                junkCount++;
                GameEventsManager.instance.gameEvents.PlayerMadeMistake(junkCount);
                if (junkCount >= maxJunkItems)
                {
                    TriggerLoseCondition();
                }
                break;

            case ItemTag.clue:
                clueCount++;
                if (clueCount >= requiredClueItems)
                {
                    TriggerWinCondition();
                }
                break;
        }
    }

    private void TriggerLoseCondition()
    {
        GameEventsManager.instance.dialogueEvents.QueueDialogue("player_lost");
        GameEventsManager.instance.dialogueEvents.onDialogueFullyEnded += LoseAfterDialogue;
    }

    private void TriggerWinCondition()
    {
        GameEventsManager.instance.dialogueEvents.QueueDialogue("player_won");
        GameEventsManager.instance.dialogueEvents.onDialogueFullyEnded += WinAfterDialogue;
    }

    private void LoseAfterDialogue()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueFullyEnded -= LoseAfterDialogue;
        GameEventsManager.instance.gameEvents.EndGame(false);
    }

    private void WinAfterDialogue()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueFullyEnded -= WinAfterDialogue;
        GameEventsManager.instance.gameEvents.EndGame(true);
    }

    public void LoadData(GameData data)
    {
        clueCount = data.clueCount;
        junkCount = data.junkCount;
    }

    public void SaveData(ref GameData data)
    {
        data.clueCount = clueCount;
        data.junkCount = junkCount;
    }
}

