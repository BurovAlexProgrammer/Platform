using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class DebugInformer : MonoBehaviour
{
    [SerializeField] private bool showDebugInfo;

    [UsedImplicitly]
    public bool ShowDebugInfo
    {
        get => showDebugInfo;
        set
        {
            showDebugInfo = value;
            OnShowDebugInfoChanged();
        }
    }

    private GameSettingsStore _gameSettingsStore;
    [SerializeField] private DebugInfo info = new DebugInfo();
    [SerializeField] private TextMeshProUGUI textMesh;

    public DebugInfo GetInfo()
    {
        return info.Send();
    }

    private void Start()
    {
        if (textMesh is null) throw new ArgumentNullException(nameof(textMesh));
        _gameSettingsStore = GameObject.FindWithTag("GameManager").GetComponent<GameSettingsStore>();
        if (_gameSettingsStore is null) throw new ArgumentNullException(nameof(_gameSettingsStore));
        ShowDebugInfo = _gameSettingsStore.currentGameSettings.showDebugInfo;
    }

    private async void CalculateFps()
    {
        var fpsCount = 0;
        var fpsElapsedTime = 1f;
        while (ShowDebugInfo)
        {
            if (fpsElapsedTime <= 0f)
            {
                fpsElapsedTime = 1f;
                info.fps = fpsCount;
                OnDebugInfoChanged();
                fpsCount = 0;
            }

            fpsElapsedTime -= Time.deltaTime;
            fpsCount++;
            await Task.Yield();
        }
    }

    private void OnShowDebugInfoChanged()
    {
        if (ShowDebugInfo) CalculateFps();
    }

    private void OnDebugInfoChanged()
    {
        var nl = Environment.NewLine;
        var text = $"FPS: {info.fps} {nl}";
        textMesh.SetText(text);
    }
}