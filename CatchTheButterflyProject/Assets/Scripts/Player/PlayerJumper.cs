using UnityEngine;

/// <summary>
/// Allows the player to jump using Rigidbody impulse forces.
/// </summary>
public class PlayerJumper : MonoBehaviour
{
    /// <summary>
    /// Determines whether the player is currently in the drown state.
    /// </summary>
    [SerializeField] private BoolVariable _isDrown;

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
    /// How many jumps the Player has remaining before they must ground.
    /// </summary>
    private int numJumpsCurrent;

    #region MonoBehaviour Methods
    private void Start()
    {
        numJumpsCurrent = GameplaySettings.TotalJumpCount;
    }
    private void OnEnable()
    {
        GroundSensor.SensorStateChanged += ResetJumpCount;
    }
    private void OnDisable()
    {
        GroundSensor.SensorStateChanged -= ResetJumpCount;
    }
    #endregion

    /// <summary>
    /// Applies an instantaneous force to the player to simulate jumping.
    /// </summary>
    public void Jump()
    {
        if (GameplaySettings.OnlyJumpWhenDrowning)
        {
            if(!_isDrown.Value)
            {
                return;
            }
        }
        
        if (GroundSensor.Active || numJumpsCurrent > 0)
        {
            GroundSensor.DisabledTimer = 0.1f;
            Vector3 jumpForce =
                new Vector3(0.0f, GameplaySettings.JumpForce, 0.0f);
            Rb.AddForce(jumpForce, ForceMode.Impulse);
            numJumpsCurrent--;
        }
    }

    /// <summary>
    /// Resets the number of jumps the player is allowed to execute.
    /// </summary>
    public void ResetJumpCount()
    {
        if (GroundSensor.Active)
        {
            numJumpsCurrent = GameplaySettings.TotalJumpCount;
        }
    }
}
