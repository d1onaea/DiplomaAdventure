using UnityEngine;

public class ProgressUI : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject progressUIContainer;

    [Header("Bright Red Hearts")]
    [SerializeField] private GameObject redHeart1;
    [SerializeField] private GameObject redHeart2;
    [SerializeField] private GameObject redHeart3;

    [Header("Dark Red Hearts")]
    [SerializeField] private GameObject darkRedHeart1;
    [SerializeField] private GameObject darkRedHeart2;
    [SerializeField] private GameObject darkRedHeart3;

    private int currentMistakes = 0;

    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += ShowHearts;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished += HideHearts;
        GameEventsManager.instance.gameEvents.onPlayerMadeMistake += UpdateHearts;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= ShowHearts;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished -= HideHearts;
        GameEventsManager.instance.gameEvents.onPlayerMadeMistake -= UpdateHearts;
    }

    private void ShowHearts(string _)
    {
        progressUIContainer.SetActive(true);
        SyncHearts();
    }

    private void HideHearts()
    {
        progressUIContainer.SetActive(false);
    }

    private void UpdateHearts(int mistakeCount)
    {
        currentMistakes = mistakeCount;
        SyncHearts();
    }

    private void SyncHearts()
    {
        // Heart 1
        redHeart1.SetActive(currentMistakes < 1);
        darkRedHeart1.SetActive(currentMistakes >= 1);

        // Heart 2
        redHeart2.SetActive(currentMistakes < 2);
        darkRedHeart2.SetActive(currentMistakes >= 2);

        // Heart 3
        redHeart3.SetActive(currentMistakes < 3);
        darkRedHeart3.SetActive(currentMistakes >= 3);
    }

    public void LoadData(GameData data)
    {
        currentMistakes = data.junkCount;
        SyncHearts();
    }

    public void SaveData(ref GameData data)
    {
        data.junkCount = currentMistakes;
    }
}
