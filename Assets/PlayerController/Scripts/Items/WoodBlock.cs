using UnityEngine;

public class WoodBlock : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameObject.Destroy(gameObject);

    }
}
