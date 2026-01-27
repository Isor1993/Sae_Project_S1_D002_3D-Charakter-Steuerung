/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : JumpConfig.cs
* Date    : 28.01.2026
* Author  : Eric Rosenberg
*
* Description :
* ScriptableObject that stores all configurable jump-related parameters.
* Allows jump behavior to be adjusted in the Unity Editor without
* modifying code.
*
* History :
* 28.01.2026 ER Created
******************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "JumpConfig", menuName = "Scriptable Objects/JumpConfig")]
public class JumpConfig : ScriptableObject
{
    [Header("JumpBehaviour Settings")]
    [Tooltip("Defines the vertical force applied when the player jumps. Default : 6f")]
    [SerializeField] private float _jumpforce = 6f;

    [Tooltip("Defines the duration after leaving the ground in which a jump is still allowed. Default: 0.2f")]
    [SerializeField] private float _coyoteTime = 0.2f;

    [Tooltip("Defines how long a buffered jump input is stored before it expires. Default : 0.15f")]
    [SerializeField] private float _jumpBufferTime = 0.15f;

    /// <summary>
    /// Gets the vertical force applied to the Rigidbody when a jump is executed.
    /// </summary>
    public float JumpForce => _jumpforce;

    /// <summary>
    /// Gets the duration in seconds for which a buffered jump input remains valid.
    /// </summary>
    public float JumpBufferTime => _jumpBufferTime; 

    /// <summary>
    /// Gets the coyote time duration in seconds, allowing a jump shortly after leaving the ground.
    /// </summary>
    public float CoyoteTime => _coyoteTime;
}
