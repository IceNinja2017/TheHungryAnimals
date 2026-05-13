using System.IO;
using UnityEngine;

public static class PlayerDataHandler
{
    private static string path = Application.persistentDataPath + "/playerdata.json";

    public static PlayerSaveData Load()
    {
        if (!File.Exists(path))
        {
            // create default data
            PlayerSaveData defaultData = GetDefaultData();

            // save it immediately so file exists
            Save(defaultData);

            return defaultData;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<PlayerSaveData>(json);
    }

    public static void Save(PlayerSaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    private static PlayerSaveData GetDefaultData()
    {
        return new PlayerSaveData
        {
            coinsOrTotalScore = 0,
            selectedCharacterId = "default",
            unlockedCharacters = new System.Collections.Generic.List<string>()
        };
    }
}