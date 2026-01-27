/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : MoveBehaviour.cs
* Date    : 28.01.2026
* Author  : Eric Rosenberg
*
* Description :
* Handles horizontal player movement based on input, grounded state
* and sprint state. Uses different movement logic for ground and air:
* - On ground: accelerates/decelerates towards a target velocity.
* - In air: applies limited air control and optional air braking.
*
* History :
* 28.01.2026 ER Created
******************************************************************************/
using UnityEngine;

public class MoveBehaviour
{
    //--- Dependencies ---
    private Rigidbody _rb;
    private MoveConfig _moveConfig;

    /// <summary>
    /// Creates a new MoveBehaviour instance.
    /// </summary>
    /// <param name="rb">
    /// Rigidbody that will be moved by setting its velocity.
    /// </param>
    /// <param name="moveConfig">
    /// Movement configuration asset (speed, acceleration, air control, etc.).
    /// </param>
    public MoveBehaviour(Rigidbody rb, MoveConfig moveConfig)
    {
        _rb = rb;
        _moveConfig = moveConfig;
    }

    /// <summary>
    /// Updates the Rigidbody's horizontal movement based on input and state.
    /// Delegates to ground or air movement logic depending on grounded state.
    /// </summary>
    /// <param name="moveInput">
    /// 2D movement input (X = strafe, Y = forward/backward).
    /// Expected range typically -1..1.
    /// </param>
    /// <param name="isGrounded">
    /// True if the character is currently grounded.
    /// </param>
    /// <param name="isSprinting">
    /// True if sprinting is currently active (uses sprint speed on ground).
    /// </param>
    public void Move(Vector2 moveInput, bool isGrounded, bool isSprinting)
    {
        Vector3 velocity = _rb.linearVelocity;
        if (isGrounded)
        {
            velocity = OnGround(moveInput, velocity, isSprinting);
        }
        else
        {
            velocity = InAir(moveInput, velocity);
        }

        _rb.linearVelocity = velocity;
    }

    /// <summary>
    /// Ground movement logic:
    /// Builds a movement direction from input relative to the character's
    /// forward and right vectors, then accelerates/decelerates the current
    /// horizontal velocity towards a target velocity.
    /// </summary>
    /// <param name="moveInput">
    /// 2D movement input (X = strafe, Y = forward/backward).
    /// </param>
    /// <param name="currentVelocity">
    /// Current Rigidbody velocity.
    /// </param>
    /// <param name="isSprinting">
    /// True if sprint speed should be used; otherwise walk speed is used.
    /// </param>
    /// <returns>
    /// Updated velocity after applying ground movement.
    /// </returns>
    private Vector3 OnGround(Vector2 moveInput, Vector3 currentVelocity, bool isSprinting)
    {
        Vector3 moveDir = (_rb.transform.forward * moveInput.y + _rb.transform.right * moveInput.x).normalized;

        float targetSpeed = isSprinting ? _moveConfig.SprintSpeed : _moveConfig.MoveSpeed;

        Vector3 targetVelocity = moveDir * targetSpeed;

        float accel = moveInput.sqrMagnitude > 0f ? _moveConfig.Acceleration : _moveConfig.Deceleration;

        currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, targetVelocity.x, accel * Time.fixedDeltaTime);

        currentVelocity.z = Mathf.MoveTowards(currentVelocity.z, targetVelocity.z, accel * Time.fixedDeltaTime);

        return currentVelocity;
    }

    /// <summary>
    /// Air movement logic:
    /// Applies limited air control by gradually steering the horizontal velocity
    /// direction towards the input direction. If the input direction is opposite
    /// to the current movement direction, a braking behavior is applied.
    /// </summary>
    /// <param name="moveInput">
    /// 2D movement input (X = strafe, Y = forward/backward).
    /// </param>
    /// <param name="currentVelocity">
    /// Current Rigidbody velocity.
    /// </param>
    /// <returns>
    /// Updated velocity after applying air movement.
    /// </returns>
    private Vector3 InAir(Vector2 moveInput, Vector3 currentVelocity)
    {
        if (moveInput.sqrMagnitude <= 0f)
            return currentVelocity;

        Vector3 horizontal = new Vector3(currentVelocity.x, 0f, currentVelocity.z);

        Vector3 inputDir =
            (_rb.transform.forward * moveInput.y +
             _rb.transform.right * moveInput.x).normalized;

        float speed = horizontal.magnitude;

        if (speed < 0.1f)
        {
            horizontal = inputDir * _moveConfig.AirStartSpeed;
        }

        else
        {
            Vector3 currentDir = horizontal.normalized;
            float dot = Vector3.Dot(currentDir, inputDir);

            if (dot > 0f)
            {               
                Vector3 newDir = Vector3.Slerp(
                    currentDir,
                    inputDir,
                    _moveConfig.AirControll * Time.fixedDeltaTime
                );

                horizontal = newDir * speed;
            }
            else
            {
                speed = Mathf.MoveTowards(
                    speed,
                    0f,
                    _moveConfig.AirBrake * Time.fixedDeltaTime
                );

                horizontal = currentDir * speed;
            }
        }

        currentVelocity.x = horizontal.x;
        currentVelocity.z = horizontal.z;

        return currentVelocity;
    }
}
