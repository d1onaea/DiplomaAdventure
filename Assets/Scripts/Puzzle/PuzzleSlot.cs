using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    [SerializeField] private int slotIndex;

    public bool IsEmpty()
    {
        return transform.childCount == 0;
    }

    public bool HasCorrectPiece()
    {
        if (transform.childCount == 0)
        {
            return false;
        }

        PuzzlePiece piece = GetComponentInChildren<PuzzlePiece>();
        return piece.correctIndex == slotIndex;
    }
}
