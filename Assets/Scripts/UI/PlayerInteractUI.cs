using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject interactContainer;
    [SerializeField] private TextMeshProUGUI interactText;
    private IInteractable lastValidInteractable;
    private void OnEnable()
    {
        GameEventsManager.instance.uiEvents.onShowInteractUI += ShowContainer;
        GameEventsManager.instance.uiEvents.onHideInteractUI += HideContainer;

        GameEventsManager.instance.dialogueEvents.onEnterDialogue += HandleDialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFullyEnded += HandleDialogueEnded;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.uiEvents.onShowInteractUI -= ShowContainer;
        GameEventsManager.instance.uiEvents.onHideInteractUI -= HideContainer;

        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= HandleDialogueStarted;
        GameEventsManager.instance.dialogueEvents.onDialogueFullyEnded -= HandleDialogueEnded;
    }

    private void HandleDialogueStarted(string knot)
    {
        interactContainer.SetActive(false);
    }

    private void HandleDialogueEnded()
    {
        if (lastValidInteractable != null)
        {
            ShowContainer(lastValidInteractable);
        }
    }

    private void ShowContainer(IInteractable interactable)
    {
        if (interactable is ICheckInteractable check && !check.CanCurrentlyInteract())
        {
            return;
        }

        lastValidInteractable = interactable;
        interactContainer.SetActive(true);
        interactText.text = interactable.GetInteractText();
    }


    private void HideContainer()
    {
        interactContainer.SetActive(false);
    }
}
