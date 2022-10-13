using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom editor for the GameplaySettings data class.
/// </summary>
[CustomEditor(typeof(GameplaySettings))]
[CanEditMultipleObjects]
public class GameplaySettingsEditor : Editor
{
    // Movement
    private SerializedProperty _canMoveInAirProperty;
    private SerializedProperty _moveForceAirborneProperty;
    private SerializedProperty _moveForceGroundedProperty;

    // Jumping & Gravity
    private SerializedProperty _gravityScaleProperty;
    private SerializedProperty _jumpFroceProperty;
    private SerializedProperty _multiJumpProperty;
    private SerializedProperty _totalJumpCountProperty;

    // Drowning Effect
    private SerializedProperty _drownEffectFadeTimeProperty;
    private SerializedProperty _vignetteIntensityProperty;

    /// <summary>
    /// Should info be displayed in the editor?
    /// </summary>
    private static bool showInfo = true;

    #region Editor Methods
    private void OnEnable()
    {
        _canMoveInAirProperty = serializedObject.FindProperty("CanMoveInAir");
        _moveForceAirborneProperty = serializedObject.FindProperty("MoveForceAirborne");
        _moveForceGroundedProperty = serializedObject.FindProperty("MoveForceGrounded");

        _gravityScaleProperty = serializedObject.FindProperty("GravityScale");
        _jumpFroceProperty = serializedObject.FindProperty("JumpForce");
        _multiJumpProperty = serializedObject.FindProperty("MultiJump");
        _totalJumpCountProperty = serializedObject.FindProperty("TotalJumpCount");

        _drownEffectFadeTimeProperty = serializedObject.FindProperty("DrownEffectFadeTime");
        _vignetteIntensityProperty = serializedObject.FindProperty("VignetteIntensity");
    }

    public override void OnInspectorGUI()
    {
        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((ScriptableObject)target), GetType(), false);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Player Movement", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_moveForceGroundedProperty);
        EditorGUILayout.PropertyField(_canMoveInAirProperty);

        if (_canMoveInAirProperty.boolValue)
        {
            EditorGUILayout.PropertyField(_moveForceAirborneProperty);
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
        EditorGUILayout.PropertyField(_vignetteIntensityProperty);
        
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
