using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached transform will always face the camera at the specified offset.
/// </summary>
public class AlwaysFaceCamera : MonoBehaviour
{
    /// <summary>
    /// Transform will always face the camera at this offset.
    /// </summary>
    [SerializeField] private Vector3 _eulerAngleOffset = new Vector3(0.0f,90.0f,0.0f);

    /// <summary>
    /// Transform of the main camera.
    /// </summary>
    private Transform _mainCameraTransform;

    #region MonoBehaviour Methods
    private void Start()
    {
        _mainCameraTransform = Camera.main.transform;
    }
    private void Update()
    {
        Vector3 relativePosition = _mainCameraTransform.transform.position - transform.position; 
        transform.rotation = Quaternion.LookRotation(relativePosition) * 
            Quaternion.Euler(_eulerAngleOffset);
    }
    #endregion
}
