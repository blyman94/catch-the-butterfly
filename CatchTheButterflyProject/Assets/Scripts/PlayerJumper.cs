using UnityEngine;

/// <summary>
/// Allows the player to jump using Rigidbody impulse forces.
/// </summary>
public class PlayerJumper : MonoBehaviour
{
    /// <summary>
    /// Rigidbody of the Player object.
    /// </summary>
    [SerializeField] private Rigidbody _playerRb;

    /// <summary>
    /// Sensor3D determining if the Player object is grounded.
    /// </summary>
    [SerializeField] private Sensor3D groundSensor3D;

    /// <summary>
    /// Instantaneous force applied to the Player object during a jump.
    /// </summary>
    [SerializeField] private float _jumpForce;

    /// <summary>
    /// Number of total jumps the Player can execute without grounding.
    /// </summary>
    [SerializeField] private int numJumpsTotal = 1;

    /// <summary>
    /// How many jumps the Player has remaining before they must ground.
    /// </summary>
    private int numJumpsCurrent;

    #region MonoBehaviour Methods
    private void Start()
    {
        numJumpsCurrent = numJumpsTotal;
    }
    private void OnEnable()
    {
        groundSensor3D.SensorStateChanged += ResetJumpCount;
    }
    private void OnDisable()
    {
        groundSensor3D.SensorStateChanged -= ResetJumpCount;
    }
    #endregion

    /// <summary>
    /// Applies an instantaneous force to the player to simulate jumping.
    /// </summary>
    public void Jump()
    {
        if (groundSensor3D.Active || numJumpsCurrent > 0)
        {
            groundSensor3D.DisabledTimer = 0.1f;
            _playerRb.AddForce(new Vector3(0.0f, _jumpForce, 0.0f), 
                ForceMode.Impulse);
            numJumpsCurrent--;
        }
    }

    /// <summary>
    /// Resets the number of jumps the player is allowed to execute.
    /// </summary>
    public void ResetJumpCount()
    {
        if (groundSensor3D.Active)
        {
            numJumpsCurrent = numJumpsTotal;
        }
    }
}
