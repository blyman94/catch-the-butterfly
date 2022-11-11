using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom editor for the GameplaySettings data class.
/// </summary>
[CustomEditor(typeof(GameplaySettings))]
[CanEditMultipleObjects]
public class GameplaySettingsEditor : Editor
{
    // Audio
    private SerializedProperty _defaultMusicVolumeProperty;
    private SerializedProperty _defaultSFXVolumeProperty;
    private SerializedProperty _defaultVoiceVolumeProperty;

    // Movement
    private SerializedProperty _constantMoveSpeedProperty;
    private SerializedProperty _moveForceGroundedProperty;
    private SerializedProperty _downstreamMaxSpeedIncreaseProperty;
    private SerializedProperty _upstreamMaxSpeedProperty;
    private SerializedProperty _useConstantMoveSpeed;

    // Jumping & Gravity
    private SerializedProperty _gravityScaleProperty;
    private SerializedProperty _jumpFroceProperty;
    private SerializedProperty _multiJumpProperty;
    private SerializedProperty _totalJumpCountProperty;

    // Drown Effect
    private SerializedProperty _cameraShakeIntensity;
    private SerializedProperty _drownEffectFadeTimeProperty;
    private SerializedProperty _graphicsBlinkCountProperty;
    private SerializedProperty _useCameraShakeProperty;
    private SerializedProperty _useGraphicsBlinkProperty;
    private SerializedProperty _useVignetteProperty;
    private SerializedProperty _useColorDesaturationProperty;
    private SerializedProperty _vignetteIntensityProperty;

    /// <summary>
    /// Should info be displayed in the editor?
    /// </summary>
    private static bool showInfo = true;

    #region Editor Methods
    private void OnEnable()
    {
        // Audio 
        _defaultMusicVolumeProperty = serializedObject.FindProperty("DefaultMusicVolume");
        _defaultSFXVolumeProperty = serializedObject.FindProperty("DefaultSFXVolume");
        _defaultVoiceVolumeProperty = serializedObject.FindProperty("DefaultVoiceVolume");
        
        // Movement
        _constantMoveSpeedProperty = serializedObject.FindProperty("ConstantMoveSpeed");
        _downstreamMaxSpeedIncreaseProperty = serializedObject.FindProperty("DownstreamMaxSpeedIncrease");
        _moveForceGroundedProperty = serializedObject.FindProperty("MoveForceGrounded");
        _upstreamMaxSpeedProperty = serializedObject.FindProperty("UpstreamMaxSpeed");
        _useConstantMoveSpeed = serializedObject.FindProperty("UseConstantMoveSpeed");

        // Jump & Gravity
        _gravityScaleProperty = serializedObject.FindProperty("GravityScale");
        _jumpFroceProperty = serializedObject.FindProperty("JumpForce");
        _multiJumpProperty = serializedObject.FindProperty("MultiJump");
        _totalJumpCountProperty = serializedObject.FindProperty("TotalJumpCount");

        // Drown Effect
        _cameraShakeIntensity = serializedObject.FindProperty("CameraShakeIntensity");
        _drownEffectFadeTimeProperty = serializedObject.FindProperty("DrownEffectFadeTime");
        _graphicsBlinkCountProperty = serializedObject.FindProperty("GraphicsBlinkCount");
        _useCameraShakeProperty = serializedObject.FindProperty("UseCameraShake");
        _useGraphicsBlinkProperty = serializedObject.FindProperty("UseGraphicsBlink");
        _useVignetteProperty = serializedObject.FindProperty("UseVignette");
        _useColorDesaturationProperty = serializedObject.FindProperty("UseColorDesaturation");
        _vignetteIntensityProperty = serializedObject.FindProperty("VignetteIntensity");
    }

    public override void OnInspectorGUI()
    {
        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((ScriptableObject)target), GetType(), false);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Player Movement", EditorStyles.boldLabel);
        
        EditorGUILayout.PropertyField(_moveForceGroundedProperty);
        EditorGUILayout.PropertyField(_useConstantMoveSpeed);
        
        if (_useConstantMoveSpeed.boolValue)
        {
            EditorGUILayout.PropertyField(_constantMoveSpeedProperty);
        }
        else
        {
            EditorGUILayout.PropertyField(_downstreamMaxSpeedIncreaseProperty);
            EditorGUILayout.PropertyField(_upstreamMaxSpeedProperty);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Player Jumping & Gravity", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_jumpFroceProperty);
        EditorGUILayout.PropertyField(_gravityScaleProperty);
        EditorGUILayout.PropertyField(_multiJumpProperty);

        if (_multiJumpProperty.boolValue)
        {
            EditorGUILayout.PropertyField(_totalJumpCountProperty);
        }
        else
        {
            _totalJumpCountProperty.intValue = 1;
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Drown Effect", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_drownEffectFadeTimeProperty);

        EditorGUILayout.PropertyField(_useCameraShakeProperty);
        if (_useCameraShakeProperty.boolValue)
        {
            EditorGUILayout.PropertyField(_cameraShakeIntensity);
        }

        EditorGUILayout.PropertyField(_useGraphicsBlinkProperty);
        if (_useGraphicsBlinkProperty.boolValue)
        {
            EditorGUILayout.PropertyField(_graphicsBlinkCountProperty);
        }

        EditorGUILayout.PropertyField(_useColorDesaturationProperty);

        EditorGUILayout.PropertyField(_useVignetteProperty);
        if (_useVignetteProperty.boolValue)
        {
            EditorGUILayout.PropertyField(_vignetteIntensityProperty);
        }

        EditorGUILayout.Space();
        
        EditorGUILayout.LabelField("Player Movement", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_defaultMusicVolumeProperty);
        EditorGUILayout.PropertyField(_defaultSFXVolumeProperty);
        EditorGUILayout.PropertyField(_defaultVoiceVolumeProperty);
        
        EditorGUILayout.Space();

        showInfo = EditorGUILayout.Foldout(showInfo, "Info");
        if (showInfo)
        {
            using (new EditorGUI.DisabledScope(true))
                EditorGUILayout.PropertyField(_totalJumpCountProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
    #endregion
}
