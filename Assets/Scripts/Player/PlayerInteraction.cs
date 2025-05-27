using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRange = 2f;
    private IInteractable currentInteractable;
    private bool interactionDisabled = false;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed += TryInteract;
        GameEventsManager.instance.playerEvents.onDisablePlayerMovement += DisablePlayerInteraction;
        GameEventsManager.instance.playerEvents.onEnablePlayerMovement += EnablePlayerInteraction;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed -= TryInteract;
        GameEventsManager.instance.playerEvents.onDisablePlayerMovement -= DisablePlayerInteraction;
        GameEventsManager.instance.playerEvents.onEnablePlayerMovement -= EnablePlayerInteraction;
    }

    private void Update()
    {
        if (interactionDisabled)
        {
            return;
        }
        IInteractable closest = GetClosestInteractable();

        if (closest != currentInteractable)
        {
            currentInteractable = closest;

            if (currentInteractable != null)
            {
                if (currentInteractable is ICheckInteractable check && !check.CanCurrentlyInteract())
                {
                    GameEventsManager.instance.uiEvents.HideInteractUI();
                }
                else
                {
                    GameEventsManager.instance.uiEvents.ShowInteractUI(currentInteractable);
                }
            }
            else
            {
                GameEventsManager.instance.uiEvents.HideInteractUI();
            }
        }
    }

    public void DisablePlayerInteraction()
    {
        interactionDisabled = true;
        GameEventsManager.instance.uiEvents.HideInteractUI();
    }

    private void EnablePlayerInteraction()
    {
        interactionDisabled = false;
        IInteractable closest = GetClosestInteractable();

        if (closest != null)
        {
            currentInteractable = closest;

            if (currentInteractable is ICheckInteractable check && !check.CanCurrentlyInteract())
            {
                GameEventsManager.instance.uiEvents.HideInteractUI();
            }
            else
            {
                GameEventsManager.instance.uiEvents.ShowInteractUI(currentInteractable);
            }
        }
    }

    private void TryInteract()
    {
        if (interactionDisabled || currentInteractable == null)
        {
            return;
        }

        if (currentInteractable is ICheckInteractable check && !check.CanCurrentlyInteract())
        {
            return;
        }

        currentInteractable.Interact();
        GameEventsManager.instance.uiEvents.ShowInteractUI(currentInteractable);
    }

    private IInteractable GetClosestInteractable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        List<IInteractable> interactables = new();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactables.Add(interactable);
            }
        }

        IInteractable closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (IInteractable interactable in interactables)
        {
            float distance = Vector3.Distance(transform.position, ((MonoBehaviour)interactable).transform.position);
            if (distance < closestDistance)
            {
                closest = interactable;
                closestDistance = distance;
            }
        }

        return closest;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
