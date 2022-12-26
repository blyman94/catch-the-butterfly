using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NewModuleSequence : MonoBehaviour
{
    [SerializeField] private NewModule[] _modules;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private ObstaclePool _obstaclePool;

    private void Start()
    {
        var testObject = _obstaclePool.Pool.Get();
        testObject.transform.position = _spawnTransform.position;
    }
}
