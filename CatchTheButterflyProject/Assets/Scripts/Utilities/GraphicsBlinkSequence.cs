using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Toggles the visibility of a MeshRenderer to create a flashing effect, a
/// trope used to communicate that damage has been taken.
/// </summary>
public class GraphicsBlinkSequence : MonoBehaviour
{
    [SerializeField] private GameplaySettings _gameplaySettings;
    [SerializeField] private MeshRenderer _graphicsRenderer;
    [SerializeField] private GameEvent _graphicsBlinkEndEvent;
    [SerializeField] private GameEvent _graphicsBlinkStartEvent;

    private float _effectTimer;
    private float _blinkTimer;
    private float _timeBetweenBlinks;

    #region MonoBehaviour Methods
    private void Update()
    {
        if (_effectTimer > 0.0f)
        {
            if (_blinkTimer < 0.0f)
            {
                _graphicsRenderer.enabled = !_graphicsRenderer.enabled;
                _blinkTimer = _timeBetweenBlinks;
            }
            _blinkTimer -= Time.deltaTime;
            _effectTimer -= Time.deltaTime;
            if (_effectTimer <= 0.0f)
            {
                _graphicsRenderer.enabled = true;
                _graphicsBlinkEndEvent?.Raise();
            }
        }
    }
    #endregion

    /// <summary>
    /// Sets the effect timer such that the blinking sequence begins. Calculates
    /// time between blinks based on blink duration and blink count.
    /// </summary>
    public void StartSequence()
    {
        if (_gameplaySettings.UseGraphicsBlink)
        {
            _graphicsBlinkStartEvent?.Raise();
            _effectTimer = _gameplaySettings.DrownEffectFadeTime;
            _timeBetweenBlinks =
                (_gameplaySettings.DrownEffectFadeTime /
                _gameplaySettings.GraphicsBlinkCount) * 0.5f;
            _blinkTimer = _timeBetweenBlinks;

            _graphicsRenderer.enabled = false;
        }
    }
}
