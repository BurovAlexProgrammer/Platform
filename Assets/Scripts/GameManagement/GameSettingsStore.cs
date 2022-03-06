using System;
using System.IO;
using UnityEngine;

namespace GameManagement
{
    [ExecuteAlways]
    public class GameSettingsStore : MonoBehaviour
    {
        private readonly GameSettings _defaultGameSettings = new GameSettings
        {
            state = "default",
            showDebugInfo = false
        };

        public GameSettings currentGameSettings;
        private GameSettings _savedGameSettings = new GameSettings();

        private void Start()
        {
            Debug.Log("GameSettingsStore Start..");
            LoadSettings();
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public void LoadSettings()
        {
            if (File.Exists(GameData.SavedSettingsPath))
            {
                JsonUtility.FromJsonOverwrite(File.ReadAllText(GameData.SavedSettingsPath), _savedGameSettings);
            }
            else
            {
                Debug.LogWarning("Saved settings file doesn't exist. Creating new from default settings");
                CreateSettingsFile();
                _savedGameSettings = _defaultGameSettings;
            }

            currentGameSettings = _savedGameSettings.Clone();
        }

        public void ResetSettings()
        {
            currentGameSettings = _defaultGameSettings.Clone();
        }

        private void CreateSettingsFile()
        {
            var settingsFolder = Path.GetDirectoryName(GameData.SavedSettingsPath);
            if (settingsFolder is null) throw new Exception("SettingsFolder cannot be null.");
            if (!Directory.Exists(settingsFolder))
            {
                Directory.CreateDirectory(settingsFolder);
                Debug.Log("Settings folder has been created.");
            }

            SaveSettings();
            Debug.Log("Settings file has been created.");
        }

        public void SaveSettings()
        {
            File.WriteAllText(GameData.SavedSettingsPath, JsonUtility.ToJson(currentGameSettings));
            _savedGameSettings = currentGameSettings.Clone();
            Debug.Log("Settings file has been saved." + Environment.NewLine +
                      "Game settings: " + JsonUtility.ToJson(_savedGameSettings));
        }
    }
}