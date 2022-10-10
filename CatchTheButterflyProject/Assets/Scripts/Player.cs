using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper class for player functionality.
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private GameplaySettings GameplaySettings;
    
    [Header("Physics Components")]
    [SerializeField] private Rigidbody Rb;
    [SerializeField] private Sensor3D GroundSensor;

    [Header("Movement Components")]
    public CustomGravity3D CustomGravity;
    public PlayerJumper Jumper;
    public PlayerMover Mover;


    #region MonoBehaviour Methods
    private void Awake()
    {
        // Assign GameplaySettings
        CustomGravity.GameplaySettings = GameplaySettings;
        Jumper.GameplaySettings = GameplaySettings;
        Mover.GameplaySettings = GameplaySettings;

        // Assign Rb
        CustomGravity.Rb = Rb;
        Jumper.Rb = Rb;
        Mover.Rb = Rb;

        // Assign GroundSensor
        Jumper.GroundSensor = GroundSensor;
        Mover.GroundSensor = GroundSensor;
    }
    #endregion
}
