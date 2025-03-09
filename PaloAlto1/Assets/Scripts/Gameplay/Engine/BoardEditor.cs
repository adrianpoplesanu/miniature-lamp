using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Board))]
public class BoardEditor : Editor {
    public override void OnInspectorGUI() {
        // Draw the default Inspector
        DrawDefaultInspector();

        // Add a custom button
        Board myScript = (Board)target;

        if (GUILayout.Button("Generate Random Board")) {
            myScript.GenerateRandomValues();
        }

        if (GUILayout.Button("Save Board")) {
            myScript.SaveCurrentBoard();
        }

        for (int i = 0; i < myScript.mapSize.x; i++) {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < myScript.mapSize.y; j++) {
                myScript.values[i, j] = EditorGUILayout.IntField(myScript.values[i, j], GUILayout.Width(16));
                //EditorGUILayout.IntField(myScript.values[i, j], GUILayout.Width(16));
            }
            GUILayout.EndHorizontal();
        }
    }
}
