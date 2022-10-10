using UnityEngine;

/// <summary>
/// Responsible for moving the player along the x axis.
/// </summary>
public class PlayerMover : MonoBehaviour
{
    /// <summary>
    /// Rigidbody of the Player object.
    /// </summary>
    [SerializeField] private Rigidbody _playerRb;

    /// <summary>
    /// Force applied over frames to move the Player along the x axis.
    /// </summary>
    [SerializeField] private float _moveForce;

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
            _playerRb.AddForce(new Vector3(-MoveInput.x, 0.0f, 0.0f) * _moveForce * Time.deltaTime);
        }
    }
    #endregion
}
