using TMPro;
using UnityEngine;

public class SafeCodeUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI displayText;

    private string currentInput = "";

    private void OnEnable()
    {
        GameEventsManager.instance.uiEvents.onOpenSafeCodeUI += Open;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.uiEvents.onOpenSafeCodeUI -= Open;
    }


    public void Open()
    {
        currentInput = "";
        displayText.text = "";
        uiPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Close()
    {
        uiPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void AddDigit(string digit)
    {
        if (currentInput.Length >= 4)
        {
            return;
        }

        currentInput += digit;
        displayText.text = currentInput;
    }

    public void DeleteLast()
    {
        if (currentInput.Length == 0)
        {
            return;
        }

        currentInput = currentInput[..^1];
        displayText.text = currentInput;
    }

    public void Submit()
    {
        GameEventsManager.instance.puzzleEvents.CodeEntered(currentInput);
        Close();
    }
}
