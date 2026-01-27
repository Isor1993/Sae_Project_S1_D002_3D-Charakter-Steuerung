/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : Ball.cs
* Date    : 28.01.2025
* Author  : Eric Rosenberg
*
* Description :
* 
* 
* 
* History :
* 28.01.2025 ER Created
******************************************************************************/
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
