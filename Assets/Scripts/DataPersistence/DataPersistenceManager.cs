
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class DataPersistenceManager : MonoBehaviour
{
    public static bool isNewGame = false;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }
    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        GameEventsManager.instance.gameEvents.onGameEnded += OnGameEnded;
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        _ = StartCoroutine(LoadGameWithDelay());
    }
    private IEnumerator LoadGameWithDelay()
    {
        yield return null;

        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    private void OnDestroy()
    {
        GameEventsManager.instance.gameEvents.onGameEnded -= OnGameEnded;
    }
    public void NewGame()
    {
        gameData = new GameData();
    }
    public void LoadGame()
    {
        gameData = dataHandler.Load();
        if (gameData == null)
        {
            Debug.Log("No data was found");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    private void OnGameEnded(bool won)
    {
        dataHandler.DeleteSaveFile();
    }

}


