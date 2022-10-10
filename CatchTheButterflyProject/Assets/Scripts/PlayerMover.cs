using UnityEngine;

/// <summary>
/// Responsible for moving the player along the x axis.
/// </summary>
public class PlayerMover : MonoBehaviour
{
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
    public Vector2 MoveInput { get; set; }

    /// <summary>
    /// Minimum magnitude of the MoveInput vector for the Player to be 
    /// considered moving.
    /// </summary>
    private const float moveThreshold = 0.01f;

    #region MonoBehaviour Methods
    private void Update()
    {
        if (Mathf.Abs(MoveInput.magnitude) >= moveThreshold)
        {
            if (GroundSensor.Active)
            {
                Rb.AddForce(new Vector3(-MoveInput.x, 0.0f, 0.0f) *
                    GameplaySettings.MoveForceGrounded * Time.deltaTime);
            }
            else
            {
                if (GameplaySettings.CanMoveInAir)
                {
                    Rb.AddForce(new Vector3(-MoveInput.x, 0.0f, 0.0f) *
                        GameplaySettings.MoveForceAirborne * Time.deltaTime);
                }
            }
        }
    }
    #endregion
}
