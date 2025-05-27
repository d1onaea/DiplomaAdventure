using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string saveFileName = "data.json";

    public void OnNewGameClicked()
    {
        FileDataHandler fileHandler = new(Application.persistentDataPath, saveFileName);
        fileHandler.DeleteSaveFile();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnContinueGameClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
