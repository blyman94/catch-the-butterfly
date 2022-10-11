using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom editor for the Sensor3D object.
/// </summary>
[CustomEditor(typeof(Sensor3D))]
[CanEditMultipleObjects]
public class Sensor3DEditor : Editor
{
    private SerializedProperty m_SensorShapeProperty;
    private SerializedProperty m_SensorSenseTagProperty;
    private SerializedProperty m_SensorRadiusProperty;
    private SerializedProperty m_SensorBoxSizeProperty;
    private SerializedProperty m_SensorLayerToSenseProperty;
    private SerializedProperty m_SensorTagToSenseProperty;
    private SerializedProperty m_SensorActiveColorProperty;
    private SerializedProperty m_SensorInactiveColorProperty;
    private SerializedProperty m_SensorActiveProperty;

    /// <summary>
    /// Should info be displayed in the editor?
    /// </summary>
    private static bool showInfo = true;

    void OnEnable()
    {
        m_SensorShapeProperty = serializedObject.FindProperty("SensorShape");
        m_SensorSenseTagProperty = serializedObject.FindProperty("SenseTag");
        m_SensorRadiusProperty = serializedObject.FindProperty("radius");
        m_SensorBoxSizeProperty = serializedObject.FindProperty("boxSize");
        m_SensorLayerToSenseProperty = serializedObject.FindProperty("layerToSense");
        m_SensorTagToSenseProperty = serializedObject.FindProperty("tagToSense");
        m_SensorActiveColorProperty = serializedObject.FindProperty("activeColor");
        m_SensorActiveProperty = serializedObject.FindProperty("active");
        m_SensorInactiveColorProperty = serializedObject.FindProperty("inactiveColor");
    }

    public override void OnInspectorGUI()
    {
        var sensor = target as Sensor3D;

        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), GetType(), false);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Geometry", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(m_SensorShapeProperty);

        switch (sensor.SensorShape)
        {
            case SensorShape.Sphere:
                EditorGUILayout.PropertyField(m_SensorRadiusProperty);
                break;
            case SensorShape.Box:
                EditorGUILayout.PropertyField(m_SensorBoxSizeProperty);
                break;
            default:
                break;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("What To Sense", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(m_SensorLayerToSenseProperty);
        EditorGUILayout.PropertyField(m_SensorSenseTagProperty);

        if (sensor.SenseTag)
        {
            EditorGUILayout.PropertyField(m_SensorTagToSenseProperty);
        }

        EditorGUILayout.LabelField("Colors", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(m_SensorActiveColorProperty);
        EditorGUILayout.PropertyField(m_SensorInactiveColorProperty);

        showInfo = EditorGUILayout.Foldout(showInfo, "Info");
        if (showInfo)
        {
            using (new EditorGUI.DisabledScope(true))
                EditorGUILayout.PropertyField(m_SensorActiveProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
