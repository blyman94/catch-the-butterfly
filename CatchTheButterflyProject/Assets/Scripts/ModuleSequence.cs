using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModuleSequence : MonoBehaviour
{
    [SerializeField] private AudioSource _voiceoverAudioSource;
    [SerializeField] private Module[] _modules;
    [SerializeField] private GameObject _gameEndSectionPrefab;
    [SerializeField] private TextMeshProUGUI _chapterNameText;
    [SerializeField] private GameEvent _moduleStartEvent;

    private int _moduleIndex = 0;
    
    #region MonoBehaviour Methods
    private void Start()
    {
        _voiceoverAudioSource.clip = _modules[0].VoiceoverClip;
        _voiceoverAudioSource.Play();
        _chapterNameText.text = _modules[0].ModuleName;
        _moduleStartEvent.Raise();
    }
    #endregion

    public void SpawnRiverSectionResponse()
    {
        if (_voiceoverAudioSource.time >= _voiceoverAudioSource.clip.length ||
            _voiceoverAudioSource.time == 0.0f)
        {
            MoveToNextModule();
        }
        else
        {
            SpawnModuleChallengeSection();
        }
    }

    public void MoveToNextModule()
    {
        if ((_moduleIndex + 1) < _modules.Length)
        {
            _moduleIndex++;
            _voiceoverAudioSource.clip = _modules[_moduleIndex].VoiceoverClip;
            _voiceoverAudioSource.Play();
            SpawnModuleChallengeSection();
            _chapterNameText.text = _modules[_moduleIndex].ModuleName;
            _moduleStartEvent.Raise();
        }
        else
        {
            SpawnEndGameSection();
        }
    }

    private void SpawnModuleChallengeSection()
    {
        GameObject sectionToSpawn =
            _modules[_moduleIndex].GetRandomRiverSectionPrefab();
        Instantiate(sectionToSpawn, new Vector3(0, 0, 24), Quaternion.identity);
    }

    private void SpawnEndGameSection()
    {
        Instantiate(_gameEndSectionPrefab, new Vector3(0, 0, 24), Quaternion.identity);
    }
    
}
