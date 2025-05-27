using UnityEngine;

public class SafeLockInteractable : MonoBehaviour, IInteractable, IDataPersistence, ICheckInteractable
{
    [SerializeField] private string correctCode = "1598";
    [SerializeField] private SafeDoor safeDoor;
    [SerializeField] private string puzzleID = "safe_1";
    [SerializeField] private GameObject itemToSpawnPrefab;
    [SerializeField] private InventoryItemData itemDataToGive;
    [SerializeField] private Transform spawnPoint;
    private Outline outline;
    private bool isSolved = false;

    private GameObject spawnedItem;


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
        GameEventsManager.instance.puzzleEvents.onCodeEntered += CheckCode;
        GameEventsManager.instance.inputEvents.onHighlightStarted += ShowHighlight;
        GameEventsManager.instance.inputEvents.onHighlightEnded += HideHighlight;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.puzzleEvents.onCodeEntered -= CheckCode;
        GameEventsManager.instance.inputEvents.onHighlightStarted -= ShowHighlight;
        GameEventsManager.instance.inputEvents.onHighlightEnded -= HideHighlight;
    }

    public void Interact()
    {
        if (isSolved)
        {
            return;
        }

        GameEventsManager.instance.uiEvents.OpenSafeCodeUI();
    }

    public string GetInteractText()
    {
        return "Enter Code";
    }

    private void CheckCode(string code)
    {
        if (isSolved)
        {
            return;
        }

        if (code == correctCode)
        {
            isSolved = true;
            safeDoor.Open();
            SpawnItem();
            outline.enabled = false;
            GameEventsManager.instance.puzzleEvents.PuzzleCompleted(puzzleID);
            GameEventsManager.instance.inputEvents.onHighlightStarted -= ShowHighlight;
            GameEventsManager.instance.inputEvents.onHighlightEnded -= HideHighlight;
        }
        else
        {
            Debug.Log("Wrong Code!");
        }
    }
    private void SpawnItem()
    {
        if (itemToSpawnPrefab != null && spawnPoint != null && spawnedItem == null)
        {
            spawnedItem = Instantiate(itemToSpawnPrefab, spawnPoint.position, spawnPoint.rotation);

            ItemInteractable itemComponent = spawnedItem.GetComponent<ItemInteractable>();
            if (itemComponent != null)
            {
                itemComponent.Init(itemDataToGive);
            }
            else
            {
                Debug.LogError("ItemInteractable component not found on the spawned item prefab!");
            }
        }
    }


    public void LoadData(GameData data)
    {
        isSolved = data.puzzlesSolved.Contains(puzzleID);
        if (isSolved)
        {
            outline.enabled = false;
            GameEventsManager.instance.inputEvents.onHighlightStarted -= ShowHighlight;
            GameEventsManager.instance.inputEvents.onHighlightEnded -= HideHighlight;
            safeDoor.Open();
        }
    }

    public void SaveData(ref GameData data)
    {
        if (isSolved && !data.puzzlesSolved.Contains(puzzleID))
        {
            data.puzzlesSolved.Add(puzzleID);
        }
    }

    private void ShowHighlight()
    {
        if (outline != null)
        {
            outline.enabled = true;
        }
    }

    private void HideHighlight()
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }
    public bool CanCurrentlyInteract()
    {
        return !isSolved;
    }
}
