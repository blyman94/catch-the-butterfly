using UnityEngine;
using UnityEngine.AI;

public class ButterflyAI : MonoBehaviour
{
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _zTargetOffset = 5.0f;
    [SerializeField] private float _startFlyingDistance = 2.0f;
    
    [Header("Random Ranges")]
    [SerializeField] private Vector2 _boundaryRange;
    [SerializeField] private Vector2 _baseOffsetRange;
    [SerializeField] private Vector2 _speedRange;
    
    [Header("Random Position Intervals")]
    [SerializeField] private float _selectNewXOffsetTime = 2.0f;
    [SerializeField] private float _selectNewBaseOffsetTime = 2.0f;
    [SerializeField] private float _selectNewSpeedTime = 2.0f;

    private bool _isWaiting;
    private int _flyTriggerHash;

    private float _xOffset;
    private float _newBaseOffsetTimer;
    private float _newSpeedTimer;
    private float _lastBaseOffset;
    private float _newBaseOffset;

    #region MonoBehaviour Methods

    private void Awake()
    {
        _flyTriggerHash = Animator.StringToHash("FlyTrigger");
    }
    private void Start()
    {
        _isWaiting = true;
        _navMeshAgent.baseOffset = _baseOffsetRange.x;
    }

    private void Update()
    {
        if (_isWaiting)
        {
            if (Mathf.Abs(_playerPosition.Value.z - transform.position.z) <
                _startFlyingDistance)
            {
                StartFlying();
                _isWaiting = false;
            }
        }
        else
        {
            // Decrement Timers
            _newBaseOffsetTimer -= Time.deltaTime;
        
            // Calculate new base offset if necessary
            if (_newBaseOffsetTimer <= 0.0f)
            {
                SelectRandomBaseOffset();
                _newBaseOffsetTimer = _selectNewBaseOffsetTime;
            }
        
            // Adjust base offset
            _navMeshAgent.baseOffset = 
                Mathf.Lerp(_lastBaseOffset, _newBaseOffset, 
                    1- (_newBaseOffsetTimer / _selectNewBaseOffsetTime));
        
            float newPlayerPosX = _playerPosition.Value.x + _xOffset;
            float newPlayerPosZ = _playerPosition.Value.z + _zTargetOffset;
            _navMeshAgent.SetDestination(new Vector3(newPlayerPosX,
                _playerPosition.Value.y, newPlayerPosZ));
        }
    }
    #endregion

    private void StartFlying()
    {
        _navMeshAgent.speed = _speedRange.x;
        
        _newBaseOffsetTimer = _selectNewBaseOffsetTime;
        _lastBaseOffset = _navMeshAgent.baseOffset;
        _newBaseOffset = _lastBaseOffset;

        InvokeRepeating(nameof(SelectRandomXOffset), 0.0f, 
            _selectNewXOffsetTime);
        InvokeRepeating(nameof(SelectRandomSpeed), 0.0f, 
            _selectNewSpeedTime);
        
        _animator.SetTrigger(_flyTriggerHash);
    }
    
    /// <summary>
    /// Selects a random x offset to the player's position for the butterfly to 
    /// travel to.
    /// </summary>
    private void SelectRandomBaseOffset()
    {
        _lastBaseOffset = _newBaseOffset;
        _newBaseOffset = Random.Range(_baseOffsetRange.x, _baseOffsetRange.y);
    }
    
    /// <summary>
    /// Selects a random speed for the butterfly to travel at.
    /// </summary>
    private void SelectRandomSpeed()
    {
        _navMeshAgent.speed = Random.Range(_speedRange.x, _speedRange.y);
    }

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
}
