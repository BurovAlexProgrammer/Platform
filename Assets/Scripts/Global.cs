using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

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

public static class Extensions
{
    public static void CheckExisting(this object T)
    {
        if (T is null)
            throw new Exception($"{T.GetType()?.Name} is null.");
    }

    public static void Replace(this GameObject T, GameObject prefab)
    {
        var newGameObject = (GameObject) PrefabUtility.InstantiatePrefab(prefab);
        newGameObject.transform.parent = T.transform.parent;
        newGameObject.transform.position = T.transform.position;
        newGameObject.transform.rotation = T.transform.rotation;
        newGameObject.transform.localScale = T.transform.localScale;
        newGameObject.transform.name = T.transform.name;
        newGameObject.transform.tag = T.transform.tag;
        newGameObject.layer = T.layer;
        Object.Destroy(T);
    }

    public static bool CompareTagWithParents(this Transform T, string tag)
    {
        var parent = T.parent;
        if (parent != null)
            return parent.CompareTag(tag) || CompareTagWithParents(parent, tag);
        return false;
    }

    public static Transform FindParentWithTag(this Transform T, string tag)
    {
        var parent = T.parent;
        if (parent != null)
            return parent.CompareTag(tag) ? parent : FindParentWithTag(parent, tag);
        return null;
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