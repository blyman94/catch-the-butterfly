using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for moving the player along the x axis.
/// </summary>
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private float moveForce;

    public Vector2 MoveInput { get; set; }

    private const float moveThreshold = 0.01f;

    #region MonoBehaviour Methods
    private void Update()
    {
        if (Mathf.Abs(MoveInput.magnitude) >= moveThreshold)
        {
            playerRb.AddForce(new Vector3(-MoveInput.x, 0.0f, 0.0f) * moveForce * Time.deltaTime);
        }
    }
    #endregion
}
