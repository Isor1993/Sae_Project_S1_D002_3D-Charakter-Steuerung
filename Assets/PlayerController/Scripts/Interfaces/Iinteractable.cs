/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : IInteractable.cs
* Date    : 28.01.2026
* Author  : Eric Rosenberg
*
* Description :
* Interface that defines a basic interaction contract.
* Any object that can be interacted with by the player
* must implement this interface and define its own
* interaction behavior.
*
* History :
* 28.01.2026 ER Created
******************************************************************************/
using UnityEngine;


public interface IInteractable
{
    /// <summary>
    /// /// <summary>
    /// Executes the interaction logic of the object.
    /// This method is typically called by an interaction
    /// handler or controller when the player triggers
    /// an interaction (e.g. pressing a key or button).
    /// </summary>    
    public void Interact();
}
