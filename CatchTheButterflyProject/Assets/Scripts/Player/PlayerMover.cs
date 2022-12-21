using UnityEngine;

// TODO: Lerp speed

/// <summary>
/// Responsible for moving the player along the x and z axes.
/// </summary>
public class PlayerMover : MonoBehaviour
{
    /// <summary>
    /// How much faster or slower than the river can the player move?
    /// </summary>
    [Header("Tech Debt")] 
    [Tooltip("How much faster or slower than the river can the player move?")]
    [SerializeField] private float _playerSpeedDelta = 0.5f;
    
    /// <summary>
    /// Event raised when the player attempts to swim downstream.
    /// </summary>
    [Header("Events")]
    [Tooltip("Event raised when the player attempts to swim downstream.")]
    [SerializeField] private GameEvent _swimDownstreamEvent;

    /// <summary>
    /// Event raised when the player stops swimming up or downstream.
    /// </summary>
    [Tooltip("Event raised when the player stops swimming up or downstream.")]
    [SerializeField] private GameEvent _swimNeutralEvent;
    
    /// <summary>
    /// Event raised when the player attempts to swim upstream.
    /// </summary>
    [Tooltip("Event raised when the player attempts to swim upstream.")]
    [SerializeField] private GameEvent _swimUpstreamEvent;
    
    /// <summary>
    /// Variable containing the current scroll speed of the river.
    /// </summary>
    [Header("River Scroll Speeds")]
    [Tooltip("Variable containing the base scroll speed of the river.")]
    [SerializeField] private FloatVariable _baseRiverSpeedVariable;
    
    /// <summary>
    /// Variable containing the current scroll speed of the river.
    /// </summary>
    [Tooltip("Variable containing the current scroll speed of the river.")]
    [SerializeField] private FloatVariable _currentRiverSpeedVariable;

    /// <summary>
    /// Settings from which gameplay setting values are read.
    /// </summary>
    public GameplaySettings GameplaySettings { get; set; }

    /// <summary>
    /// Rigidbody of the Player object.
    /// </summary>
    public Rigidbody Rb { get; set; }

    /// <summary>
    /// Sensor3D determining if the Player object is grounded.
    /// </summary>
    public Sensor3D GroundSensor { get; set; }

    private bool _isSwimmingUpstream = false;
    private bool _isSwimmingDownstream = false;

    /// <summary>
    /// Represents the current move input for the Player object.
    /// </summary>
    public Vector2 MoveInput
    {
        get
        {
            return _moveInput;
        }
        set
        {
            _moveInput = value;
            if (value.x > 0.0f)
            {
                if (!_isSwimmingDownstream)
                {
                    _isSwimmingDownstream = true;
                    _isSwimmingUpstream = false;
                    _swimDownstreamEvent.Raise();
                    _currentRiverSpeedVariable.Value += _playerSpeedDelta;
                }
            }
            else if (value.x < 0.0f)
            {
                if (!_isSwimmingUpstream)
                {
                    _isSwimmingUpstream = true;
                    _isSwimmingDownstream = false;
                    _swimUpstreamEvent.Raise();
                    _currentRiverSpeedVariable.Value -= _playerSpeedDelta;
                }
            }
            else
            {
                _isSwimmingUpstream = false;
                _isSwimmingDownstream = false;
                
                _currentRiverSpeedVariable.Value = _baseRiverSpeedVariable.Value;
                _swimNeutralEvent.Raise();
            }
        }
    }
    
    /// <summary>
    /// Represents the current move input for the Player object.
    /// </summary>
    private Vector2 _moveInput;

    private float _baseRiverSpeed;

    #region MonoBehaviour Methods
    private void FixedUpdate()
    {
        Vector3 forceToApplyTopBottom = Vector3.zero;
        
        // Add force from player movement (top-bottom)
        forceToApplyTopBottom += new Vector3(-MoveInput.y, 0.0f, 0.0f) * 
                                 (GameplaySettings.MoveForceGrounded * Time.fixedDeltaTime);
        Rb.AddForce(forceToApplyTopBottom);
    }
    #endregion
}
