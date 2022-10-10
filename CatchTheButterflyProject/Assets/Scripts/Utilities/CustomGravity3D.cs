using UnityEngine;

/// <summary>
/// Allows for custom gravity scale in Unity 3D.
/// </summary>
public class CustomGravity3D : MonoBehaviour
{
    /// <summary>
    /// Rigidbody that gravity will be customized for.
    /// </summary>
    [SerializeField] private Rigidbody _rb;

    /// <summary>
    /// Amount by which gravity should be scaled. Final gravity will be equal
    /// to globalGravity * gravityScale.
    /// </summary>
    public float gravityScale = 1.0f;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.
    public static float globalGravity = -9.81f;

    #region MonoBehaviour Methods
    void OnEnable()
    {
        _rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        _rb.AddForce(gravity, ForceMode.Acceleration);
    }
    #endregion
}
