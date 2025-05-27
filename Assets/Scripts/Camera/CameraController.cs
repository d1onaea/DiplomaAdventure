using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineInputProvider inputProvider;

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onDisableCameraControl += DisableCamControl;
        GameEventsManager.instance.playerEvents.onEnableCameraControl += EnableCamControl;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onDisableCameraControl -= DisableCamControl;
        GameEventsManager.instance.playerEvents.onEnableCameraControl -= EnableCamControl;
    }

    private void DisableCamControl()
    {
        inputProvider.enabled = false;
    }

    private void EnableCamControl()
    {
        inputProvider.enabled = true;
    }

}
