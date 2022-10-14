using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollisionResponse
{
    /// <summary>
    /// Which tag should this collision response detect?
    /// </summary>
    [Tooltip("Which tag should this collision response detect?")]
    public string Tag;
    public UnityEvent OnCollisionEnterResponse;
    public UnityEvent OnCollisionStayResponse;
    public UnityEvent OnCollisionExitResponse;
}
