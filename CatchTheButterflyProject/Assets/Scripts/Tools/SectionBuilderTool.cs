using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class SectionBuilderTool : MonoBehaviour
{
    [SerializeField] private GameplaySettings _gameplaySettings;
    [SerializeField] private AudioClip _sectionAudioClip;
    [SerializeField] private Transform _rockObstacleParent;
    [SerializeField] private string _dataStoragePath = "Assets/Game/Data/RiverSectionData/";

    [Header("Object Guides")]
    [SerializeField] private Vector3 _playerStartPosition;

    [Header("Line Guides")]
    [SerializeField] private float _minXBoundary;
    [SerializeField] private float _maxXBoundary;
    [SerializeField] private float _waterLineYPosition;

    #region MonoBehaviour Methods
    private void OnDrawGizmos()
    {
        DrawLineGuides();
        DrawPlayerStartPositionGuide();
        DrawPlayerEndPositionGuide();
    }
    #endregion

    public void StoreSectionData()
    {
        // Store positions of all rock obstacles
        List<Vector3> rockObstaclePositions = new List<Vector3>();
        foreach (Transform child in _rockObstacleParent.transform)
        {
            rockObstaclePositions.Add(child.localPosition);
        }

        // Order obstacle positions by z value ascending.
        List<Vector3> positionsToWrite = rockObstaclePositions.OrderBy(rockPos => rockPos.z).ToList();

        // Write positions to asset
        RiverSectionData dataToStore = ScriptableObject.CreateInstance<RiverSectionData>();
        dataToStore.ObstaclePositions = positionsToWrite;

        // Write asset to database
        string path = _dataStoragePath + transform.root.name + ".asset";
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.CreateAsset(dataToStore, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = dataToStore;
    }

    private void DrawLineGuides()
    {
        Gizmos.color = Color.red;
        Handles.Label(new Vector3(_minXBoundary, 0.0f, 0.0f), "Top Boundary Line");
        Gizmos.DrawRay(new Vector3(_minXBoundary, 0.0f, 0.0f), Vector3.forward * 1000);
        Handles.Label(new Vector3(_maxXBoundary, 0.0f, 0.0f), "Bottom Boundary Line");
        Gizmos.DrawRay(new Vector3(_maxXBoundary, 0.0f, 0.0f), Vector3.forward * 1000);

        Gizmos.color = Color.blue;
        Handles.Label(new Vector3(0.0f, _waterLineYPosition, 0.0f), "Water Line");
        Gizmos.DrawRay(new Vector3(0.0f, _waterLineYPosition, 0.0f), Vector3.forward * 1000);
    }

    private void DrawPlayerEndPositionGuide()
    {
        if (_sectionAudioClip == null)
        {
            return;
        }

        Gizmos.color = Color.green;

        // Calculate player end positions
        float _playerEndMinDistance = _sectionAudioClip.length *
            (_gameplaySettings.PlayerBaseSpeed -
            _gameplaySettings.PlayerSpeedDelta);
        Vector3 _playerEndMinPosition = new Vector3(_minXBoundary, 0.0f, _playerEndMinDistance);
        Gizmos.DrawLine(_playerEndMinPosition,
            new Vector3(_maxXBoundary, 0.0f, _playerEndMinDistance));
        Handles.Label(_playerEndMinPosition, "Min Distance");

        float _playerEndMaxDistance = _sectionAudioClip.length *
            (_gameplaySettings.PlayerBaseSpeed +
            _gameplaySettings.PlayerSpeedDelta);
        Vector3 _playerEndMaxPosition = new Vector3(_minXBoundary, 0.0f, _playerEndMaxDistance);
        Gizmos.DrawLine(_playerEndMaxPosition,
            new Vector3(_maxXBoundary, 0.0f, _playerEndMaxDistance));
        Handles.Label(_playerEndMaxPosition, "Max Distance");

        float _playerEndBaseDistance = _sectionAudioClip.length *
            _gameplaySettings.PlayerBaseSpeed;
        Vector3 _playerEndBasePosition = new Vector3(_minXBoundary, 0.0f, _playerEndBaseDistance);
        Gizmos.DrawLine(_playerEndBasePosition,
            new Vector3(_maxXBoundary, 0.0f, _playerEndBaseDistance));
        Handles.Label(_playerEndBasePosition, "Base Distance");
        

    }

    private void DrawPlayerStartPositionGuide()
    {
        Gizmos.color = new Color(0, 1, 0, 0.25f);
        Handles.Label(_playerStartPosition, "Player");
        Gizmos.DrawCube(_playerStartPosition, Vector3.one * 0.25f);
    }
}
