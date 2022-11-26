using UnityEngine;

/// <summary>
/// Moves the attached GameObject in the negative Z direction at the specified
/// scroll speed.
/// </summary>
public class Scroller : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FloatVariable _scrollSpeed;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        _scrollSpeed.VariableUpdated += SetSpeed;
    }

    private void Start()
    {
        SetSpeed();
    }
    private void OnDisable()
    {
        _scrollSpeed.VariableUpdated -= SetSpeed;
    }
    #endregion
    
    /// <summary>
    /// Sets the velocity of the rigidbody to make the section scroll.
    /// </summary>
    private void SetSpeed()
    {
        _rigidbody.velocity = new Vector3(0.0f, 0.0f, -_scrollSpeed.Value);
    }
}
