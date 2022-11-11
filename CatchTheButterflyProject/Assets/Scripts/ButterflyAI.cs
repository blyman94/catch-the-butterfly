using UnityEngine;
using UnityEngine.AI;

public class ButterflyAI : MonoBehaviour
{
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Vector2 _boundaryRange;
    [SerializeField] private float _selectNewOffsetTime = 2.0f;
    [SerializeField] private float _minHeight = 0.5f;
    [SerializeField] private float _maxHeight = 2.0f;
    [SerializeField] private float _zTargetOffset = 5.0f;

    private float _xOffset;
    private float _newOffsetTimer;
    private float _lastBaseOffset;
    private float _newBaseOffset;

    #region MonoBehaviour Methods
    private void Start()
    {
        _newOffsetTimer = _selectNewOffsetTime;

        _navMeshAgent.baseOffset = _minHeight;
        _lastBaseOffset = _navMeshAgent.baseOffset;
        _newBaseOffset = _navMeshAgent.baseOffset;
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(new Vector3(_playerPosition.Value.x + _xOffset,
            _playerPosition.Value.y, _playerPosition.Value.z + _zTargetOffset));

        _newOffsetTimer -= Time.deltaTime;
        _navMeshAgent.baseOffset = 
            Mathf.Lerp(_lastBaseOffset, _newBaseOffset, 
            1- (_newOffsetTimer / _selectNewOffsetTime));

        if (_newOffsetTimer <= 0.0f)
        {
            SelectRandomXOffset();
            SelectRandomBaseOffset();
            _newOffsetTimer = _selectNewOffsetTime;
        }
    }
    #endregion

    /// <summary>
    /// Selects a random x offset to the player's position for the butterfly to 
    /// travel to.
    /// </summary>
    private void SelectRandomXOffset()
    {
        float xOffsetMin = -(Mathf.Abs(_playerPosition.Value.x - _boundaryRange.x));
        float xOffsetMax = (Mathf.Abs(_playerPosition.Value.x - _boundaryRange.y));

        _xOffset = Random.Range(xOffsetMin, xOffsetMax);
    }

    /// <summary>
    /// Selects a random x offset to the player's position for the butterfly to 
    /// travel to.
    /// </summary>
    private void SelectRandomBaseOffset()
    {
        _lastBaseOffset = _newBaseOffset;
        _newBaseOffset = Random.Range(_minHeight, _maxHeight);
    }
}
