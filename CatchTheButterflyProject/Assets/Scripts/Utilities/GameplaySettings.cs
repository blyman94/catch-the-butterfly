using UnityEngine;

/// <summary>
/// A collection of all gameplay settings for easy tweaking by designers.
/// </summary>
[CreateAssetMenu]
public class GameplaySettings : ScriptableObject
{
    /// <summary>
    /// How much should the camera shake when a player collides with a rock?
    /// </summary>
    [Tooltip("How much should the camera shake when a player collides with " + 
        "a rock?")]
    public float CameraShakeIntensity = 2.0f;
    
    /// <summary>
    /// What should the constant move speed of the player be?
    /// </summary>
    [Tooltip("What should the constant move speed of the player be?")]
    public float ConstantMoveSpeed = 1.0f;

    /// <summary>
    /// How much faster than the river can the player swim?
    /// </summary>
    [Tooltip("How much faster than the river can the player swim?")]
    public float DownstreamMaxSpeedIncrease = 1.0f;

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
    /// How many times should the player's graphics blink? Note: Duration of the 
    /// blink is driven by the DrownEffectFadeTime.
    /// </summary>
    [Tooltip("How many times should the player's graphics blink? Note: " + 
        "Duration of the blink effect is driven by the DrownEffectFadeTime.")]
    public int GraphicsBlinkCount = 10;

    /// <summary>
    /// Instantaneous force applied to the Player object during a jump.
    /// </summary>
    [Tooltip("Instantaneous force applied to the Player object during a jump.")]
    public float JumpForce = 10.0f;

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
    /// What is the maximum speed at which the player can swim upstream?
    /// </summary>
    [Tooltip("What is the maximum speed at which the player can swim upstream?")]
    public float UpstreamMaxSpeed = 0.5f;

    /// <summary>
    /// Should the camera shake when the drown sequence starts?
    /// </summary>
    [Tooltip("Should the camera shake when the drown sequence starts?")]
    public bool UseCameraShake = false;

    /// <summary>
    /// Should a the colors fade to black and white when the drown sequence 
    /// starts?
    /// </summary>
    [Tooltip("Should a the colors fade to black and white when the drown " +
        "sequence starts?")]
    public bool UseColorDesaturation = true;
    
    /// <summary>
    /// Should a constant player speed be used?
    /// </summary>
    [Tooltip("Should a constant player speed be used?")]
    public bool UseConstantMoveSpeed = true;
        
    /// <summary>
    /// Should the player's graphics blink when they hit a rock?
    /// </summary>
    [Tooltip("Should the player's graphics blink when they hit a rock?")]
    public bool UseGraphicsBlink = true;

    /// <summary>
    /// Should a vignette appear when the drown sequence starts?
    /// </summary>
    [Tooltip("Should a vignette appear when the drown sequence starts?")]
    public bool UseVignette = true;

    /// <summary>
    /// How much of the screen should be covered by the drown event vignette
    /// effect?
    /// </summary>
    [Tooltip("How much of the screen should be covered by the drown " +
        "event vignette effect?")]
    [Range(0.0f, 1.0f)]
    public float VignetteIntensity = 0.6f;
}
