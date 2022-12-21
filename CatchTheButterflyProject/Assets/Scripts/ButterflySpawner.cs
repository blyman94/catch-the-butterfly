using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ButterflySpawner : MonoBehaviour
{
    [SerializeField] private BoolVariable _isDrown;
    [SerializeField] private GameObject _butterflyPrefab;
    [SerializeField] private GameObject _rockPrefab;
    [SerializeField] private Vector2 _butterflySpawnXPositionRange;
    [SerializeField] private float _distanceBuffer = 7.7f;
    
    public bool ButterflyActive { get; set; }
    
    #region MonoBehaviour Methods

    private void OnEnable()
    {
        _isDrown.VariableUpdated += SpawnButterfly;
    }
    
    private void OnDisable()
    {
        _isDrown.VariableUpdated -= SpawnButterfly;
    }

    #endregion

    private void SpawnButterfly()
    {
        if (_isDrown.Value && !ButterflyActive)
        {
            float xPos = Random.Range(_butterflySpawnXPositionRange.x,
                _butterflySpawnXPositionRange.y);
            Vector3 spawnPosRock = new Vector3(xPos, 0.0f, _distanceBuffer);
            Vector3 spawnPosButterfly = new Vector3(xPos, -0.25f, _distanceBuffer);
            Instantiate(_rockPrefab, spawnPosRock, Quaternion.identity);
            Instantiate(_butterflyPrefab, spawnPosButterfly, Quaternion.identity);
            ButterflyActive = true;
        }
    }

}
