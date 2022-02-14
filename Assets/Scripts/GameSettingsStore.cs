using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Global;

[ExecuteAlways]
public class GameSettingsStore : MonoBehaviour
{
    public GameSettings defaultGameSettings;
    public GameSettings currentGameSettings;
    public GameSettings savedGameSettings;

    private void Awake() {
        savedGameSettings ??= ScriptableObject.CreateInstance<GameSettings>();
        if (!File.Exists(GameData.savedSettingsPath)) {
            Debug.LogWarning("Saved settings file doesn't exist. Creating new from default settings");
            if (!defaultGameSettings)
                throw new System.Exception("defaultGameSettings is null");
            CreateSettingsFile();
            savedGameSettings = defaultGameSettings;
        }
        JsonUtility.FromJsonOverwrite(File.ReadAllText(GameData.savedSettingsPath), savedGameSettings);
        if (!savedGameSettings) {
            Debug.LogError("Settings file was broken. Creating file from default settings.");
            CreateSettingsFile();
            savedGameSettings = defaultGameSettings;
        }
        currentGameSettings = savedGameSettings;

        DontDestroyOnLoad(gameObject);
    }

    void CreateSettingsFile() {
        File.WriteAllText(GameData.savedSettingsPath, JsonUtility.ToJson(defaultGameSettings));
        Debug.Log("Settings file has been created.");
    }
}
