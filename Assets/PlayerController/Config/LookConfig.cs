using UnityEngine;

[CreateAssetMenu(fileName = "LookConfig", menuName = "Scriptable Objects/LookConfig")]
public class LookConfig : ScriptableObject
{
    [Header("Settings")]
    [Tooltip("Defines how sensitiv the lookmovement is.")]
    [Range(0, 1)]
    [SerializeField] private float _mouseSensitivity = 0.279f;
    [Range(0, 240)]
    [SerializeField] private float _controllerSensitivity = 120f;
    [SerializeField] private float _maxLookUp;
    [SerializeField] private float _minLookDown;


    public float Sensitivity => _mouseSensitivity;

    public float ControllerSensitivity => _controllerSensitivity;

    public float MaxLookUp => _maxLookUp;

    public float MinLookDown => _minLookDown;

}
