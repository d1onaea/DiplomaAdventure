using UnityEngine;

public class DrawerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float moveDistance = 0.5f;
    [SerializeField]
    private float moveSpeed = 2f;

    private bool isOpen = false;
    private bool isMoving = false;
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        closedPosition = transform.position;
        openPosition = transform.position + (transform.right * moveDistance);
        targetPosition = closedPosition;
    }
    public string GetInteractText()
    {
        return isOpen ? "Close Drawer" : "Open Drawer";
    }

    public void Interact()
    {
        if (isMoving)
        {
            return;
        }

        isOpen = !isOpen;
        targetPosition = isOpen ? openPosition : closedPosition;
        isMoving = true;
        if (isOpen)
        {
            GameEventsManager.instance.enviornmentEvents.DrawerOpened();
        }
        else
        {
            GameEventsManager.instance.enviornmentEvents.DrawerClosed();
        }
    }

    private void Update()
    {
        if (!isMoving)
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
}
