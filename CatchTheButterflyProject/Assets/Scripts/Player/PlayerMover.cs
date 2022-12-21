using UnityEngine;

/// <summary>
/// Responsible for moving the player along the x and z axes.
/// </summary>
public class PlayerMover : MonoBehaviour
{
    /// <summary>
    /// FloatVariable representing the flow speed of the river section the player is in.
    /// </summary>
    [Header("River Section Data Storage Variables")]
    [SerializeField] private FloatVariable _currentRiverSectionFlowSpeed;

    /// <summary>
    /// FloatVariable representing the acceleration force of the river section the player is in.
    /// </summary>
    [SerializeField] private FloatVariable _currentRiverSectionAccelerationForce;

    /// <summary>
    /// Vector2Variable representing the direction of the river section the player is in.
    /// </summary>
    [SerializeField] private Vector2Variable _currentRiverSectionDirection;

    [Header("Events")]
    [SerializeField] private GameEvent SwimUpstreamEvent;
    [SerializeField] private GameEvent SwimNeutralEvent;
    [SerializeField] private GameEvent SwimDownstreamEvent;

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
                SwimDownstreamEvent.Raise();
            }
            else if (value.x < 0.0f)
            {
                SwimUpstreamEvent.Raise();
            }
            else
            {
                SwimNeutralEvent.Raise();
            }
        }
    }

    private Vector2 _moveInput;

    #region MonoBehaviour Methods
    private void FixedUpdate()
    {
        if (GameplaySettings.UseConstantMoveSpeed)
        {
            if (Rb.velocity.z < GameplaySettings.ConstantMoveSpeed)
            {
                float newZVel = Mathf.Lerp(Rb.velocity.z, GameplaySettings.ConstantMoveSpeed, 0.1f);
                Rb.velocity = new Vector3(Rb.velocity.x, Rb.velocity.y, newZVel);
            }
        }
        else
        {
            float currentVelocity = Rb.velocity.z;
            Vector3 forceToApplyLeftRight = Vector3.zero;
            bool isSwimming = false;

            // Add base force from river
            Vector2 direction = _currentRiverSectionDirection.Value;
            forceToApplyLeftRight += new Vector3(direction.x, 0.0f, direction.y) *
                                     _currentRiverSectionAccelerationForce.Value * Time.fixedDeltaTime;

            // Add force from player movement (left-right)
            isSwimming = MoveInput.x != 0.0f;
            forceToApplyLeftRight += new Vector3(0.0f, 0.0f, MoveInput.x) *
                                     GameplaySettings.MoveForceGrounded * Time.fixedDeltaTime;

            // Determine max speed
            float maxDownstreamSpeed = isSwimming
                ? _currentRiverSectionFlowSpeed.Value +
                  GameplaySettings.DownstreamMaxSpeedIncrease
                : _currentRiverSectionFlowSpeed.Value;

            // Cap downstream speed
            if (currentVelocity > maxDownstreamSpeed)
            {
                float brakeSpeedDownstream = currentVelocity - maxDownstreamSpeed; // calculate the speed decrease
                forceToApplyLeftRight += new Vector3(0.0f, 0.0f, -brakeSpeedDownstream);
            }
            // Cap upstream speed
            else if (currentVelocity < -GameplaySettings.UpstreamMaxSpeed)
            {
                float brakeSpeedUpstream =
                    -GameplaySettings.UpstreamMaxSpeed - currentVelocity; // calculate the speed increase
                forceToApplyLeftRight += new Vector3(0.0f, 0.0f, brakeSpeedUpstream);
            }

            // Apply final force
            Rb.AddForce(forceToApplyLeftRight);
        }
        
        Vector3 forceToApplyTopBottom = Vector3.zero;
        
        // Add force from player movement (top-bottom)
        forceToApplyTopBottom += new Vector3(-MoveInput.y, 0.0f, 0.0f) * 
                                 GameplaySettings.MoveForceGrounded * Time.fixedDeltaTime;
        Rb.AddForce(forceToApplyTopBottom);
    }
    #endregion
}
