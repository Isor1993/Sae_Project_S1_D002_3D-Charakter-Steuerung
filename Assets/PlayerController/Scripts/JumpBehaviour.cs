/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : JumpBehaviour.cs
* Date    : 28.01.2026
* Author  : Eric Rosenberg
*
* Description :
* Handles jump logic for a character using a Rigidbody.
* Determines whether a jump is allowed based on grounded state,
* coyote time and internal jump availability, and applies
* vertical velocity when a jump is executed.
*
* History :
* 28.01.2026 ER Created
******************************************************************************/
using UnityEngine;

public class JumpBehaviour
{
    [Header("Dependencies")]
    [Tooltip("Rigidbody used to apply jump velocity.")]
    private Rigidbody _rb;

    [Tooltip("Jump configuration asset.")]
    private JumpConfig _jumpConfig;

    private bool _groundJumpAvailable = true;

    /// <summary>
    /// Creates a new JumpBehaviour instance.
    /// </summary>
    /// <param name="rb">
    /// Rigidbody that will receive the jump velocity.
    /// </param>
    /// <param name="jumpConfig">
    /// Jump configuration asset (force, coyote time, etc.).
    /// </param>
    public JumpBehaviour(Rigidbody rb, JumpConfig jumpConfig)
    {
        _rb = rb;
        _jumpConfig = jumpConfig;
    }

    /// <summary>
    /// Attempts to execute a jump based on the provided jump state data.
    /// </summary>
    /// <param name="jumpData">
    /// Struct or object containing current jump-related state information
    /// such as grounded state and coyote time status.
    /// </param>
    /// <returns>
    /// True if a jump was successfully performed; otherwise, false.
    /// </returns>
    public bool Jump(JumpStateData jumpData)
    {
        if (CanJumpOnGround(jumpData.IsGrounded, jumpData.IsCoyoteActive))
        {
            PerformJumpPhysic();
            _groundJumpAvailable = false;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Applies the vertical jump force to the Rigidbody
    /// by directly modifying its Y-axis velocity.
    /// </summary>
    private void PerformJumpPhysic()
    {
        Vector3 currentVelocity = _rb.linearVelocity;
        currentVelocity.y = _jumpConfig.JumpForce;
        _rb.linearVelocity = currentVelocity;
    }

    /// <summary>
    /// Resets the ground jump availability.
    /// Should typically be called when the character lands.
    /// </summary>
    public void ResetJumpCountGround()
    {
        _groundJumpAvailable = true;
    }

    /// <summary>
    /// Determines whether a ground-based jump is allowed.
    /// A jump is allowed if a ground jump is available and
    /// the character is either grounded or within coyote time.
    /// </summary>
    /// <param name="isGrounded">
    /// True if the character is currently touching the ground.
    /// </param>
    /// <param name="isCoyoteAktive">
    /// True if the coyote time window is still active.
    /// </param>
    /// <returns>
    /// True if a jump can be executed; otherwise, false.
    /// </returns>
    private bool CanJumpOnGround(bool isGrounded, bool isCoyoteAktive)
    {
        return _groundJumpAvailable && (isGrounded || isCoyoteAktive);
    }
}
