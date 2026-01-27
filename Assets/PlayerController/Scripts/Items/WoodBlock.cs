/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : WoodBlock.cs
* Date    : 28.01.2026
* Author  : Eric Rosenberg
*
* Description :
* Interactable object that destroys itself when the player interacts with it.
* Can be used for breakable objects such as crates or obstacles.
*
* History :
* 28.01.2026 ER Created
******************************************************************************/
using UnityEngine;

public class WoodBlock : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Called when the player interacts with the object.
    /// Destroys this GameObject.
    /// </summary>
    public void Interact()
    {
        GameObject.Destroy(gameObject);
    }
}
