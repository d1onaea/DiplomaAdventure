using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public InputEvents inputEvents;
    public PlayerEvents playerEvents;
    public UIEvents uiEvents;
    public InventoryEvents inventoryEvents;
    public DialogueEvents dialogueEvents;
    public GameEvents gameEvents;
    public EnviornmentEvents enviornmentEvents;
    public PuzzleEvents puzzleEvents;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        inputEvents = new InputEvents();
        playerEvents = new PlayerEvents();
        uiEvents = new UIEvents();
        inventoryEvents = new InventoryEvents();
        dialogueEvents = new DialogueEvents();
        gameEvents = new GameEvents();
        enviornmentEvents = new EnviornmentEvents();
        puzzleEvents = new PuzzleEvents();
    }
}
