using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        gameObject.SetActive(false);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void LoadData(GameData data)
    {
        volumeSlider.value = data.soundVolume;
        SetVolume(data.soundVolume);
    }

    public void SaveData(ref GameData data)
    {
        data.soundVolume = volumeSlider.value;
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        DataPersistenceManager.instance.SaveGame();
    }
}
