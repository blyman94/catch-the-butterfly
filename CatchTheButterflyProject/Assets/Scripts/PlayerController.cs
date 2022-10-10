using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMover playerMover;

    /// <summary>
    /// Updates the MoveInput of the assigned PlayerMover to reflect the state
    /// of the input.
    /// </summary>
    /// <param name="context">Context representing the player's input.</param>
    public void OnMovementInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerMover.MoveInput = new Vector2(context.ReadValue<float>(),0);
        }
        else
        {
            playerMover.MoveInput = Vector2.zero;
        }
    }
}
