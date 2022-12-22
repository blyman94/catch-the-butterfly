using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper class for player functionality.
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField] private GameplaySettings GameplaySettings;

    [Header("Data")]
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private BoolVariable _isDrown;

    [Header("Movement Components")]
    public CustomGravity3D CustomGravity;
    public PlayerJumper Jumper;
    public PlayerMover Mover;

    [Header("Physics Components")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Sensor3D _groundSensor;

    #region MonoBehaviour Methods
    private void Awake()
    {
        // Assign GameplaySettings
        CustomGravity.GameplaySettings = GameplaySettings;
        Jumper.GameplaySettings = GameplaySettings;
        Mover.GameplaySettings = GameplaySettings;

        // Assign Rb
        CustomGravity.Rb = _rb;
        Jumper.Rb = _rb;
        Mover.Rb = _rb;

        // Assign GroundSensor
        CustomGravity.GroundSensor = _groundSensor;
        Jumper.GroundSensor = _groundSensor;
        Mover.GroundSensor = _groundSensor;
    }
    private void Start()
    {
        _isDrown.Value = false;
    }
    private void Update()
    {
        _playerPosition.Value = transform.position;
    }
    #endregion
}
