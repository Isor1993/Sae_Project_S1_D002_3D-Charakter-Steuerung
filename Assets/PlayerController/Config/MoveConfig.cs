/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : MoveConfig.cs
* Date    : 28.01.2026
* Author  : Eric Rosenberg
*
* Description :
* ScriptableObject that stores all movement-related configuration values.
* Defines ground movement speeds, acceleration behavior and air control
* parameters. All values can be adjusted in the Unity Editor without
* modifying code.
*
* History :
* 28.01.2026 ER Created
******************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "MoveConfig", menuName = "Scriptable Objects/MoveConfig")]
public class MoveConfig : ScriptableObject
{
    [Header("MoveBehaviour Settings")]
    [Tooltip("Base movement speed applied when the character is walking.")]
    [SerializeField]private float _moveSpeed = 4f;

    [Tooltip("Maximum movement speed applied while sprinting.")]
    [SerializeField] private float _sprintSpeed = 6f;

    [Header("Dynamics")]
    [Tooltip("Rate at which the character accelerates towards the target speed when movement input is applied.")]
    [SerializeField] private float _deceleration = 40f;

    [Tooltip("Rate at which the character slows down when no movement input is present.")]
    [SerializeField] private float _acceleration = 30f;

    [Tooltip("Initial horizontal movement speed when entering the air.")]
    [SerializeField] private float _airStartSpeed = 1f;

    [Tooltip("Braking force applied to horizontal movement while airborne.")]
    [SerializeField] private float _airBrake = 8f;

    [Header("Air Control")]
    [Range(0f, 3f)]
    [Tooltip("Multiplier that reduces acceleration and deceleration while the character is airborne.")]
    [SerializeField] private float _airControll = 2f;


    /// <summary>
    /// Gets the base walking movement speed.
    /// </summary>
    public float MoveSpeed => _moveSpeed;

    /// <summary>
    /// Gets the sprinting movement speed.
    /// </summary>
    public float SprintSpeed => _sprintSpeed;

    /// <summary>
    /// Gets the deceleration rate when movement input is applied.
    /// </summary>
    public float Deceleration => _deceleration;

    /// <summary>
    /// Gets the acceleration rate when no movement input is present.
    /// </summary>
    public float Acceleration => _acceleration;

    /// <summary>
    /// Gets the initial horizontal movement speed applied in the air.
    /// </summary>
    public float AirStartSpeed => _airStartSpeed;

    /// <summary>
    /// Gets the air braking force applied while airborne.
    /// </summary>
    public float AirBrake => _airBrake;

    /// <summary>
    /// Gets the air control multiplier.
    /// </summary>
    public float AirControll => _airControll;
}

