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
    [SerializeField] private AnimationCurve _difficultyCurve;

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
            
            ChangeDifficulty();
        }
    }

    private void ChangeDifficulty()
    {
        // Eval position will be between 0 and 1.
        float evalPos = _currentZPos / maxZValue;
        
        // Eval result will likewise be between 0 and 1.
        float evalResult = _difficultyCurve.Evaluate(evalPos);
        
        // Adjust rock spawning according to curve.
        _rockOffsetZIntervalRange = new Vector2(_rockOffsetZIntervalRange.x - (evalResult * _rockOffsetZIntervalRange.x),
            Mathf.Max(_rockOffsetZIntervalRange.y - (evalResult * _rockOffsetZIntervalRange.y), 2));
    }
}
