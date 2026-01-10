/*****************************************************************************
* Project : 3D Charakter Steuerung (K2, S2, S3)
* File    : JumpBehaviour
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* *
* History :
* xx.xx.2025 ER Created
******************************************************************************/
using UnityEngine;

public class JumpBehaviour
{
    private Rigidbody _rb;
    private JumpConfig _jumpConfig;

    private bool _groundJumpAvailable = true;
    public JumpBehaviour(Rigidbody rb,JumpConfig jumpConfig)
    {
        _rb = rb;
        _jumpConfig = jumpConfig;        
    }

    public bool Jump(JumpStateData jumpData)
    {      
        if (CanJumpOnGround(jumpData.IsGrounded,jumpData.IsCoyoteActive))
        {
            PerformJumpPhysic();
            _groundJumpAvailable = false;
            return true;
        }  
        return false;
    }

    /// <summary>
    /// Applies the vertical jump force to the Rigidbody2D.
    /// </summary>
    private void PerformJumpPhysic()
    {
        Vector3 currentVelocity = _rb.linearVelocity;
        currentVelocity.y = _jumpConfig.JumpForce;
        _rb.linearVelocity = currentVelocity;
    }

    /// <summary>
    /// Resets the ground jump availability.
    /// </summary>
    public void ResetJumpCountGround()
    {
        _groundJumpAvailable = true;
    }

    /// <summary>
    /// Determines whether a ground or coyote-time jump is allowed.
    /// </summary>
    /// <param name="isGrounded">
    /// Indicates whether the player is currently grounded.
    /// </param>
    /// <param name="isCoyoteAktive">
    /// Indicates whether coyote time is currently active.
    /// </param>
    /// <returns>
    /// True if a ground jump is allowed; otherwise, false.
    /// </returns>
    private bool CanJumpOnGround(bool isGrounded, bool isCoyoteAktive)
    {
        return _groundJumpAvailable && (isGrounded || isCoyoteAktive);
    }

}
