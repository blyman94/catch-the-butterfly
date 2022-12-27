using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModuleSequence : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private AudioSource _voiceoverAudioSource;
    [SerializeField] private AudioFader _voiceoverAudioFader;
    [SerializeField] private TextMeshProUGUI _chapterNameText;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private ObstaclePool _obstaclePool;

    [Header("Prefabs")]
    [SerializeField] private GameObject _goalPrefab;

    [Header("Data")]
    [SerializeField] private GameplaySettings _gameplaySettings;
    [SerializeField] private Module[] _modules;
    [SerializeField] private FloatVariable _currentRiverSpeedVariable;
    [SerializeField] private BoolVariable _isDrown;

    [Header("Events")]
    [SerializeField] private GameEvent _moduleStartEvent;

    // States
    private bool _isDelaying = true;
    private bool _isSpawningFromModule = false;
    private bool _isSpawningRandomly = false;
    private bool _isVoiceOverFinished = false;
    private Module _currentModule;
    private int _moduleIndex = 0;
    private int _obstacleIndex = 0;
    private float _sectionZ = 0.0f;
    private float _randomSectionZ = 0.0f;
    private float _randomZDist = 0.0f;
    private int _pickupIndex = 0;

    #region MonoBehaviour Methods
    private void Start()
    {
        _moduleIndex = 0;
        _pickupIndex = 0;
        _sectionZ = 0;
        _randomSectionZ = 0;
        _currentModule = _modules[_moduleIndex];
        _randomZDist = Random.Range(_currentModule.MinZDist,
            _currentModule.MaxZDist);
    }
    private void Update()
    {
        if (!_isSpawningRandomly && !_isDrown.Value)
        {
            _sectionZ += _currentRiverSpeedVariable.Value * Time.deltaTime;
        }

        _isVoiceOverFinished = _voiceoverAudioSource.time >=
            _voiceoverAudioSource.clip.length ||
            _voiceoverAudioSource.time == 0.0f;

        // Spawn Pickup
        if (_pickupIndex < _currentModule.PickupSpawnTimes.Length && 
            _voiceoverAudioSource.time >= _currentModule.PickupSpawnTimes[_pickupIndex])
        {
            var randomX = Random.Range(_currentModule.MinXPos,
                _currentModule.MaxXPos);
            Instantiate(_currentModule.Pickups[_pickupIndex], new Vector3(randomX,
                0.0f, _spawnTransform.position.z), Quaternion.identity);
            _pickupIndex++;
        }

        if (_isDelaying && !_isDrown.Value)
        {
            if (_sectionZ >= _currentModule.StartDelayMeters)
            {
                _voiceoverAudioSource.Play();
                _chapterNameText.text = _modules[_moduleIndex].ModuleName;
                _moduleStartEvent.Raise();
                _sectionZ = 0;

                _isDelaying = false;
                _isSpawningFromModule = true;
            }
        }
        else if (_isSpawningFromModule && !_isDrown.Value)
        {
            var nextObstaclePos =
                _modules[_moduleIndex].RiverSectionData.ObstaclePositions[_obstacleIndex];
            if (_sectionZ >= nextObstaclePos.z)
            {
                var obstacle = _obstaclePool.Pool.Get();
                obstacle.transform.position = new Vector3(nextObstaclePos.x,
                    nextObstaclePos.y, _spawnTransform.position.z);
                if (_obstacleIndex + 1 < _currentModule.RiverSectionData.ObstaclePositions.Count)
                {
                    _obstacleIndex++;
                }
                else
                {
                    _obstacleIndex = 0;
                    _sectionZ = 0;

                    if (_isVoiceOverFinished)
                    {
                        MoveToNextModule();
                    }
                    else
                    {
                        _randomSectionZ = 0;
                        _isSpawningFromModule = false;
                        _isSpawningRandomly = true;
                    }
                }
            }
        }
        else if (_isSpawningRandomly || (_isDrown.Value && !_isVoiceOverFinished))
        {
            _randomSectionZ += _currentRiverSpeedVariable.Value * Time.deltaTime;
            if (_isVoiceOverFinished)
            {
                _obstacleIndex = 0;
                _sectionZ = 0;
                MoveToNextModule();
            }
            else
            {
                HandleRandomSpawn();
            }
        }
    }
    #endregion

    public void PauseVoiceoverPlayback()
    {
        _voiceoverAudioFader.FadeOut(_gameplaySettings.DrownEffectFadeTime, true);
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

        _voiceoverAudioFader.FadeIn(_gameplaySettings.DrownEffectFadeTime, true);
    }

    private void HandleRandomSpawn()
    {
        if (_randomSectionZ >= _randomZDist)
        {
            var randomX = Random.Range(_currentModule.MinXPos,
                _currentModule.MaxXPos);
            var obstacle = _obstaclePool.Pool.Get();
            obstacle.transform.position = new Vector3(randomX,
                0.0f, _spawnTransform.position.z);
            _randomZDist = Random.Range(_currentModule.MinZDist,
                _currentModule.MaxZDist);
            _randomSectionZ = 0;
        }
    }

    private void MoveToNextModule()
    {
        if (_moduleIndex + 1 < _modules.Length)
        {
            _moduleIndex++;
            _pickupIndex = 0;
            _currentModule = _modules[_moduleIndex];
            if (_currentModule.StartDelayMeters > 0.0f)
            {
                _isDelaying = true;
                _isSpawningFromModule = false;
            }
            else
            {
                _isSpawningFromModule = true;
            }
        }
        else
        {
            _isSpawningFromModule = false;
            _isSpawningRandomly = false;

            Vector3 goalSpawnPos = new Vector3(0.0f,
                0.0f, _spawnTransform.position.z);
            Instantiate(_goalPrefab, goalSpawnPos, Quaternion.identity);
        }
    }
}
