using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject dialogueContainer;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueChoiceButton[] choiceButtons;
    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted += DialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished += DialogueFinished;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue += DisplayDialogue;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStarted -= DialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFinished -= DialogueFinished;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
    }

    private void Awake()
    {
        dialogueContainer.SetActive(false);
        ResetContainer();
    }

    private void DialogueStarted()
    {
        dialogueContainer.SetActive(true);
    }

    private void DialogueFinished()
    {
        dialogueContainer.SetActive(false);
        ResetContainer();
    }

    private void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices)
    {
        dialogueText.text = dialogueLine;
        if (dialogueChoices.Count > choiceButtons.Length)
        {
            Debug.LogError("More dialogue choices (" + dialogueChoices.Count + ") than are supported");
        }
        foreach (DialogueChoiceButton choiceButton in choiceButtons)
        {
            choiceButton.gameObject.SetActive(false);
        }
        int choiceButtonIndex = dialogueChoices.Count - 1;
        for (int inkChoiceIndex = 0; inkChoiceIndex < dialogueChoices.Count; inkChoiceIndex++)
        {
            Choice dialogueChoice = dialogueChoices[inkChoiceIndex];
            DialogueChoiceButton choiceButton = choiceButtons[choiceButtonIndex];

            choiceButton.gameObject.SetActive(true);
            choiceButton.SetChoiceText(dialogueChoice.text);
            choiceButton.SetChoiceIndex(inkChoiceIndex);

            if (inkChoiceIndex == 0)
            {
                choiceButton.SelectButton();
                GameEventsManager.instance.dialogueEvents.UpdateChoiceIndex(0);
            }

            choiceButtonIndex--;
        }
    }

    private void ResetContainer()
    {
        dialogueText.text = "";
    }
}
