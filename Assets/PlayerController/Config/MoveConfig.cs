/*****************************************************************************
* Project : 3D Charakter Steuerung (K2, S2, S3)
* File    : MoveConfig
* Date    : xx.xx.2025
* Author  : Eric Rosenberg
*
* Description :
* *
* History :
* xx.xx.2025 ER Created
******************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "MoveConfig", menuName = "Scriptable Objects/MoveConfig")]
public class MoveConfig : ScriptableObject
{
    [Header("Movement")]
    [Tooltip("Base movement speed applied when the character is walking.")]
    public float MoveSpeed = 5f;
    [Tooltip("Maximum movement speed applied while sprinting.")]
    public float SprintSpeed = 10f;
    [Header("Dynamics")]
    [Tooltip("Rate at which the character accelerates towards the target speed when movement input is applied.")]
    public float Deceleration = 40f;
    [Tooltip("Rate at which the character slows down when no movement input is present.")]
    public float Acceleration = 30f;
    [Header("Air Control")]
    [Range(0f, 1f)]
    [Tooltip("Multiplier that reduces acceleration and deceleration while the character is airborne. A value of 0 disables air control, while 1 allows full ground-like control.")]
    public float AirControll = 0.8f;

}

