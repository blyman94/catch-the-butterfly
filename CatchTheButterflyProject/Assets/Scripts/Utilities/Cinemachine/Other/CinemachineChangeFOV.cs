using System.Collections;
using UnityEngine;
using Cinemachine;

/// <summary>
/// Smoothly changes the lens FOV to create a nice zoom effect.
/// </summary>
public class CinemachineChangeFOV : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vcam;
    [SerializeField] private float zoomTime = 1.0f;

    private IEnumerator _activeRoutine;

    public void ChangeLensFOV(float newSize)
    {
        if (_activeRoutine != null)
        {
            StopCoroutine(_activeRoutine);
        }
        _activeRoutine = ChangeLensOrthographicSizeRoutine(newSize);
        StartCoroutine(_activeRoutine);
    }

    private IEnumerator ChangeLensOrthographicSizeRoutine(float newSize)
    {
        float startSize = _vcam.m_Lens.FieldOfView;
        float elapsedTime = 0.0f;

        while (elapsedTime < zoomTime)
        {
            _vcam.m_Lens.FieldOfView = Mathf.Lerp(startSize, newSize, elapsedTime / zoomTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _vcam.m_Lens.FieldOfView = newSize;
    }
}
