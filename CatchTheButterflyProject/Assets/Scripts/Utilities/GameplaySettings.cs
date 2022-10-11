using UnityEngine;

/// <summary>
/// A collection of all gameplay settings for easy tweaking by designers.
/// </summary>
[CreateAssetMenu]
public class GameplaySettings : ScriptableObject
{
    /// <summary>
    /// Force applied over time to move the Player along the x axis while 
    /// grounded.
    /// </summary>
    [Tooltip("Force applied over time to move the Player along the " + 
        "x axis while grounded.")]
    public float MoveForceGrounded = 50;

    /// <summary>
    /// Force applied over time to move the Player along the x axis while 
    /// airborne.
    /// </summary>
    [Tooltip("Force applied over time to move the Player along the " +
        "x axis while airborne.")]
    public float MoveForceAirborne = 50;

    /// <summary>
    /// Can the player adjust its movement in the air?
    /// </summary>
    [Tooltip("Can the player adjust its movement in the air?")]
    public bool CanMoveInAir = false;

    /// <summary>
    /// Instantaneous force applied to the Player object during a jump.
    /// </summary>
    [Tooltip("Instantaneous force applied to the Player object during a jump.")]
    public float JumpForce = 10;

    /// <summary>
    /// Amount by which gravity should be scaled. Final gravity will be equal
    /// to globalGravity * gravityScale.
    /// </summary>
    [Tooltip("Amount by which global gravity (default: 9.81f) should " +
        "be scaled.")]
    public float GravityScale = 0.25f;

    /// <summary>
    /// Can the player jump more than once?
    /// </summary>
    [Tooltip("Can the player jump more than once?")]
    public bool MultiJump = false;

    /// <summary>
    /// Number of total jumps the Player can execute without grounding.
    /// </summary>
    [Tooltip("Number of total jumps the Player can execute without grounding.")]
    public int TotalJumpCount = 1;

}
