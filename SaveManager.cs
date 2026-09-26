using System.IO;
using UnityEngine;

/// <summary>
/// JSON-based save/load utility, ported from the Love2D SaveManager module.
/// Uses Unity's JsonUtility, so any data class you pass in must be [Serializable]
/// and only contain fields JsonUtility supports (no Dictionary, use lists of pairs instead).
/// </summary>
public static class SaveManager
{
    private static string GetPath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName + ".json");
    }

    public static void Save<T>(T data, string fileName)
    {
        string json = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(GetPath(fileName), json);
    }

    public static bool Exists(string fileName)
    {
        return File.Exists(GetPath(fileName));
    }

    public static T Load<T>(string fileName) where T : new()
    {
        string path = GetPath(fileName);
        if (!File.Exists(path))
            return new T();

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }

    public static void Delete(string fileName)
    {
        string path = GetPath(fileName);
        if (File.Exists(path))
            File.Delete(path);
    }
}
