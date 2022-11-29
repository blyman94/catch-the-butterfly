using UnityEngine;

/// <summary>
/// Moves the attached GameObject in the negative Z direction at the specified
/// scroll speed.
/// </summary>
public class Scroller : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FloatVariable _scrollSpeed;

    private bool _override = false;

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

    public void SetSpeedOverride(float newSpeed)
    {
        _override = true;
        _rigidbody.velocity = 
            new Vector3(_rigidbody.velocity.x, 0.0f, newSpeed);
    }
    
    /// <summary>
    /// Sets the velocity of the rigidbody to make the section scroll.
    /// </summary>
    private void SetSpeed()
    {
        if (!_override)
        {
            _rigidbody.velocity = 
                new Vector3(_rigidbody.velocity.x, 0.0f, -_scrollSpeed.Value);
        }
    }
}
