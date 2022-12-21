using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsBlinkSequence2D : MonoBehaviour
{
    [SerializeField] private GameplaySettings _gameplaySettings;
    [SerializeField] private SpriteRenderer[] _spriteRenderersToBlink;
    [SerializeField] private GameEvent _graphicsBlinkEndEvent;
    [SerializeField] private GameEvent _graphicsBlinkStartEvent;

    private float _effectTimer;
    private float _blinkTimer;
    private float _timeBetweenBlinks;
    private List<Sprite> _spritesToBlink;
    private bool _spriteShown = true;

    #region MonoBehaviour Methods

    private void Start()
    {
        _spritesToBlink = new List<Sprite>();
        foreach (SpriteRenderer renderer in _spriteRenderersToBlink)
        {
            _spritesToBlink.Add(renderer.sprite);
        }
    }
    private void Update()
    {
        if (_effectTimer > 0.0f)
        {
            if (_blinkTimer < 0.0f)
            {
                for(int i = 0; i < _spriteRenderersToBlink.Length; i++)
                {
                    if (_spriteShown)
                    {
                        _spriteShown = false;
                        _spriteRenderersToBlink[i].sprite = null;
                    }
                    else
                    {
                        _spriteShown = true;
                        _spriteRenderersToBlink[i].sprite = _spritesToBlink[i];
                    }
                }
                _blinkTimer = _timeBetweenBlinks;
            }
            _blinkTimer -= Time.deltaTime;
            _effectTimer -= Time.deltaTime;
            if (_effectTimer <= 0.0f)
            {
                for(int i = 0; i < _spriteRenderersToBlink.Length; i++)
                {
                    _spriteShown = true;
                    _spriteRenderersToBlink[i].sprite = _spritesToBlink[i];
                }
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
            
            for(int i = 0; i < _spriteRenderersToBlink.Length; i++)
            {
                _spriteShown = false;
                _spriteRenderersToBlink[i].sprite = null;
            }
        }
    }
}
