using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    [SerializeField] private PuzzleSlot[] slots;
    [SerializeField] private Transform piecesParent;
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private Sprite[] pieceSprites;
    [SerializeField] private GameObject draggableParentPanel;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnPiecesRandomly();
    }

    private void SpawnPiecesRandomly()
    {
        List<int> indices = new();
        for (int i = 0; i < pieceSprites.Length; i++)
        {
            indices.Add(i);
        }

        Shuffle(indices);

        RectTransform panelRect = piecesParent.GetComponent<RectTransform>();

        foreach (int index in indices)
        {
            GameObject pieceObj = Instantiate(piecePrefab, piecesParent);
            PuzzlePiece piece = pieceObj.GetComponent<PuzzlePiece>();
            piece.correctIndex = index;
            piece.Init(draggableParentPanel);
            pieceObj.GetComponent<Image>().sprite = pieceSprites[index];
            RectTransform pieceRect = pieceObj.GetComponent<RectTransform>();

            float panelWidth = panelRect.rect.width;
            float panelHeight = panelRect.rect.height;

            float pieceWidth = pieceRect.rect.width;
            float pieceHeight = pieceRect.rect.height;

            float x = Random.Range((-panelWidth / 2) + (pieceWidth / 2), (panelWidth / 2) - (pieceWidth / 2));
            float y = Random.Range((-panelHeight / 2) + (pieceHeight / 2), (panelHeight / 2) - (pieceHeight / 2));

            pieceRect.anchoredPosition = new Vector2(x, y);
        }
    }
    public Vector2 GetClosestFreePositionInPanel()
    {
        RectTransform panelRect = draggableParentPanel.GetComponent<RectTransform>();

        float panelWidth = panelRect.rect.width;
        float panelHeight = panelRect.rect.height;

        float pieceWidth = piecePrefab.GetComponent<RectTransform>().rect.width;
        float pieceHeight = piecePrefab.GetComponent<RectTransform>().rect.height;

        float x = Random.Range((-panelWidth / 2) + (pieceWidth / 2), (panelWidth / 2) - (pieceWidth / 2));
        float y = Random.Range((-panelHeight / 2) + (pieceHeight / 2), (panelHeight / 2) - (pieceHeight / 2));

        return new Vector2(x, y);
    }


    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    public PuzzleSlot GetClosestSlot(Vector3 position)
    {
        PuzzleSlot closest = null;
        float minDist = float.MaxValue;
        float snapDistance = 100f;

        foreach (PuzzleSlot slot in slots)
        {
            float dist = Vector3.Distance(slot.transform.position, position);
            if (dist < minDist && dist < snapDistance && slot.IsEmpty())
            {
                minDist = dist;
                closest = slot;
            }
        }

        return closest;
    }
    public Vector2 GetEdgePositionInPanel(Vector3 worldPosition)
    {
        RectTransform panelRect = draggableParentPanel.GetComponent<RectTransform>();

        _ = RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, worldPosition, null, out Vector2 localPoint);
        float panelHalfWidth = panelRect.rect.width / 2f;
        float panelHalfHeight = panelRect.rect.height / 2f;
        RectTransform pieceRect = piecePrefab.GetComponent<RectTransform>();
        float pieceHalfWidth = pieceRect.rect.width / 2f;
        float pieceHalfHeight = pieceRect.rect.height / 2f;
        float clampedX = Mathf.Clamp(localPoint.x, -panelHalfWidth + pieceHalfWidth, panelHalfWidth - pieceHalfWidth);
        float clampedY = Mathf.Clamp(localPoint.y, -panelHalfHeight + pieceHalfHeight, panelHalfHeight - pieceHalfHeight);

        return new Vector2(clampedX, clampedY);
    }


    public void CheckCompletion()
    {
        foreach (PuzzleSlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                return;
            }
        }

        foreach (PuzzleSlot slot in slots)
        {
            if (!slot.HasCorrectPiece())
            {
                return;
            }
        }

        string puzzleID = "DrawerPuzzleTrigger";
        GameEventsManager.instance.puzzleEvents.PuzzleCompleted(puzzleID);
    }

}


