using UnityEngine;
using TMPro;

public class ModuleSequence : MonoBehaviour
{
    [SerializeField] private GameplaySettings _gameplaySettings;
    [SerializeField] private AudioSource _voiceoverAudioSource;
    [SerializeField] private Module[] _modules;
    [SerializeField] private GameObject _gameEndSectionPrefab;
    [SerializeField] private TextMeshProUGUI _chapterNameText;
    [SerializeField] private GameEvent _moduleStartEvent;
    [SerializeField] private BoolVariable _isDrown;

    private int _moduleIndex = -1;
    private int _fixedRiverSectionIndex = 0;

    public void PauseVoiceOverPlayback()
    {
        if (_gameplaySettings.RewindAudio)
        {
            if (_voiceoverAudioSource.time >= _gameplaySettings.RewindLength)
            {
                _voiceoverAudioSource.time -= _gameplaySettings.RewindLength;
            }
            else
            {
                _voiceoverAudioSource.time = 0.01f;
            }
        }

        _voiceoverAudioSource.Pause();
    }

    public void PlayVoiceOverPlayback()
    {
        _voiceoverAudioSource.UnPause();
    }

    public void SpawnRiverSectionResponse()
    {
        if (_voiceoverAudioSource.time >= _voiceoverAudioSource.clip.length ||
            _voiceoverAudioSource.time == 0.0f)
        {
            MoveToNextModule();
        }
        else
        {
            SpawnModuleSection();
        }
    }

    public void MoveToNextModule()
    {
        if ((_moduleIndex + 1) >= _modules.Length)
        {
            SpawnEndGameSection();
            return;
        }

        if (_isDrown.Value)
        {
            SpawnModuleSection();
            return;
        }

        _moduleIndex++;
        _fixedRiverSectionIndex = -1;
        _voiceoverAudioSource.clip = _modules[_moduleIndex].VoiceoverClip;
        _voiceoverAudioSource.Play();
        SpawnModuleSection();
        _chapterNameText.text = _modules[_moduleIndex].ModuleName;
        _moduleStartEvent.Raise();
    }

    private void SpawnModuleSection()
    {
        GameObject sectionToSpawn;
        if (_fixedRiverSectionIndex + 1 <
            _modules[_moduleIndex].FixedSectionSequence.Length)
        {
            _fixedRiverSectionIndex++;
            sectionToSpawn = _modules[_moduleIndex]
                .GetFixedSection(_fixedRiverSectionIndex);
        }
        else
        {
            sectionToSpawn =
                _modules[_moduleIndex].GetRandomRiverSectionPrefab();
        }

        Instantiate(sectionToSpawn, new Vector3(0, 0, 24), Quaternion.identity);
    }

    private void SpawnEndGameSection()
    {
        Instantiate(_gameEndSectionPrefab, new Vector3(0, 0, 24), Quaternion.identity);
    }

}
