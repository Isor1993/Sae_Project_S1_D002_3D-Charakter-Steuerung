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
using UnityEngine.UIElements;

public class Ball :MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _directionOrgin;
    [SerializeField] private float _forwardForce = 10f;
    [SerializeField] private float _upForce = 3f;
    public void Interact()
    {
        Shoot();
    }

    private void Shoot()
    {
        Vector3 forward = _directionOrgin.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 direction =
            forward * _forwardForce +
            Vector3.up * _upForce;

        _rb.AddForce(direction, ForceMode.Impulse);
    }
}

