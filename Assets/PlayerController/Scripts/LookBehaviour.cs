/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : Ball.cs
* Date    : 28.01.2025
* Author  : Eric Rosenberg
*
* Description :
* 
* 
* 
* History :
* 28.01.2025 ER Created
******************************************************************************/
using UnityEngine;

public class LookBehaviour
{

    //--- Dependencies ---

    private readonly LookConfig _lookConfig;
    private readonly Rigidbody _rb;
    private readonly Transform _camTransform;

    //--- Fields ----
    private float _yaw;
    private float _pitch;



    public LookBehaviour(Rigidbody rb, LookConfig lookConfig, Transform camTransform)
    {
        _lookConfig = lookConfig;
        _rb = rb;
        _camTransform = camTransform;


    }

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
