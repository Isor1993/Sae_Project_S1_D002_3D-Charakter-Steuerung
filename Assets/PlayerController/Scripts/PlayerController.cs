/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : PlayerController.cs
* Date    : 28.01.2026
* Author  : Eric Rosenberg
*
* Description :
* Central controller that coordinates player input, movement, jumping,
* looking, grounding checks and interaction handling.
* Acts as the main integration point for all behavior modules.
*
* History :
* 28.01.2026 ER Created
******************************************************************************/
using UnityEditor.Experimental.GraphView;
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
    [Header("Dependencies")]
    [Tooltip("Rigibody from Player")]
    [SerializeField] private Rigidbody _rb;

    [Tooltip("Ground check component")]
    [SerializeField] private GroundCheck _groundCheck;

    [Tooltip("Camera transform")]
    [SerializeField] private Transform _camTransform;

    [Tooltip("Raycast target provider")]
    [SerializeField] private RaycastTargetProvider _targetProvider;

    [Tooltip("Target interaction handler")]
    [SerializeField] private TargetHandler _targetHandler;

    [Tooltip("Interaction UI panel")]
    [SerializeField] private GameObject _interactionPanel;


    [Tooltip("MoveConfig Asset")]
    [SerializeField] private MoveConfig _moveConfig;

    [Tooltip("JumpConfig Asset")]
    [SerializeField] private JumpConfig _jumpConfig;

    [Tooltip("LookConfig Asset")]
    [SerializeField] private LookConfig _lookConfig;

    //--- Fields ---
    private MoveBehaviour _moveBehaviour;
    private JumpBehaviour _jumpBehaviour;
    private LookBehaviour _lookBehaviour;

    // --- Input State ---
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private JumpStateData JumpData;

    // --- Jump Timers ---
    private float _coyoteTimeCounter = 0f;
    private float _jumpBufferCounter = 0f;

    // --- Player State ---
    private bool _isGrounded = false;
    private bool _wasGrounded;
    private bool _isSprinting = false;
    private bool _IsControllerInput;

    // --- Input System ---
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

    private void Awake()
    {
        MappingInptutAction();
        InitializeData();
    }

    private void Start()
    {
        CurssorSettings();
    }

    private void OnEnable()
    {
        _inputAction.Player.Enable();
    }

    private void OnDisable()
    {
        _inputAction?.Player.Disable();
    }

    void Update()
    {
        MappingInput();
        _lookBehaviour.Look(_lookInput, _IsControllerInput);
        if (_jump.WasPressedThisFrame())
        {
            ResetJumpBufferTimer();
        }
        SetIsSprinting();
        HandleInteraction();
    }

    private void FixedUpdate()
    {
        UpdateGroundState();
        ReduceCoyoteTimer();
        ReduceJumpBuffer();
        HandleGroundTransition();
        HandleMovement(_isGrounded);
        HandleJump();
    }

    /// <summary>
    /// Reads movement and look input values.
    /// </summary>
    private void MappingInput()
    {
        _moveInput = _move.ReadValue<Vector2>();
        _IsControllerInput = IsControllerLook();
        _lookInput = _look.ReadValue<Vector2>();
    }

    /// <summary>
    /// Handles interaction logic with detected targets.
    /// </summary>
    private void HandleInteraction()
    {
        if (_targetProvider.TryGetTarget(out var hit) &&
        hit.collider.TryGetComponent<IInteractable>(out var interactable))
        {
            if (!IsControllerLook())
            {
                _interactionPanel.SetActive(true);
            }
            else
            {
                _interactionPanel.SetActive(false);
            }

            if (_interact.WasPressedThisFrame())
            {
                interactable.Interact();
            }
        }
        else
        {
            _interactionPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Updates grounded state and tracks ground transitions.
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
    /// Resets the coyote time counter.
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

    /// <summary>
    ///  Resets the jump buffer timer.
    /// </summary>
    private void ResetJumpBufferTimer()
    {
        _jumpBufferCounter = _jumpConfig.JumpBufferTime;
    }

    /// <summary>
    /// Handles transitions between grounded and airborne states.
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
    /// Attempts to execute a jump if buffered input is available.
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
        return JumpData;
    }

    /// <summary>
    /// Updates sprinting state based on input.
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
    /// Maps input actions from the Input System.
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
    /// Initializes all behavior modules and state data.
    /// </summary>
    private void InitializeData()
    {
        _moveBehaviour = new(_rb, _moveConfig);
        _jumpBehaviour = new(_rb, _jumpConfig);
        _lookBehaviour = new(_rb, _lookConfig, _camTransform);
        JumpData = new JumpStateData();
    }

    /// <summary>
    /// Applies horizontal movement logic based on the current grounded state.
    /// Delegates movement handling to the MoveBehaviour module.
    /// </summary>
    /// <param name="isGrounded">
    /// True if the player is currently grounded; false if airborne.
    /// </param>
    private void HandleMovement(bool isGrounded)
    {
        _moveBehaviour.Move(_moveInput, isGrounded, _isSprinting);
    }

    /// <summary>
    /// Determines whether the current look input originates from a controller.
    /// Used to select the appropriate look sensitivity.
    /// </summary>
    /// <returns>
    /// True if the active look input device is a gamepad; otherwise, false.
    /// </returns>
    private bool IsControllerLook()
    {
        return _look.activeControl?.device is Gamepad;
    }
  

    /// <summary>
    /// Applies cursor lock and visibility settings.
    /// </summary>
    private static void CurssorSettings()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
