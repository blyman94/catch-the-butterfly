using UnityEngine;

/// <summary>
/// Uses a raycast to detect a surface below the caster. Then, moves a transform
/// representing the shadow to the top of that surface. This fake shadow helps
/// the player calculate where they should jump from when in isometric view.
/// </summary>
public class ShadowCaster : MonoBehaviour
{
    [SerializeField] private Transform _shadowTransform;
    [SerializeField] private LayerMask _shadowCollisionLayerMask;

    private void FixedUpdate()
    {
        RaycastHit info;
        Physics.Raycast(transform.position, Vector3.down, out info, Mathf.Infinity, _shadowCollisionLayerMask);
        _shadowTransform.localPosition = new Vector3(0.0f, -info.distance + 0.01f, 0.0f);
    }
}
