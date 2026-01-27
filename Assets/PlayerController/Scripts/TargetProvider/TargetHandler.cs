/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : TargetHandler.cs
* Date    : 28.01.2026
* Author  : Eric Rosenberg
*
* Description :
* Handles interactions with detected targets.
* Checks whether the hit object implements the IInteractable interface
* and triggers its interaction logic if available.
*
* History :
* 28.01.2026 ER Created
******************************************************************************/
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    /// <summary>
    /// Handles an interaction attempt for a detected raycast hit.
    /// If the hit collider implements IInteractable, its Interact
    /// method is executed.
    /// </summary>
    /// <param name="hit">
    /// RaycastHit containing information about the detected target.
    /// </param>
    public void HandleInteracttion(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }
}
