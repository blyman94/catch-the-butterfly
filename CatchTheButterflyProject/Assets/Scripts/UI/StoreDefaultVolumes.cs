using UnityEngine;
using UnityEngine.UI;

public class StoreDefaultVolumes : MonoBehaviour
{
    /// <summary>
    /// Settings to which defaults will be stored.
    /// </summary>
    [Tooltip("Settings to which defaults will be stored.")]
    [SerializeField] private GameplaySettings _gameplaySettings;
    
    /// <summary>
    /// Slider representing the default music volume.
    /// </summary>
    [Tooltip("Slider representing the default music volume.")]
    [SerializeField] private Slider _musicVolumeSlider;
    
    /// <summary>
    /// Slider representing the default sfx volume.
    /// </summary>
    [Tooltip("Slider representing the default sfx volume.")]
    [SerializeField] private Slider _sfxVolumeSlider;
    
    /// <summary>
    /// Slider representing the default voice volume.
    /// </summary>
    [Tooltip("Slider representing the default voice volume.")]
    [SerializeField] private Slider _voiceVolumeSlider;
    
    /// <summary>
    /// Stores the current value of each of the 3 sliders as default values.
    /// </summary>
    public void StoreValuesAsDefault()
    {
        _gameplaySettings.DefaultMusicVolume = _musicVolumeSlider.value;
        _gameplaySettings.DefaultSFXVolume = _sfxVolumeSlider.value;
        _gameplaySettings.DefaultVoiceVolume = _voiceVolumeSlider.value;
    }
}
