
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static string savePath => Path.Combine(Application.persistentDataPath, "save.json");

    public static void Save()
    {
        List<BusinessSaveData> saveData = new List<BusinessSaveData>();
        List<Business> businessList = Events.RequestBusinesses();
        foreach (var business in businessList)
        {
            saveData.Add(new BusinessSaveData
            {
                businessData = business.businessData, // Use the name of the ScriptableObject.
                level = business.getLevel(),
                position = business.transform.position
            });
        }

        string json = JsonUtility.ToJson(new SaveData
        {
            businessSaveData = saveData,
            level = Events.RequestLevel(),
            money = Events.RequestMoney(),
        }, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"Game saved to {savePath}");
    }

    public static SaveData Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("Save file not found.");
            return null;
        }
        string json = File.ReadAllText(savePath);
        SaveData container = JsonUtility.FromJson<SaveData>(json);
        return container;
    }

}
