using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private BoolVariable _isDrown;
    [SerializeField] private GameObject _butterflyPrefab;
    [SerializeField] private List<Transform> _butterflySpawnPositions;
    [SerializeField] private float _distanceBuffer = 5.5f;
    
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
            Transform spawnTransform = _butterflySpawnPositions.Where(x =>
                x.position.z > _playerPosition.Value.z + 
                _distanceBuffer).OrderBy(x => x.position.z).ToList()[0];
            
            Instantiate(_butterflyPrefab, spawnTransform.position,
                Quaternion.identity);
            ButterflyActive = true;
        }
    }

}
