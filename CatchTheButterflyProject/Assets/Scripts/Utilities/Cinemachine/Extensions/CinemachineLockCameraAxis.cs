using UnityEngine;
using Cinemachine;

/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks camera movement
/// on the specified axes.
/// </summary>
[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class CinemachineLockCameraAxis : CinemachineExtension
{
    /// <summary>
    /// Lock the camera's X position to this value.
    /// </summary>
    [Tooltip("Lock the camera's X position to this value.")]
    [SerializeField] private bool _lockXPosition = false;

    /// <summary>
    /// Lock the camera's Y position to this value.
    /// </summary>
    [Tooltip("Lock the camera's Y position to this value.")]
    [SerializeField] private bool _lockYPosition = false;

    /// <summary>
    /// Lock the camera's Z position to this value.
    /// </summary>
    [Tooltip("Lock the camera's Z position to this value.")]
    [SerializeField] private bool _lockZPosition = false;

    /// <summary>
    /// Lock the camera's X position to this value.
    /// </summary>
    [Tooltip("Lock the camera's X position to this value.")]
    [SerializeField] private float _xPosition = 10;

    /// <summary>
    /// Lock the camera's Y position to this value.
    /// </summary>
    [Tooltip("Lock the camera's Y position to this value.")]
    [SerializeField] private float _yPosition = 10;

    /// <summary>
    /// Lock the camera's Z position to this value.
    /// </summary>
    [Tooltip("Lock the camera's Z position to this value.")]
    [SerializeField] private float _zPosition = 10;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 rawPos = state.RawPosition;
            if (_lockXPosition)
            {
                rawPos.x = _xPosition;
            }
            if (_lockYPosition)
            {
                rawPos.y = _yPosition;
            }
            if (_lockZPosition)
            {
                rawPos.z = _zPosition;
            }
            state.RawPosition = rawPos;
        }
    }
}
