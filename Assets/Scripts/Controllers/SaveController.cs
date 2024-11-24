using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveController: MonoBehaviour
{
    public float autoSaveIntervalSeconds = 30f;
    private float _lastSaveTime;
    private void Start()
    {
        SaveData data = SaveSystem.Load();
        if(data == null) return;
        Debug.Log("Loaded Level: " + data.level);
        Debug.Log("Loaded money: " + data.money);
        Events.SetLevel(data.level);
        Events.SetMoney(data.money);
        Events.LoadBusinesses(createBusinesses(data));
    }

    private void Update()
    {
        if (Time.time - _lastSaveTime > autoSaveIntervalSeconds)
        {
            SaveSystem.Save();
            Debug.Log("Auto save");
            _lastSaveTime = Time.time;
        }
    }

    private List<Business> createBusinesses(SaveData data)
    {
        List<Business> businesses = new List<Business>();
        foreach (var businessSaveData in data.businessSaveData)
        {
            Business business = Instantiate(businessSaveData.businessData.businessPrefab,
                businessSaveData.position, Quaternion.identity);
            business.setLevel(businessSaveData.level);
            businesses.Add(business);
        }
        return businesses;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        SaveSystem.Save();
    }

    private void OnApplicationQuit()
    {
        SaveSystem.Save();
    }
}