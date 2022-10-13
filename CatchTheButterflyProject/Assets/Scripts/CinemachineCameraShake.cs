using UnityEngine;
using Cinemachine;

/// <summary>
/// Manipulates the Cinemachine basic multi-channel perlin noise component to 
/// create a camera shake effect.
/// </summary>
public class CinemachineCameraShake : MonoBehaviour
{
    [SerializeField] private GameplaySettings gameplaySettings;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    /// <summary>
    /// Tracks how long the camera has been shaking.
    /// </summary>
    private float timer = 0.0f;

    #region MonoBehavior Methods
    private void Awake()
    {
        cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (timer > 0.0f)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(gameplaySettings.CameraShakeIntensity, 0.0f, 
                1 - (timer / gameplaySettings.CameraShakeDuration));
            timer -= Time.deltaTime;
        }
    }
    #endregion

    /// <summary>
    /// Begins shaking the camera at the intensity stored in the intensity 
    /// float variable by setting the shake timer.
    /// </summary>
    public void StartCameraShake()
    {
        timer = gameplaySettings.CameraShakeDuration;
    }

    /// <summary>
    /// Stops shaking the camera.
    /// </summary>
    public void StopCameraShake()
    {
        timer = 0.0f;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
    }
}
