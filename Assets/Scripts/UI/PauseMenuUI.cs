using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUIContainer;

    private bool isMenuOpen = false;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onMenuTogglePressed += ToggleMenu;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onMenuTogglePressed -= ToggleMenu;
    }

    private void ToggleMenu()
    {
        if (!isMenuOpen)
        {
            OpenMenu();
        }
        else
        {
            OnResumeGameClicked();
        }
    }

    public void OpenMenu()
    {
        isMenuOpen = true;
        pauseMenuUIContainer.SetActive(true);
        Time.timeScale = 0f;
        DataPersistenceManager.instance.SaveGame();
    }

    public void OnResumeGameClicked()
    {
        isMenuOpen = false;
        pauseMenuUIContainer.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnMainMenuClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}

