/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : LightSwitch.cs
* Date    : 28.01.2026
* Author  : Eric Rosenberg
*
* Description :
* Interactable object that toggles a Light component on and off
* when the player interacts with it.
*
* History :
* 28.01.2026 ER Created
******************************************************************************/
using UnityEngine;

public class LightSwitch : MonoBehaviour,IInteractable
{
    [Header("Dependencies")]
    [Tooltip("Light component that will be enabled or disabled.")]
    [SerializeField] private Light _lightOrigin;

    /// <summary>
    /// Called when the player interacts with the light switch.
    /// Toggles the enabled state of the assigned Light component.
    /// </summary>
    public void Interact()
    {
        _lightOrigin.enabled=!_lightOrigin.enabled;        
    }    
}
