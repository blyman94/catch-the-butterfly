using UnityEngine;

/// <summary>
/// Allows for custom gravity scale in Unity 3D.
/// </summary>
public class CustomGravity3D : MonoBehaviour
{
    /// <summary>
    /// Settings from which gameplay setting values are read.
    /// </summary>
    public GameplaySettings GameplaySettings { get; set; }

    /// <summary>
    /// Rigidbody that gravity will be customized for.
    /// </summary>
    public Rigidbody Rb { get; set; }

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.
    public static float globalGravity = -9.81f;

    #region MonoBehaviour Methods
    void OnEnable()
    {
        Rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = 
            globalGravity * GameplaySettings.GravityScale * Vector3.up;
        Rb.AddForce(gravity, ForceMode.Acceleration);
    }
    #endregion
}
