using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable, IDataPersistence, ICheckInteractable
{
    [Header("Dialogue")]
    [SerializeField] private string initialDialogueKnot = "game_beginning";
    [SerializeField] private string fallbackKnot = "show_evidence";

    private string currentDialogueKnot;
    private bool hasInteractedOnce = false;
    private bool dialogueIsActive = false;
    private bool itemSelectionIsActive = false;

    private void OnEnable()
    {
        DialogueEvents dialogueEvents = GameEventsManager.instance.dialogueEvents;
        dialogueEvents.onEnterDialogue += HandleEnterDialogue;
        dialogueEvents.onDialogueFullyEnded += HandleDialogueEnded;
        dialogueEvents.onChooseItem += HandleChooseItem;
        dialogueEvents.onDisableSelectionMode += HandleDisableItemSelection;
    }

    private void OnDisable()
    {
        DialogueEvents dialogueEvents = GameEventsManager.instance.dialogueEvents;
        dialogueEvents.onEnterDialogue -= HandleEnterDialogue;
        dialogueEvents.onDialogueFullyEnded -= HandleDialogueEnded;
        dialogueEvents.onChooseItem -= HandleChooseItem;
        dialogueEvents.onDisableSelectionMode -= HandleDisableItemSelection;
    }

    private void Awake()
    {
        currentDialogueKnot = initialDialogueKnot;
    }

    public string GetInteractText()
    {
        return "Talk to Sara";
    }

    public void Interact()
    {
        if (!string.IsNullOrEmpty(currentDialogueKnot))
        {
            GameEventsManager.instance.dialogueEvents.EnterDialogue(currentDialogueKnot);
            if (!hasInteractedOnce)
            {
                hasInteractedOnce = true;
                currentDialogueKnot = fallbackKnot;
            }
        }
    }

    public void SetDialogueKnot(string newKnot)
    {
        currentDialogueKnot = newKnot;
    }

    public void LoadData(GameData data)
    {
        hasInteractedOnce = data.hasInteractedOnce;
        currentDialogueKnot = hasInteractedOnce ? fallbackKnot : initialDialogueKnot;
    }

    public void SaveData(ref GameData data)
    {
        data.hasInteractedOnce = hasInteractedOnce;
    }

    public bool CanCurrentlyInteract()
    {
        return !dialogueIsActive && !itemSelectionIsActive;
    }

    private void HandleEnterDialogue(string knot)
    {
        dialogueIsActive = true;
    }

    private void HandleDialogueEnded()
    {
        dialogueIsActive = false;
    }

    private void HandleChooseItem()
    {
        itemSelectionIsActive = true;
    }

    private void HandleDisableItemSelection()
    {
        itemSelectionIsActive = false;
    }
}
