using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //--- Depedndencies ---
    [Header("Dependencies")]
    [Tooltip("Rigibody from Player")]
    [SerializeField] private Rigidbody _rb;
    [Tooltip("MoveConfig Asset")]
    [SerializeField] private MoveConfig _moveConfig;
    private MoveBehaviour _moveBehaviour;

    //--- Fields ---
    private Vector2 _moveInput;

    private bool _isGrounded = true;
    private bool _isSprinting = false;

    private PlayerInputAction _inputAction;
    private InputAction _move;
    private InputAction _look;
    private InputAction _jump;
    private InputAction _sprint;
    private InputAction _interact;





    private void Awake()
    {
        MappingInptutAction();
        _moveBehaviour = new(_rb, _moveConfig);


    }


    private void OnEnable()
    {
        _inputAction.Player.Enable();

    }
    private void OnDisable()
    {
        _inputAction?.Player.Disable();
    }
    private void FixedUpdate()
    {
        HandleMovement(_isGrounded);

    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = _move.ReadValue<Vector2>();
        SetIsSprinting();


    }

    /// <summary>
    /// 
    /// </summary>
    private void SetIsSprinting()
    {
        if (_sprint.IsPressed())
        {
            _isSprinting = true;
        }
        else
        {
            _isSprinting = false;
        }
    }

    /// <summary>
    /// Mapping Input Actions
    /// </summary>
    private void MappingInptutAction()
    {
        _inputAction = new();
        _move = _inputAction.Player.Move;
        _jump = _inputAction.Player.Jump;
        _sprint = _inputAction.Player.Sprint;
        _look = _inputAction.Player.Look;
        _interact = _inputAction.Player.Interact;
    }

    private void HandleMovement(bool isGrounded)
    {
        _moveBehaviour.Move(_moveInput, isGrounded, _isSprinting);
    }
}
