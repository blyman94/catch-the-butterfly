using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Represnts a slider that controls the volume of the game.
/// </summary>
public class VolumeSlider : MonoBehaviour
{
    /// <summary>
    /// Settings from which volume default setting values are read.
    /// </summary>
    [Tooltip("Settings from which volume default setting values are read.")]
    [SerializeField] private GameplaySettings _gameplaySettings;
    
    [Tooltip("Audio Mixer to control volume of.")]
    [SerializeField] private AudioMixer _mainMixer;

    [Tooltip("Key representing both the PlayerPrefs key that will be used" +
        "to save the volume, and the exposed parameter of the mixer that" +
        "will be controlled by the slider.")]
    [SerializeField] private string _keyForVolumeSave;

    [Tooltip("Unity UI Slider used to control the volume.")]
    [SerializeField] private Slider _slider;

    private void Start()
    {
        LoadVolumeSettings();
        ResetVolume();
    }
    
    /// <summary>
    /// Loads the current volume setting for this slider from PlayerPrefs, and
    /// adjusts the volume accordingly.
    /// </summary>
    private void LoadVolumeSettings()
    {
        switch (_keyForVolumeSave)
        {
            case "MusicVolume":
                _slider.value = PlayerPrefs.GetFloat(_keyForVolumeSave, _gameplaySettings.DefaultMusicVolume);
                break;
            case "SFXVolume":
                _slider.value = PlayerPrefs.GetFloat(_keyForVolumeSave, _gameplaySettings.DefaultSFXVolume);
                break;
            case "VoiceVolume":
                _slider.value = PlayerPrefs.GetFloat(_keyForVolumeSave, _gameplaySettings.DefaultVoiceVolume);
                break;
            default:
                break;
        }
        _mainMixer.SetFloat(_keyForVolumeSave, 
            Mathf.Log(_slider.value) * 20);
    }

    /// <summary>
    /// Sets the volume of the main mixer to the new volume percentage,
    /// translated to the mixer's native logarithmic scale. Then, saves the 
    /// volume using PlayerPrefs to preserve volume changes between play
    /// sessions.
    /// </summary>
    /// <param name="newVolumePercent"></param>
    public void OnVolumeChange(float newVolumePercent)
    {
        _mainMixer.SetFloat(_keyForVolumeSave, 
            Mathf.Log(newVolumePercent) * 20);
        PlayerPrefs.SetFloat(_keyForVolumeSave, newVolumePercent);
    }

    /// <summary>
    /// Reset the value of this slider to its default.
    /// </summary>
    public void ResetVolume()
    {
        float newVolumePercent = 0.0f;
        switch (_keyForVolumeSave)
        {
            case "MusicVolume":
                newVolumePercent = _gameplaySettings.DefaultMusicVolume;
                break;
            case "SFXVolume":
                newVolumePercent = _gameplaySettings.DefaultSFXVolume;
                break;
            case "VoiceVolume":
                newVolumePercent = _gameplaySettings.DefaultVoiceVolume;
                break;
            default:
                break;
        }
        _mainMixer.SetFloat(_keyForVolumeSave, 
            Mathf.Log(newVolumePercent) * 20);
        _slider.value = newVolumePercent;
        PlayerPrefs.SetFloat(_keyForVolumeSave, newVolumePercent);
    }
}