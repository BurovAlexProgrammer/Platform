using System;

[Serializable]
public class DebugInfo
{
    public int fps;


    public DebugInfo Send()
    {
        return (DebugInfo) this.MemberwiseClone();
    }
}