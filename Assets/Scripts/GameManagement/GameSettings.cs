using System;

[Serializable]
public class GameSettings {
    public string state;
    public bool showDebugInfo;

    public GameSettings Clone()
    {
        return (GameSettings) this.MemberwiseClone();
    } 
}

