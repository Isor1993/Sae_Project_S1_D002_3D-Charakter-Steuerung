/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : Ball.cs
* Date    : 28.01.2026
* Author  : Eric Rosenberg
*
* Description :
* Interactable object that applies an impulse force when interacted with.
* The force is applied in the forward direction of a given transform,
* combined with an upward force to simulate a kick or shot.
*
* History :
* 28.01.2026 ER Created
******************************************************************************/
using UnityEngine;

public class Ball :MonoBehaviour, IInteractable
{
    [Header("Dependencies")]
    [Tooltip("Rigidbody used to apply physics forces to the ball.")]
    [SerializeField] private Rigidbody _rb;

    [Tooltip("Transform that defines the forward direction of the shot.")]
    [SerializeField] private Transform _directionOrigin;

    [Header("Settings")]
    [Tooltip("Forward impulse force applied to the ball.")]
    [SerializeField] private float _forwardForce = 10f;

    [Tooltip("Upward impulse force applied to the ball.")]
    [SerializeField] private float _upForce = 3f;

    /// <summary>
    /// Called when the player interacts with the ball.
    /// Triggers the shooting behavior.
    /// </summary>
    public void Interact()
    {
        Shoot();
    }

    /// <summary>
    /// Applies an impulse force to the Rigidbody.
    /// The forward direction is flattened on the Y-axis to ensure
    /// horizontal movement, while an additional upward force
    /// creates a lifting effect.
    /// </summary>
    private void Shoot()
    {
        Vector3 forward = _directionOrigin.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 direction =
            forward * _forwardForce +
            Vector3.up * _upForce;

        _rb.AddForce(direction, ForceMode.Impulse);
    }
}

