using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private float rotationAngle = 90f;
    [SerializeField] private float rotationSpeed = 2f;

    private bool isOpen = false;
    private bool isRotating = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, rotationAngle, 0));
        targetRotation = closedRotation;
    }

    public string GetInteractText()
    {
        return isOpen ? "Close door" : "Open door";
    }

    public void Interact()
    {
        if (isRotating)
        {
            return;
        }

        isOpen = !isOpen;
        targetRotation = isOpen ? openRotation : closedRotation;
        isRotating = true;

    }

    private void Update()
    {
        if (!isRotating)
        {
            return;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
        {
            transform.rotation = targetRotation;
            isRotating = false;
        }
    }
}
