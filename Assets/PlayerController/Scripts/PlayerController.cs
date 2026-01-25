/*****************************************************************************
* Project : 3D Charakter Steuerung (K2, S2, S3)
* File    : PlayerController
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* *
* History :
* xx.xx.2025 ER Created
******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Data container holding all relevant state information required
/// to evaluate and execute jump behaviour.
/// </summary>
public struct JumpStateData
{
    public bool IsGrounded;
    public bool IsCoyoteActive;   
    public bool MultiJumpEnabled;
    
}
public class PlayerController : MonoBehaviour
{
    //--- Depedndencies ---
    [Header("Dependencies")]
    [Tooltip("Rigibody from Player")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private Transform _camTransform;

    [Tooltip("MoveConfig Asset")]
    [SerializeField] private MoveConfig _moveConfig;
    [SerializeField] private JumpConfig _jumpConfig;
    [SerializeField] private LookConfig _lookConfig;

    [Tooltip("Activates Multi Jump")]
    [SerializeField] private bool _multiJumpEnabled = true;

    private MoveBehaviour _moveBehaviour;
    private JumpBehaviour _jumpBehaviour;
    private LookBehaviour _lookBehaviour;

    //--- Fields ---
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private JumpStateData JumpData;

    private float _coyoteTimeCounter = 0f;
    private float _jumpBufferCounter = 0f;


    private bool _isGrounded = false;
    private bool _wasGrounded;
    private bool _isSprinting = false;

    private PlayerInputAction _inputAction;
    private InputAction _move;
    private InputAction _look;
    private InputAction _jump;
    private InputAction _sprint;
    private InputAction _interact;

    /// <summary>
    /// True during the frame in which the player has just landed on the ground.
    /// </summary>
    public bool JustLanded => !_wasGrounded && _isGrounded;

    /// <summary>
    /// True during the frame in which the player has just left the ground.
    /// </summary>
    public bool JustLeftGround => _wasGrounded && !_isGrounded;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        MappingInptutAction();
        InitializeData();
    }
    
    private void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible=false;
    }
        
    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        _inputAction.Player.Enable();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        _inputAction?.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = _move.ReadValue<Vector2>();
        if (_jump.WasPressedThisFrame())
        {
            ResetJumpBufferTimer();
        }
        _lookInput= _look.ReadValue<Vector2>();
        Debug.Log($"{_lookInput.x}||{_lookInput.y}");
        

        SetIsSprinting();
    }

    /// <summary>
    /// 
    /// </summary>
    private void FixedUpdate()
    {
        UpdateGroundState();
        ReduceCoyoteTimer();
        ReduceJumpBuffer();
        HandleGroundTransition();                
        HandleMovement(_isGrounded);
        HandleJump();
        _lookBehaviour.Look(_lookInput);
    }

    /// <summary>
    /// Updates the grounded state and tracks ground transitions.
    /// </summary>
    private void UpdateGroundState()
    {
        _wasGrounded = _isGrounded;
        _isGrounded = _groundCheck.IsGrounded();
    }

    /// <summary>
    /// Decreases the coyote time counter over time.
    /// </summary>
    private void ReduceCoyoteTimer()
    {
        if (!_isGrounded && _coyoteTimeCounter > 0f)
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ResetCoyoteTimer()
    {
        _coyoteTimeCounter = _jumpConfig.CoyoteTime;
    }

    /// <summary>
    /// Checks whether coyote time is currently active.
    /// </summary>
    /// <returns>
    /// True if coyote time is active; otherwise, false.
    /// </returns>
    private bool IsCoyoteTimeActive()
    {
        return _coyoteTimeCounter > 0f;
    }

    /// <summary>
    /// Decreases the jump buffer timer over time.
    /// </summary>
    private void ReduceJumpBuffer()
    {
        if (_jumpBufferCounter > 0f)
        {
            _jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void ResetJumpBufferTimer()
    {
        _jumpBufferCounter = _jumpConfig.JumpBufferTime;
    }

    /// <summary>
    /// 
    /// </summary>
    private void HandleGroundTransition()
    {
        if (JustLanded)
        {
            ResetGroundJumpCounter();
        }
        if (JustLeftGround)
        {
            ResetCoyoteTimer();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void HandleJump()
    {       
        if (_jumpBufferCounter <= 0f)
            return;

        JumpData = BuildJumpData(JumpData);
       
        if (_jumpBehaviour.Jump(JumpData))
        {            
            _jumpBufferCounter = 0f;
        }
    }    

    /// <summary>
    /// Builds and returns the current jump state data.
    /// </summary>
    /// <param name="JumpData">
    /// Existing jump state data instance to populate.
    /// </param>
    /// <returns>
    /// Fully populated JumpStateData struct.
    /// </returns>
    private JumpStateData BuildJumpData(JumpStateData JumpData) 
    {
        JumpData.IsGrounded = _isGrounded;
        JumpData.IsCoyoteActive = IsCoyoteTimeActive();        
        JumpData.MultiJumpEnabled = _multiJumpEnabled;       

        return JumpData;
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
    /// Resets the ground jump availability.
    /// </summary>
    private void ResetGroundJumpCounter()
    {
        _jumpBehaviour.ResetJumpCountGround();        
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

    /// <summary>
    /// 
    /// </summary>
    private void InitializeData()
    {
        _moveBehaviour = new(_rb, _moveConfig);
        _jumpBehaviour = new(_rb, _jumpConfig);
        _lookBehaviour = new(_rb, _lookConfig,_camTransform);
        JumpData = new JumpStateData();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isGrounded"></param>
    private void HandleMovement(bool isGrounded)
    {
        _moveBehaviour.Move(_moveInput, isGrounded, _isSprinting);
    }
}
