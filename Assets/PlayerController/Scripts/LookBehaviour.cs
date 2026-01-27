/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : LookBehaviour.cs
* Date    : 28.01.2026
* Author  : Eric Rosenberg
*
* Description :
* Handles camera and character rotation based on look input.
* Separates horizontal rotation (yaw) applied to the Rigidbody
* from vertical rotation (pitch) applied to the camera transform.
* Supports both mouse and controller input with configurable sensitivity.
*
* History :
* 28.01.2026 ER Created
******************************************************************************/
using UnityEngine;

public class LookBehaviour
{

    //--- Dependencies ---
    private readonly LookConfig _lookConfig;
    private readonly Rigidbody _rb;
    private readonly Transform _camTransform;
    //--- Fields ---
    private float _yaw;
    private float _pitch;

    /// <summary>
    /// Creates a new LookBehaviour instance.
    /// </summary>
    /// <param name="rb">
    /// Rigidbody representing the player body rotation.
    /// </param>
    /// <param name="lookConfig">
    /// Look configuration asset (sensitivities and angle limits).
    /// </param>
    /// <param name="camTransform">
    /// Transform of the camera that will be rotated vertically.
    /// </param>
    public LookBehaviour(Rigidbody rb, LookConfig lookConfig, Transform camTransform)
    {
        _lookConfig = lookConfig;
        _rb = rb;
        _camTransform = camTransform;
    }

    /// <summary>
    /// Applies look rotation based on input.
    /// Handles horizontal rotation (yaw) for the character body
    /// and vertical rotation (pitch) for the camera.
    /// </summary>
    /// <param name="lookInput">
    /// 2D look input (X = horizontal, Y = vertical).
    /// </param>
    /// <param name="isController">
    /// True if input comes from a controller; otherwise mouse input is assumed.
    /// </param>
    public void Look(Vector2 lookInput, bool isController)
    {
        float multiplier = isController ? _lookConfig.ControllerSensitivity * Time.deltaTime : _lookConfig.Sensitivity;

        _yaw += lookInput.x * multiplier;
        _pitch -= lookInput.y * multiplier;
        _pitch = Mathf.Clamp(_pitch, _lookConfig.MinLookDown, _lookConfig.MaxLookUp);

        _rb.rotation = Quaternion.Euler(0, _yaw, 0);
        _camTransform.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }
}
