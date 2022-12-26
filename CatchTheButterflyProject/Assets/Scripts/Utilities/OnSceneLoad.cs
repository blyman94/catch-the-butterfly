using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OnSceneLoad : MonoBehaviour
{
    [SerializeField] private UnityEvent _onSceneLoadResponse;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoadedResponse;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoadedResponse;
    }

    private void SceneLoadedResponse(Scene scene, LoadSceneMode mode)
    {
        _onSceneLoadResponse?.Invoke();
    }
}
