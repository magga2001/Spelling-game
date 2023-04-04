using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(AlphabetData), false), CanEditMultipleObjects]
public class AlphabetDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
    }
}
