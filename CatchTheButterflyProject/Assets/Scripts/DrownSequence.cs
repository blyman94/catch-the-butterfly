using System.Collections;
using UnityEngine;
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
        float currentVignetteIntensity = _vignette.intensity.value;
        float currentColorAdjustmentsSaturation =
            _colorAdjustments.saturation.value;

        while (elapsedTime < gameplaySettings.DrownEffectFadeTime)
        {
            _vignette.intensity.value = Mathf.Lerp(currentVignetteIntensity,
                gameplaySettings.VignetteIntensity,
                elapsedTime / gameplaySettings.DrownEffectFadeTime);
            _colorAdjustments.saturation.value =
                Mathf.Lerp(currentColorAdjustmentsSaturation, -100.0f,
                elapsedTime / gameplaySettings.DrownEffectFadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

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
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _vignette.intensity.value = 0.0f;
        _colorAdjustments.saturation.value = 0.0f;
    }
}
