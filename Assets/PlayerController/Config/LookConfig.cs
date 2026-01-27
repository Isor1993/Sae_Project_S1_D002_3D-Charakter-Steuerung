/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : LookConfig.cs
* Date    : 28.01.2025
* Author  : Eric Rosenberg
*
* Description :
* ScriptableObject that stores all configuration values related to
* camera and look movement. Allows mouse and controller sensitivity
* as well as vertical look limits to be adjusted in the Unity Editor
* without changing code.
*
* History :
* 28.01.2025 ER Created
******************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "LookConfig", menuName = "Scriptable Objects/LookConfig")]
public class LookConfig : ScriptableObject
{
    [Header("Settings")]
    [Tooltip("Defines how sensitive the mouse look movement is.")]
    [Range(0, 1)]
    [SerializeField] private float _mouseSensitivity = 0.279f;

    [Tooltip("Defines how sensitive the controller look movement is.")]
    [Range(0, 240)]
    [SerializeField] private float _controllerSensitivity = 120f;

    [Tooltip("Defines the maximum upward look angle.")]
    [SerializeField] private float _maxLookUp;

    [Tooltip("Defines the maximum downward look angle.")]
    [SerializeField] private float _minLookDown;


    /// <summary>
    /// Gets the mouse look sensitivity value.
    /// </summary>
    public float Sensitivity => _mouseSensitivity;

    /// <summary>
    /// Gets the controller look sensitivity value.
    /// </summary>
    public float ControllerSensitivity => _controllerSensitivity;

    /// <summary>
    /// Gets the maximum upward look angle.
    /// </summary>
    public float MaxLookUp => _maxLookUp;

    /// <summary>
    /// Gets the maximum downward look angle.
    /// </summary>
    public float MinLookDown => _minLookDown;
}
