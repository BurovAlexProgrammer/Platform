using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Common.Editor
{
    [InitializeOnLoad]
    public class AutoSave
    {
        static AutoSave()
        {
            EditorApplication.playModeStateChanged += SaveOnPlay;
        }

        private static void SaveOnPlay(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
                Save();
        }

        private async void SaveByTimeout()
        {
            var timer = 0f;
            var timeout = 5 * 60;
            while (true)
            {
                if (timer >= timeout)
                {
                    timer = 0;
                    Save();
                }

                await Task.Yield();
                timer += Time.deltaTime;
            }
        }

        private static void Save()
        {
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
            Debug.Log("Auto-saving done.");
        }
    }
}