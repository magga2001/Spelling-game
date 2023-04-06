using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

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

#endif
