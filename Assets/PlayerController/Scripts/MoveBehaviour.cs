using Unity.VisualScripting;
using UnityEngine;

public class MoveBehaviour
{
    private Rigidbody _rb;

    // Parameter config later
    float _moveSpeed = 5f;


    public MoveBehaviour(Rigidbody rb)
    {
        _rb = rb;
       
    }

    public void Move(Vector2 moveInput,bool isGrounded)
    {
        Vector3 velocity;
        if(isGrounded)
        {
            velocity = _rb.linearVelocity;
            velocity= OnGround(moveInput, velocity);
            _rb.linearVelocity = velocity;
        }


    }

    private Vector3 OnGround(Vector2 moveInput, Vector3 currentVelocity)
    {
        currentVelocity.x = moveInput.x * _moveSpeed;
        currentVelocity.z = moveInput.y * _moveSpeed;
        return currentVelocity;
    }


}
