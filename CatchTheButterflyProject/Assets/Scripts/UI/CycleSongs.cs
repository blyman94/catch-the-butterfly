using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CycleSongs : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _songsToCycle;
    [SerializeField] private AudioSource _musicAudioSource;

    private int _currentSongIndex = 0;
    
    #region MonoBehaviour Methods

    private void Start()
    {
        _musicAudioSource.clip = _songsToCycle[_currentSongIndex];
    }
    #endregion
    
    /// <summary>
    /// Changes the current song to the next one in the cycle.
    /// </summary>
    public void NextSong()
    {
        if (_currentSongIndex + 1 < _songsToCycle.Count)
        {
            _currentSongIndex++;
        }
        else
        {
            _currentSongIndex = 0;
        }

        _musicAudioSource.Stop();
        _musicAudioSource.clip = _songsToCycle[_currentSongIndex];
        _musicAudioSource.Play();
    }
}
