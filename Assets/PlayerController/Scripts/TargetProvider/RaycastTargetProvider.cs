using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RaycastTargetProvider : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 0f;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private Transform _orgin;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    public bool TryGetTarget(out RaycastHit hit)
    {
        bool result = Physics.Raycast(_orgin.position, _orgin.forward, out hit, _maxDistance, _targetLayerMask);
        return result;
    }
    public bool TryGetTarget()
    {
        bool result = Physics.Raycast(_orgin.position, _orgin.forward, _maxDistance, _targetLayerMask);
        return result;
    }
}
