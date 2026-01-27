/*****************************************************************************
* Project : 3D Character Controller (K2, S2, S3)
* File    : RaycastTargetProvider.cs
* Date    : 28.01.2026
* Author  : Eric Rosenberg
*
* Description :
* Provides a raycast-based target detection system.
* Casts a ray from a defined origin forward direction to detect
* interactable objects within a specified distance and layer mask.
*
* History :
* 28.01.2026 ER Created
******************************************************************************/
using UnityEngine;

public class RaycastTargetProvider : MonoBehaviour
{
    [Header("Dependencies")]
    [Tooltip("Layer mask used to filter detectable target objects.")]
    [SerializeField] private LayerMask _targetLayerMask;

    [Tooltip("Transform that defines the raycast origin and direction.")]
    [SerializeField] private Transform _orgin;

    [Header("Settings")]
    [Tooltip("Maximum distance the raycast can reach.")]
    [SerializeField] private float _maxDistance = 0f;

    /// <summary>
    /// Attempts to detect a target using a raycast.
    /// Returns detailed hit information if a target is detected.
    /// </summary>
    /// <param name="hit">
    /// RaycastHit containing information about the detected object.
    /// </param>
    /// <returns>
    /// True if a target was hit by the raycast; otherwise, false.
    /// </returns>
    public bool TryGetTarget(out RaycastHit hit)
    {
        bool result = Physics.Raycast(_orgin.position, _orgin.forward, out hit, _maxDistance, _targetLayerMask);
        return result;
    }

    /// <summary>
    /// Attempts to detect a target using a raycast without returning hit data.
    /// </summary>
    /// <returns>
    /// True if a target was hit by the raycast; otherwise, false.
    /// </returns>
    public bool TryGetTarget()
    {
        bool result = Physics.Raycast(_orgin.position, _orgin.forward, _maxDistance, _targetLayerMask);
        return result;
    }
}
