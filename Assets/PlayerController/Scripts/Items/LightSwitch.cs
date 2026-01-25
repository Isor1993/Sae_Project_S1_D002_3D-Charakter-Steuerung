using UnityEngine;

public class LightSwitch : MonoBehaviour,IInteractable
{

    [SerializeField] private Light _lightOrigin;
    
    public void Interact()
    {
        _lightOrigin.enabled=!_lightOrigin.enabled;
        
    }

    
}
