﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class GameData {
    private static string DataPath {
        get {
            var path = Application.persistentDataPath + "/Data/";
            if (!Directory.Exists(path)) {
                Debug.LogWarning($"Directory '{path}' is not exist. Creating..");
                Directory.CreateDirectory(path);
                Debug.Log("Directory has been created.");
            }
            return path;
        }
    }

    private const string SavedSettingsFileName = "savedSettings.json";
    public static readonly string SavedSettingsPath = Path.Combine(DataPath, SavedSettingsFileName);
}

public static class Extensions {
    public static void CheckExisting(this object T) {
        if (T is null)
            throw new Exception($"{T.GetType()?.Name} is null.");
    }
}

[Serializable]
public struct RangedFloat {
    public float minValue;
    public float maxValue;
}

public class MinMaxRangeAttribute : Attribute {
    public MinMaxRangeAttribute(float min, float max) {
        Min = min;
        Max = max;
    }
    public float Min { get; private set; }
    public float Max { get; private set; }
}

//Global end