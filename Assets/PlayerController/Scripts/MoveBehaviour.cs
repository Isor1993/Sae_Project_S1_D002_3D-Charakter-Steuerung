using Unity.VisualScripting;
using UnityEngine;

public class MoveBehaviour
{
    private Rigidbody _rb;
    private MoveConfig _moveConfig;

    // Parameter config later



    public MoveBehaviour(Rigidbody rb, MoveConfig moveConfig)
    {
        _rb = rb;
        _moveConfig = moveConfig;

    }

    public void Move(Vector2 moveInput, bool isGrounded, bool isSprinting)
    {
        Vector3 velocity;
        if (isGrounded)
        {
            velocity = _rb.linearVelocity;
            velocity = OnGround(moveInput, velocity, isSprinting);
            _rb.linearVelocity = velocity;
        }


    }

    private Vector3 OnGround(Vector2 moveInput, Vector3 currentVelocity, bool isSprinting)
    {
        var moveSpeed = moveInput * _moveConfig.MoveSpeed;
        var sprintSpeed = moveInput * _moveConfig.SprintSpeed;
        var acceleration = _moveConfig.Acceleration;
        var deceleration = _moveConfig.Deceleration;

        if (!isSprinting)
        {
            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, moveSpeed.x, acceleration * Time.fixedDeltaTime);
            currentVelocity.z = Mathf.MoveTowards(currentVelocity.z, moveSpeed.y, acceleration * Time.fixedDeltaTime);
            Debug.Log($"{currentVelocity.x}||{currentVelocity.z}");
        }
        else
        {
            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, sprintSpeed.x, acceleration * Time.fixedDeltaTime);
            currentVelocity.z = Mathf.MoveTowards(currentVelocity.z, sprintSpeed.y, acceleration * Time.fixedDeltaTime);
            Debug.Log($"{currentVelocity.x}||{currentVelocity.z}");
        }

        return currentVelocity;
    }


}
