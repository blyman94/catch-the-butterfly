using UnityEngine;

/// <summary>
/// Represents how a section of the river carries objects floating in it.
/// </summary>
public class RiverSection : MonoBehaviour
{
    /// <summary>
    /// The direction in which the current will pull the player.
    /// </summary>
    [Header("River Section Data")]
    [Tooltip("The direction in which the current will pull the player.")]
    [SerializeField] private Vector2 _direction;

    /// <summary>
    /// Force at which objects carried will accelerate.
    /// </summary>
    [Tooltip("Force at which objects carried will accelerate.")]
    [SerializeField] private float _accelerationForce;

    /// <summary>
    /// Flow speed of carried objects.
    /// </summary>
    [Tooltip("Flow speed of carried objects.")]
    [SerializeField] private float _flowSpeed;
    
    [Header("Storage Variables")]
    [SerializeField] private FloatVariable _currentRiverSectionFlowSpeed;
    [SerializeField] private FloatVariable _currentRiverSectionAccelerationForce;
    [SerializeField] private Vector2Variable _currentRiverSectionDirection;

    #region MonoBehaviour Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _currentRiverSectionAccelerationForce.Value = _accelerationForce;
            _currentRiverSectionDirection.Value = _direction;
            _currentRiverSectionFlowSpeed.Value = _flowSpeed;
        }
    }
    #endregion
}
