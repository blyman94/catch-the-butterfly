using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private Transform _startPoisitionTransform;
    [SerializeField] private GameObject[] _rockPrefabs;
    [SerializeField] private float maxZValue = 610.0f;
    [SerializeField] private Vector2 _rockOffsetZIntervalRange = new Vector2(1.0f, 5.0f);
    [SerializeField] private Vector2 _rockOffsetXRange = new Vector2(-2.5f, 1.7f);
    [SerializeField] private Vector2 _rockOffsetYRange = new Vector2(-0.22f, -0.22f);

    private float _currentZPos;
    
    #region MonoBehaviour Methods

    private void Start()
    {
        _currentZPos = _startPoisitionTransform.position.z;
        SpawnRocks();
    }
    #endregion

    private void SpawnRocks()
    {
        while (_currentZPos <= maxZValue)
        {
            // Calculate random position
            float randomXOffset = Random.Range(_rockOffsetXRange.x, _rockOffsetXRange.y);
            float randomYOffset = Random.Range(_rockOffsetYRange.x, _rockOffsetYRange.y);
            Vector3 newPos = new Vector3(randomXOffset, randomYOffset, _currentZPos);
            
            // Instantiate Rock
            int randomRockIndex = Random.Range(0, _rockPrefabs.Length);
            GameObject rockObject = Instantiate(_rockPrefabs[randomRockIndex],
                newPos,Quaternion.identity,transform);
            
            // Increment Z
            _currentZPos += Random.Range(_rockOffsetZIntervalRange.x, _rockOffsetZIntervalRange.y);
        }
    }
}
