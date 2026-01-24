using System.IO;
using UnityEngine;

public static class SaveService
{
    private const string FILE_Name = "save.json";
    private static string FilePath => Path.Combine(Application.persistentDataPath, FILE_Name);

    public static void Save(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(FilePath, json);
        Debug.Log("Player data saved at " + FilePath);
    }

    public static SaveData Load()
    {
        string json = File.ReadAllText(FilePath);

        if(string.IsNullOrEmpty(json))
        {
            Debug.LogError("No save data found at path : " +  FilePath);
            return null;
        }

        var result = JsonUtility.FromJson<SaveData>(json);

        return result;
    }
}