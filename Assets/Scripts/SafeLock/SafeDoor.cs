using UnityEngine;

public class SafeDoor : MonoBehaviour
{
    private bool isOpen = false;

    public void Open()
    {
        if (isOpen)
        {
            return;
        }

        isOpen = true;

        transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
    }
}
