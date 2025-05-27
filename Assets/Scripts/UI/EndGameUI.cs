using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private Image loseGameImage;
    [SerializeField] private Image winGameImage;
    [SerializeField] private Button returnToMenuButton;

    private void Start()
    {
        endGamePanel.SetActive(false);
        GameEventsManager.instance.gameEvents.onGameEnded += ShowEndGameUI;

        returnToMenuButton.onClick.AddListener(OnReturnToMenuClicked);
    }

    private void ShowEndGameUI(bool won)
    {
        endGamePanel.SetActive(true);

        winGameImage.gameObject.SetActive(won);
        loseGameImage.gameObject.SetActive(!won);
    }

    public void OnReturnToMenuClicked()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.gameEvents.onGameEnded -= ShowEndGameUI;
    }
}
