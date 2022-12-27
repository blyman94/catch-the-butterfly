using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteSwapper : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private Sprite _playerGroundedSprite;
    [SerializeField] private Sprite _playerNotGroundedSprite;
    [SerializeField] private Sensor3D _groundSensor;

    private void OnEnable()
    {
        _groundSensor.SensorStateChanged += SwapPlayerGroundedSprite;
    }
    private void OnDisable()
    {
        _groundSensor.SensorStateChanged -= SwapPlayerGroundedSprite;
    }

    private void SwapPlayerGroundedSprite()
    {
        if (_groundSensor.Active)
        {
            _playerSpriteRenderer.sprite = _playerGroundedSprite;
        }
        else
        {
            _playerSpriteRenderer.sprite = _playerNotGroundedSprite;
        }
    }
}
