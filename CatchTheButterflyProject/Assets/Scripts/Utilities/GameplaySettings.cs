using UnityEngine;

/// <summary>
/// A collection of all gameplay settings for easy tweaking by designers.
/// </summary>
[CreateAssetMenu]
public class GameplaySettings : ScriptableObject
{
    /// <summary>
    /// How long should the camera shake when a player collides with a rock? The
    /// shake intensity will fade out over this time.
    /// </summary>
    [Tooltip("How long should the camera shake when a player collides with " +
        "a rock? The shake intensity will fade out over this time.")]
    public float CameraShakeDuration = 0.5f;

    /// <summary>
    /// How much should the camera shake when a player collides with a rock?
    /// </summary>
    [Tooltip("How much should the camera shake when a player collides with " + 
        "a rock?")]
    public float CameraShakeIntensity = 5.0f;

    /// <summary>
    /// Can the player adjust its movement in the air?
    /// </summary>
    [Tooltip("Can the player adjust its movement in the air?")]
    public bool CanMoveInAir = false;

    /// <summary>
    /// How long should it take the drown effect to fade in and out?
    /// </summary>
    [Tooltip("How long should it take the drown effect to fade in and out?")]
    public float DrownEffectFadeTime = 1.0f;

    /// <summary>
    /// Amount by which gravity should be scaled. Final gravity will be equal
    /// to globalGravity * gravityScale.
    /// </summary>
    [Tooltip("Amount by which global gravity (default: 9.81f) should " +
        "be scaled.")]
    public float GravityScale = 0.25f;

    /// <summary>
    /// Instantaneous force applied to the Player object during a jump.
    /// </summary>
    [Tooltip("Instantaneous force applied to the Player object during a jump.")]
    public float JumpForce = 10.0f;

    /// <summary>
    /// Force applied over time to move the Player along the x axis while 
    /// airborne.
    /// </summary>
    [Tooltip("Force applied over time to move the Player along the " +
        "x axis while airborne.")]
    public float MoveForceAirborne = 50.0f;

    /// <summary>
    /// Force applied over time to move the Player along the x axis while 
    /// grounded.
    /// </summary>
    [Tooltip("Force applied over time to move the Player along the " + 
        "x axis while grounded.")]
    public float MoveForceGrounded = 50.0f;

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

    /// <summary>
    /// How much of the screen should be covered by the drown event vignette
    /// effect?
    /// </summary>
    [Tooltip("How much of the screen should be covered by the drown " +
        "event vignette effect?")]
    [Range(0.0f, 1.0f)]
    public float VignetteIntensity = 0.6f;
}
