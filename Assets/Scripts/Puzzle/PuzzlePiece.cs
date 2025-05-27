using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject draggableParentPanel;

    public int correctIndex;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent;
    }

    public void Init(GameObject draggablePanel)
    {
        draggableParentPanel = draggablePanel;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(originalParent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetCanvasScale();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        PuzzleSlot closestSlot = PuzzleManager.instance.GetClosestSlot(transform.position);

        if (closestSlot != null)
        {
            transform.SetParent(closestSlot.transform);
            rectTransform.anchoredPosition = Vector2.zero;
            PuzzleManager.instance.CheckCompletion();
        }
        else
        {
            transform.SetParent(draggableParentPanel.transform);

            if (IsInsidePanel(transform.position))
            {
            }
            else
            {
                rectTransform.anchoredPosition = PuzzleManager.instance.GetEdgePositionInPanel(transform.position);
            }
        }
    }

    private bool IsInsidePanel(Vector3 worldPosition)
    {
        RectTransform panelRect = draggableParentPanel.GetComponent<RectTransform>();
        _ = RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, worldPosition, null, out Vector2 localPoint);
        return panelRect.rect.Contains(localPoint);
    }

    private float GetCanvasScale()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        return canvas.scaleFactor;
    }
}
