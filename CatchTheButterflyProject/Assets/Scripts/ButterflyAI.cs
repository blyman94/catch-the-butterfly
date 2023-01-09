using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ButterflyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Animator _animator;
    [SerializeField] private Scroller _scroller;

    [Header("Positioning")]
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private float _startFlyingDistance = 2.0f;
    [SerializeField] private float _speedX;

    [Header("Random Ranges")]
    [SerializeField] private Vector2 _boundaryRange;
    [SerializeField] private Vector2 _baseOffsetRange;
    [SerializeField] private Vector2 _speedRange;

    [Header("Random Position Intervals")]
    [SerializeField] private float _selectNewXOffsetTime = 2.0f;
    [SerializeField] private float _selectNewBaseOffsetTime = 2.0f;
    [SerializeField] private float _selectNewSpeedTime = 2.0f;

    [Header("River Speeds")]
    [SerializeField] private FloatVariable _currentRiverSpeed;

    private bool _isWaiting = true;
    private int _flyTriggerHash;
    private Vector2 _relativeSpeedRange;

    private float _xOffset;
    private float _newBaseOffsetTimer;
    private float _lastBaseOffset;
    private float _newBaseOffset;
    private float _relativeZSpeed;

    #region MonoBehaviour Methods

    private void Awake()
    {
        _flyTriggerHash = Animator.StringToHash("FlyTrigger");
        UpdateRelativeSpeedRange();
    }

    private void OnEnable()
    {
        _currentRiverSpeed.VariableUpdated += UpdateRelativeSpeedRange;
    }
    private void Start()
    {
        _isWaiting = true;
    }
    private void OnDisable()
    {
        _currentRiverSpeed.VariableUpdated -= UpdateRelativeSpeedRange;
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
                    1 - (_newBaseOffsetTimer / _selectNewBaseOffsetTime));

            float newPlayerPosX = _playerPosition.Value.x + _xOffset;
            _navMeshAgent.SetDestination(new Vector3(newPlayerPosX,
                transform.position.y, transform.position.z));
        }
    }
    #endregion

    private void StartFlying()
    {
        _navMeshAgent.speed = _speedX;

        _newBaseOffsetTimer = _selectNewBaseOffsetTime;
        _lastBaseOffset = _navMeshAgent.baseOffset;
        _newBaseOffset = _lastBaseOffset;

        InvokeRepeating(nameof(SelectRandomXOffset), 0.0f,
            _selectNewXOffsetTime);
        InvokeRepeating(nameof(SelectRandomRelativeZSpeed), 0.0f,
            _selectNewSpeedTime);

        _animator.SetTrigger(_flyTriggerHash);
    }

    private void UpdateRelativeSpeedRange()
    {
        _relativeSpeedRange = new Vector2(_speedRange.x - _currentRiverSpeed.Value,
            _speedRange.y - _currentRiverSpeed.Value);
        SelectRandomRelativeZSpeed();
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
    private void SelectRandomRelativeZSpeed()
    {
        if (_isWaiting)
        {
            return;
        }
        _relativeZSpeed = Random.Range(_relativeSpeedRange.x,
            _relativeSpeedRange.y);
        _scroller.SetSpeedOverride(_relativeZSpeed);
    }

    /// <summary>
    /// Selects a random x offset to the player's position for the butterfly to 
    /// travel to.
    /// </summary>
    private void SelectRandomXOffset()
    {
        float xOffsetMin = -Mathf.Abs(_playerPosition.Value.x - _boundaryRange.x);
        float xOffsetMax = Mathf.Abs(_playerPosition.Value.x - _boundaryRange.y);

        _xOffset = Random.Range(xOffsetMin, xOffsetMax);
    }
}
