using UnityEngine;
using UnityEngine.Events;

public class AudioFader : MonoBehaviour
{
    [SerializeField] private AudioSource _sourceToFade;

    private float _fadeTime;
    private float _elapsedTime;
    private bool _fadingIn;
    private bool _fadingOut;
    private bool _shouldPause;
    private float _startVolume;

    #region MonoBehaviour Methods
    private void Start()
    {
        _startVolume = _sourceToFade.volume;
    }
    private void Update()
    {
        if (_fadingIn)
        {
            _sourceToFade.volume = Mathf.Lerp(0.0f, _startVolume,
                _elapsedTime / _fadeTime);
            _elapsedTime += Time.deltaTime;
            if (_sourceToFade.volume >= _startVolume)
            {
                _fadingIn = false;
            }
        }
        else if (_fadingOut)
        {
            _sourceToFade.volume = Mathf.Lerp(_startVolume, 0.0f,
                _elapsedTime / _fadeTime);
            _elapsedTime += Time.deltaTime;
            if (_sourceToFade.volume <= 0.0f)
            {
                _fadingOut = false;
                _sourceToFade.volume = 0.0f;
                if (_shouldPause)
                {
                    _sourceToFade.Pause();
                }
            }
        }
    }

    public void FadeIn(float fadeTime, bool unPauseFirst)
    {
        _fadingOut = false;
        _fadingIn = true;

        _sourceToFade.volume = 0.0f;
        _fadeTime = fadeTime;
        _elapsedTime = 0.0f;

        if(unPauseFirst)
        {
            _sourceToFade.UnPause();
        }
    }

    public void FadeOut(float fadeTime, bool shouldPause)
    {
        _fadingOut = true;
        _fadingIn = false;
        _shouldPause = shouldPause;

        _sourceToFade.volume = _startVolume;
        _fadeTime = fadeTime;
        _elapsedTime = 0.0f;
    }

    #endregion
}
