using UnityEngine;
using TMPro;

public class ModuleSequence : MonoBehaviour
{
    [SerializeField] private GameplaySettings _gameplaySettings;
    [SerializeField] private AudioSource _voiceoverAudioSource;
    [SerializeField] private AudioFader _voiceoverAudioFader;
    [SerializeField] private Module[] _modules;
    [SerializeField] private GameObject _gameEndSectionPrefab;
    [SerializeField] private TextMeshProUGUI _chapterNameText;
    [SerializeField] private GameEvent _moduleStartEvent;
    [SerializeField] private BoolVariable _isDrown;

    private int _moduleIndex = -1;
    private int _clipIndex = -1;
    private int _fixedRiverSectionIndex = 0;

    public void PauseVoiceOverPlayback()
    {
        _voiceoverAudioFader.FadeOut(_gameplaySettings.AudioFadeTime, true);
    }

    public void PlayVoiceOverPlayback()
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

        _voiceoverAudioFader.FadeIn(_gameplaySettings.AudioFadeTime, true);
    }

    public void SpawnRiverSectionResponse()
    {
        if (_voiceoverAudioSource.time >= _voiceoverAudioSource.clip.length ||
            _voiceoverAudioSource.time == 0.0f)
        {

            if (_moduleIndex != -1 && (_clipIndex + 1) < _modules[_moduleIndex].VoiceoverClips.Length)
            {
                _clipIndex++;
                _voiceoverAudioSource.clip = _modules[_moduleIndex].VoiceoverClips[_clipIndex];
                _voiceoverAudioSource.Play();
                SpawnModuleSection();
                return;
            }

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
        _clipIndex = 0;
        _fixedRiverSectionIndex = -1;
        _voiceoverAudioSource.clip = _modules[_moduleIndex].VoiceoverClips[_clipIndex];
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
