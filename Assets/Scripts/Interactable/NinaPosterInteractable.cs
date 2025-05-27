using UnityEngine;

public class NinaPosterInteractable : MonoBehaviour, IInteractable
{
    private Outline outline;

    public string GetInteractText()
    {
        return "Examine";
    }

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
        GameEventsManager.instance.inputEvents.onHighlightStarted += ShowHighlight;
        GameEventsManager.instance.inputEvents.onHighlightEnded += HideHighlight;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onHighlightStarted -= ShowHighlight;
        GameEventsManager.instance.inputEvents.onHighlightEnded -= HideHighlight;
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

    public void Interact()
    {
        Debug.Log("NinaPoster Interacted");
        GameEventsManager.instance.uiEvents.OpenNinaPoster();
    }
}
