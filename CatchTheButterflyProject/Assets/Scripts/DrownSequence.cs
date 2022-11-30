using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Manipulates the vignette and color adjustments post-processing effects to 
/// change the state of the game to the player drowning state.
/// </summary>
public class DrownSequence : MonoBehaviour
{
    [SerializeField] private GameplaySettings gameplaySettings;
    [SerializeField] private Volume _volume;
    [SerializeField] private AudioMixer _audioMixer;

    [Header("Tech Debt")] 
    [SerializeField] private float _sfxLowpassCutoffBase = 5000.0f;
    [SerializeField] private float _sfxLowpassCutoffDrown = 1200.0f;
    [SerializeField] private float _musicLowpassCutoffBase = 5000.0f;
    [SerializeField] private float _musicLowpassCutoffDrown = 1200.0f;
    
    private Vignette _vignette;
    private ColorAdjustments _colorAdjustments;
    private IEnumerator activeCoroutine;

    #region MonoBehaviour Methods
    private void Start()
    {
        _volume.profile.TryGet<Vignette>(out _vignette);
        _volume.profile.TryGet<ColorAdjustments>(out _colorAdjustments);
    }
    #endregion

    /// <summary>
    /// Adds a vignette and color desaturation post-processing effects to the 
    /// scene which visually represents the player entering the drowning state.
    /// </summary>
    public void StartDrownSequence()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }
        activeCoroutine = StartDrownSequenceRoutine();
        StartCoroutine(activeCoroutine);
    }

    /// <summary>
    /// Removes the vignette and color desaturation post-processing effects
    /// from the scene which visually represents the player return from the
    /// drowning state.
    /// </summary>
    public void EndDrownSequence()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }
        activeCoroutine = EndDrownSequenceRoutine();
        StartCoroutine(activeCoroutine);
    }

    /// <summary>
    /// Coroutine to achieve the fading behaviour of the drowning effect. Fades
    /// in a vignette and fades out the color saturation based on values from 
    /// the assigned GameplaySettings.
    /// </summary>
    /// <returns> IEnumerator to segment the execution of the 
    /// coroutine.</returns>
    private IEnumerator StartDrownSequenceRoutine()
    {
        float elapsedTime = 0.0f;
        
        float currentSFXLowpassCutoff;
        _audioMixer.GetFloat("SFXLowpassCutoff", out currentSFXLowpassCutoff);
        float currentMusicLowpassCutoff;
        _audioMixer.GetFloat("MusicLowpassCutoff", out currentMusicLowpassCutoff);
        
        float currentVignetteIntensity = _vignette.intensity.value;
        float currentColorAdjustmentsSaturation =
            _colorAdjustments.saturation.value;

        while (elapsedTime < gameplaySettings.DrownEffectFadeTime)
        {
            if (gameplaySettings.UseColorDesaturation)
            {
                _colorAdjustments.saturation.value =
                    Mathf.Lerp(currentColorAdjustmentsSaturation, -100.0f,
                    elapsedTime / gameplaySettings.DrownEffectFadeTime);
            }
            if (gameplaySettings.UseVignette)
            {
                _vignette.intensity.value = Mathf.Lerp(currentVignetteIntensity,
                gameplaySettings.VignetteIntensity,
                elapsedTime / gameplaySettings.DrownEffectFadeTime);
            }
            
            // Lowpass
            _audioMixer.SetFloat("SFXLowpassCutoff", Mathf.Lerp(
                currentSFXLowpassCutoff,
                _sfxLowpassCutoffDrown,
                elapsedTime / gameplaySettings.DrownEffectFadeTime));
            _audioMixer.SetFloat("MusicLowpassCutoff", Mathf.Lerp(
                currentMusicLowpassCutoff,
                _musicLowpassCutoffDrown,
                elapsedTime / gameplaySettings.DrownEffectFadeTime));
            
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _audioMixer.SetFloat("SFXLowpassCutoff", _sfxLowpassCutoffDrown);
        _audioMixer.SetFloat("MusicLowpassCutoff", _musicLowpassCutoffDrown);
        _vignette.intensity.value = gameplaySettings.VignetteIntensity;
        _colorAdjustments.saturation.value = -100.0f;
    }

    /// <summary>
    /// Coroutine to achieve the fading behaviour of the drowning effect. Fades
    /// in a vignette and fades out the color saturation based on values from 
    /// the assigned GameplaySettings.
    /// </summary>
    /// <returns> IEnumerator to segment the execution of the 
    /// coroutine.</returns>
    private IEnumerator EndDrownSequenceRoutine()
    {
        float elapsedTime = 0.0f;
        
        float currentSFXLowpassCutoff;
        _audioMixer.GetFloat("SFXLowpassCutoff", out currentSFXLowpassCutoff);
        float currentMusicLowpassCutoff;
        _audioMixer.GetFloat("MusicLowpassCutoff", out currentMusicLowpassCutoff);
        
        float currentVignetteIntensity = _vignette.intensity.value;
        float currentColorAdjustmentsSaturation =
            _colorAdjustments.saturation.value;

        while (elapsedTime < gameplaySettings.DrownEffectFadeTime)
        {
            _vignette.intensity.value = Mathf.Lerp(currentVignetteIntensity,
                0.0f, elapsedTime / gameplaySettings.DrownEffectFadeTime);
            _colorAdjustments.saturation.value =
                Mathf.Lerp(currentColorAdjustmentsSaturation, 0.0f,
                elapsedTime / gameplaySettings.DrownEffectFadeTime);
            
            // Lowpass
            _audioMixer.SetFloat("SFXLowpassCutoff", Mathf.Lerp(
                currentSFXLowpassCutoff,
                _sfxLowpassCutoffBase,
                elapsedTime / gameplaySettings.DrownEffectFadeTime));
            _audioMixer.SetFloat("MusicLowpassCutoff", Mathf.Lerp(
                currentMusicLowpassCutoff,
                _musicLowpassCutoffBase,
                elapsedTime / gameplaySettings.DrownEffectFadeTime));
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        _audioMixer.SetFloat("SFXLowpassCutoff", _sfxLowpassCutoffBase);
        _audioMixer.SetFloat("MusicLowpassCutoff", _musicLowpassCutoffBase);
        _vignette.intensity.value = 0.0f;
        _colorAdjustments.saturation.value = 0.0f;
    }
}
