using UnityEngine;

public class PuzzleTriggerInteractable : MonoBehaviour, IInteractable, IDataPersistence, ICheckInteractable
{
    [SerializeField] private GameObject puzzleUIContainer;
    [SerializeField] private InventoryItemData rewardItem;
    [SerializeField] private string puzzleID;
    private Outline outline;

    private bool puzzleCompleted = false;
    private bool canInteract = false;
    private bool isBeingDestroyed = false;

    private void OnEnable()
    {
        if (GameEventsManager.instance != null)
        {
            EnviornmentEvents env = GameEventsManager.instance.enviornmentEvents;
            PuzzleEvents puzzle = GameEventsManager.instance.puzzleEvents;

            if (env != null)
            {
                env.onDrawerOpened += EnableInteraction;
                env.onDrawerClosed += DisableInteraction;
            }

            if (puzzle != null)
            {
                puzzle.onPuzzleCompleted += OnPuzzleCompleted;
            }
        }

        GameEventsManager.instance.inputEvents.onHighlightStarted += ShowHighlight;
        GameEventsManager.instance.inputEvents.onHighlightEnded += HideHighlight;
    }

    private void OnDisable()
    {
        if (isBeingDestroyed || GameEventsManager.instance == null)
        {
            return;
        }

        EnviornmentEvents env = GameEventsManager.instance.enviornmentEvents;
        PuzzleEvents puzzle = GameEventsManager.instance.puzzleEvents;

        if (env != null)
        {
            env.onDrawerOpened -= EnableInteraction;
            env.onDrawerClosed -= DisableInteraction;
        }

        if (puzzle != null)
        {
            puzzle.onPuzzleCompleted -= OnPuzzleCompleted;
        }

        GameEventsManager.instance.inputEvents.onHighlightStarted += ShowHighlight;
        GameEventsManager.instance.inputEvents.onHighlightEnded += HideHighlight;
    }

    private void Awake()
    {
        outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    private void HideHighlight()
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    private void ShowHighlight()
    {
        if (outline != null)
        {
            outline.enabled = true;
        }
    }

    public void LoadData(GameData data)
    {
        puzzleCompleted = data.puzzlesSolved.Contains(puzzleID);
        if (puzzleCompleted)
        {
            isBeingDestroyed = true;
            Destroy(gameObject);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (puzzleCompleted && !data.puzzlesSolved.Contains(puzzleID))
        {
            data.puzzlesSolved.Add(puzzleID);
        }
    }

    private void EnableInteraction()
    {
        canInteract = true;
    }

    private void DisableInteraction()
    {
        canInteract = false;
    }

    public void Interact()
    {
        if (!CanCurrentlyInteract())
        {
            return;
        }

        puzzleUIContainer.SetActive(true);
    }

    public void OnPuzzleCompleted(string completedPuzzleID)
    {
        if (completedPuzzleID != puzzleID || puzzleCompleted)
        {
            return;
        }

        puzzleCompleted = true;
        puzzleUIContainer.SetActive(false);
        GameEventsManager.instance.inventoryEvents.ItemPickedUp(rewardItem);
        DataPersistenceManager.instance.SaveGame();

        isBeingDestroyed = true;
        Destroy(gameObject);
    }

    public string GetInteractText()
    {
        return CanCurrentlyInteract() ? "Examine" : null;
    }

    public bool CanCurrentlyInteract()
    {
        return canInteract && !puzzleCompleted;
    }
}
