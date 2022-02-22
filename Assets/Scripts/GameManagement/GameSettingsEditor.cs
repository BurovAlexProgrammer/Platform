using System;
using Global;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameSettingsStore))]
public class GameSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var gameSettingsStore = target as GameSettingsStore;
        if (gameSettingsStore == null) throw new ArgumentNullException(nameof(gameSettingsStore));
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            gameSettingsStore.SaveSettings();
        }

        if (GUILayout.Button("Log"))
        {
            Debug.Log("Game settings: " + JsonUtility.ToJson(gameSettingsStore.currentGameSettings));
        }

        if (GUILayout.Button("Explorer"))
        {
            EditorUtility.RevealInFinder(GameData.savedSettingsPath);
        }

        if (GUILayout.Button("Reset"))
        {
            gameSettingsStore.ResetSettings();
        }

        GUILayout.EndHorizontal();
    }
}