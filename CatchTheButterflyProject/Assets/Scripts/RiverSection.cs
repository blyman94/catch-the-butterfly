using UnityEngine;

/// <summary>
/// Represents how a section of the river carries objects floating in it.
/// </summary>
public class RiverSection : MonoBehaviour
{
    /// <summary>
    /// The direction in which the current will pull the player.
    /// </summary>
    [Tooltip("The direction in which the current will pull the player.")]
    [SerializeField] private Vector2 direction;

    /// <summary>
    /// Force at which objects carried will excellerate.
    /// </summary>
    [Tooltip("Force at which objects carried will excellerate.")]
    [SerializeField] private float accelerationForce;

    /// <summary>
    /// Maximum speed of carried objects.
    /// </summary>
    [Tooltip("Maximum speed of carried objects.")]
    [SerializeField] private float maxSpeed;

    /// <summary>
    /// Rigidbody of the player character who has entered the zone.
    /// </summary>
    private Rigidbody activeRb;

    #region MonoBehaviour Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activeRb = other.GetComponent<Rigidbody>();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (activeRb != null)
        {
            float currentSpeed = Mathf.Abs(activeRb.velocity.z);

            Vector3 forceToApply = Vector3.zero;

            if (currentSpeed > maxSpeed)
            {
                float brakeSpeed = currentSpeed - maxSpeed;  // calculate the speed decrease
                forceToApply = new Vector3(0.0f, 0.0f, -brakeSpeed);
            }
            else
            {
                forceToApply = new Vector3(direction.x, 0.0f, direction.y) *
                    accelerationForce * Time.deltaTime;
            }

            activeRb.AddForce(forceToApply);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (activeRb != null)
        {
            activeRb = null;
        }
    }
    #endregion
}
