using Ink.Runtime;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Ink story")]
    [SerializeField] private TextAsset inkJson;

    private Story story;
    private int currentChoiceIndex = -1;
    private bool dialoguePlaying = false;
    private InkExternalFunctions inkExternalFunctions;
    private bool selectionMode = false;
    private string queuedKnotName = null;
    private void Awake()
    {
        story = new Story(inkJson.text);
        inkExternalFunctions = new InkExternalFunctions();
        inkExternalFunctions.Bind(story);
    }

    private void OnDestroy()
    {
        inkExternalFunctions.Unbind(story);
    }
    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
        GameEventsManager.instance.inputEvents.onDialogueContinuePressed += DialogueContinuePressed;
        GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex += UpdateChoiceIndex;

        GameEventsManager.instance.dialogueEvents.onChooseItem += EnableSelectionMode;
        GameEventsManager.instance.dialogueEvents.onDisableSelectionMode += DisableSelectionMode;
        GameEventsManager.instance.dialogueEvents.onQueueDialogue += QueueDialogue;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
        GameEventsManager.instance.inputEvents.onDialogueContinuePressed -= DialogueContinuePressed;
        GameEventsManager.instance.dialogueEvents.onUpdateChoiceIndex -= UpdateChoiceIndex;

        GameEventsManager.instance.dialogueEvents.onChooseItem -= EnableSelectionMode;
        GameEventsManager.instance.dialogueEvents.onDisableSelectionMode -= DisableSelectionMode;
        GameEventsManager.instance.dialogueEvents.onQueueDialogue -= QueueDialogue;
    }

    private void QueueDialogue(string knotName)
    {
        queuedKnotName = knotName;
    }

    private void DisableSelectionMode()
    {
        selectionMode = false;
    }

    private void EnableSelectionMode()
    {
        selectionMode = true;
    }

    private void UpdateChoiceIndex(int choiceIndex)
    {
        currentChoiceIndex = choiceIndex;
    }

    private void DialogueContinuePressed()
    {
        if (!dialoguePlaying)
        {
            return;
        }

        ContinueOrExitStory();
    }

    private void EnterDialogue(string knotName)
    {
        if (dialoguePlaying)
        {
            return;
        }
        GameEventsManager.instance.dialogueEvents.DialogueStarted();

        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        GameEventsManager.instance.playerEvents.DisableCameraControl();
        GameEventsManager.instance.playerEvents.DisablePlayerInteraction();
        GameEventsManager.instance.playerEvents.DisableInventoryToggle();
        dialoguePlaying = true;


        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);
        }
        else
        {
            Debug.LogWarning("Knot name is an empty string when entering dialogue");
        }
        DialogueContinuePressed();
    }

    private void ContinueOrExitStory()
    {
        if (story.currentChoices.Count > 0 && currentChoiceIndex != -1)
        {
            story.ChooseChoiceIndex(currentChoiceIndex);
            currentChoiceIndex = -1;
        }

        if (story.canContinue)
        {
            string dialogueLine = story.Continue();

            while (IsLineBlank(dialogueLine) && story.canContinue)
            {
                dialogueLine = story.Continue();
            }
            if (IsLineBlank(dialogueLine) && !story.canContinue)
            {
                ExitDialogue();
            }
            else
            {
                GameEventsManager.instance.dialogueEvents.DisplayDialogue(dialogueLine, story.currentChoices);
            }
        }
        else if (story.currentChoices.Count == 0)
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        dialoguePlaying = false;
        GameEventsManager.instance.dialogueEvents.DialogueFinished();

        if (!selectionMode)
        {
            GameEventsManager.instance.playerEvents.EnablePlayerMovement();
            GameEventsManager.instance.playerEvents.EnableCameraControl();
            GameEventsManager.instance.playerEvents.EnablePlayerInteraction();
            GameEventsManager.instance.playerEvents.EnableInventoryToggle();
        }
        story.ResetState();
        if (!string.IsNullOrEmpty(queuedKnotName))
        {
            string nextKnot = queuedKnotName;
            queuedKnotName = null;
            EnterDialogue(nextKnot);
        }
        else
        {
            GameEventsManager.instance.dialogueEvents.DialogueFullyEnded();
        }
    }

    private bool IsLineBlank(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }
}
