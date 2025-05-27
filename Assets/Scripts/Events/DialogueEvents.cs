using Ink.Runtime;
using System;
using System.Collections.Generic;

public class DialogueEvents
{
    public event Action<string> onEnterDialogue;
    public event Action onDialogueStarted;
    public event Action onDialogueFinished;
    public event Action<string, List<Choice>> onDisplayDialogue;
    public event Action<int> onUpdateChoiceIndex;


    public event Action onChooseItem;
    public event Action onDisableSelectionMode;
    public event Action<string> onQueueDialogue;
    public event Action onDialogueFullyEnded; //for end game
    public void EnterDialogue(string knotName)
    {
        onEnterDialogue?.Invoke(knotName);
    }

    public void DialogueStarted()
    {
        onDialogueStarted?.Invoke();
    }

    public void DialogueFinished()
    {
        onDialogueFinished?.Invoke();
    }

    public void DisplayDialogue(string dialogueLine, List<Choice> dialogieChoices)
    {
        onDisplayDialogue?.Invoke(dialogueLine, dialogieChoices);
    }

    public void UpdateChoiceIndex(int choiceIndex)
    {
        onUpdateChoiceIndex?.Invoke(choiceIndex);
    }

    public void ChooseItem()
    {
        onChooseItem?.Invoke();
    }

    public void DisableSelectionMode()
    {
        onDisableSelectionMode?.Invoke();
    }

    public void QueueDialogue(string knotName)
    {
        onQueueDialogue?.Invoke(knotName);
    }

    public void DialogueFullyEnded()
    {
        onDialogueFullyEnded?.Invoke();
    }
}
