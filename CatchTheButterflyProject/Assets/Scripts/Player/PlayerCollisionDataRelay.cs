using UnityEngine;

/// <summary>
/// Allows designers to configure responses to OnCollisionEnter, 
/// OnCollisionStay, and OnCollisionExit with a specific tag all in the 
/// inspector.
/// </summary>
public class PlayerCollisionDataRelay : MonoBehaviour
{
    [SerializeField] private CollisionResponse[] collisionResponses;

    #region MonoBehaviour Methods
    private void OnCollisionEnter(Collision other)
    {
        foreach (CollisionResponse collisionResponse in collisionResponses)
        {
            if (other.transform.CompareTag(collisionResponse.Tag))
            {
                collisionResponse.OnCollisionEnterResponse?.Invoke();
            }
        }
    }
    private void OnCollisionStay(Collision other)
    {
        foreach (CollisionResponse collisionResponse in collisionResponses)
        {
            if (other.transform.CompareTag(collisionResponse.Tag))
            {
                collisionResponse.OnCollisionStayResponse?.Invoke();
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        foreach (CollisionResponse collisionResponse in collisionResponses)
        {
            if (other.transform.CompareTag(collisionResponse.Tag))
            {
                collisionResponse.OnCollisionExitResponse?.Invoke();
            }
        }
    }
    #endregion
}
