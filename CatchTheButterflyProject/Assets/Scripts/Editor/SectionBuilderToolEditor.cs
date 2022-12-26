using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SectionBuilderTool), editorForChildClasses: true)]
public class SectionBuilderToolEditor : Editor
{
    public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = !Application.isPlaying;

            EditorGUILayout.Space();

            SectionBuilderTool s = target as SectionBuilderTool;
            if (GUILayout.Button("Store Data"))
                s.StoreSectionData();
        }
}
