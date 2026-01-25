using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    public void HandleInteracttion(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }
}
