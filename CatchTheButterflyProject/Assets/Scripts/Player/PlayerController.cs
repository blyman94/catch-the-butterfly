using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Holds a reference to the component pieces of the Player object, allowing the 
/// New Unity Input System to access and control the Player object.
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// The Player's PlayerMover component.
    /// </summary>
    [SerializeField] private PlayerMover _playerMover;

    /// <summary>
    /// The Player's PlayerJumper component.
    /// </summary>
    [SerializeField] private PlayerJumper _playerJumper;

    /// <summary>
    /// Executes a player jump when the input is pressed.
    /// </summary>
    /// <param name="context">Context representing the player's input.</param>
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _playerJumper.Jump();
        }
    }

    /// <summary>
    /// Updates the MoveInput of the assigned PlayerMover to reflect the state
    /// of the input.
    /// </summary>
    /// <param name="context">Context representing the player's input.</param>
    public void OnMovementInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerMover.MoveInput = context.ReadValue<Vector2>();
        }
        else
        {
            _playerMover.MoveInput = Vector2.zero;
        }
    }
}
