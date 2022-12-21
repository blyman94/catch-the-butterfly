using System.Collections;
using UnityEngine;
using Cinemachine;

public class CinemachineChangeLensOrthoSize : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vcam;
    [SerializeField] private float zoomTime = 1.0f;

    private IEnumerator _activeRoutine;

    public void ChangeLensOrthographicSize(float newSize)
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
        float startSize = _vcam.m_Lens.OrthographicSize;
        float elapsedTime = 0.0f;

        while (elapsedTime < zoomTime)
        {
            _vcam.m_Lens.OrthographicSize = Mathf.Lerp(startSize, newSize, elapsedTime / zoomTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _vcam.m_Lens.OrthographicSize = newSize;
    }
}
