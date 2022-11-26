using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private Vector2 _alphaRange = new Vector2(0.0f, 1.0f);
    [SerializeField] private bool _shownBlocksRaycast = true;
    [SerializeField] private bool _shownInteractable = true;
    
    private float _fadeTimer;
    private float _fadeTime;
    private bool _newTimer = false;
    private float _startAlpha;

    public float FadeTimer
    {
        get
        {
            return _fadeTimer;
        }
        set
        {
            _fadeTime = value;
            _fadeTimer = value;
            _newTimer = true;
        }
    }
    public bool FadeIn { get; set; } = false;
    public bool FadeOut { get; set; } = true;
    
    #region MonoBehaviour Methods
    private void Update()
    {
        if (_fadeTimer > 0.0f)
        {
            if (_newTimer)
            {
                _startAlpha = _group.alpha;
                _newTimer = false;
            }
            if (FadeIn)
            {
                FadeCanvasGroupIn();
            }
            else if (FadeOut)
            {
                FadeCanvasGroupOut();
            }
        }
    }
    #endregion

    private void FadeCanvasGroupOut()
    {
        _fadeTimer -= Time.deltaTime;
        _group.alpha = Mathf.Lerp(_startAlpha, _alphaRange.x, 1 - (_fadeTimer / _fadeTime));
        if (_fadeTimer <= 0.0f)
        {
            _group.alpha = _alphaRange.x;
            _group.blocksRaycasts = false;
            _group.interactable = false;
        }
    }
    
    private void FadeCanvasGroupIn()
    {
        _group.alpha = Mathf.Lerp(_startAlpha, _alphaRange.y, 1 - (_fadeTimer / _fadeTime));
        _fadeTimer -= Time.deltaTime;
        if (_fadeTimer <= 0.0f)
        {
            _group.alpha = _alphaRange.y;
            _group.blocksRaycasts = _shownBlocksRaycast;
            _group.interactable = _shownInteractable;
        }
    }
}
