using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int junkCount;
    public int clueCount;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public SerializableDictionary<string, bool> itemsCollected;
    public List<string> inventoryItemNames;
    public bool hasInteractedOnce;
    public List<string> puzzlesSolved;
    public bool altHintShown;
    public bool inventoryHintShown;
    public bool descriptionHintShown;
    public float soundVolume;

    public GameData()
    {
        junkCount = 0;
        clueCount = 0;
        playerPosition = Vector3.zero;
        playerRotation = new Quaternion(0, 0, 0, 0);
        itemsCollected = new SerializableDictionary<string, bool>();
        inventoryItemNames = new List<string>();
        hasInteractedOnce = false;
        puzzlesSolved = new List<string>();
        altHintShown = false;
        inventoryHintShown = false;
        descriptionHintShown = false;
        soundVolume = 1.0f;
    }
}
