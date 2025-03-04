using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveController: MonoBehaviour
{
    public float autoSaveIntervalSeconds = 30f;
    private float _lastSaveTime;

    public bool deleteSave = false;

    private void Awake()
    {
        if (deleteSave)
        {
            SaveSystem.DeleteSaveData();
        }
    }

    private void Start()
    {
        
        SaveData data = SaveSystem.Load();
        if(data == null) return;
        Debug.Log("Loaded Level: " + data.level);
        Debug.Log("Loaded money: " + data.money);
        Debug.Log("Loaded rocks: " + data.rocks);
        Debug.Log("Loaded oil: " + data.oil);
        Debug.Log("Loaded iron: " + data.iron);
        Debug.Log("Loaded water: " + data.water);
        Debug.Log("Loaded gold: " + data.gold);
        Events.SetLevel(data.level);
        Events.SetMoney(data.money);
        Events.SetRocks(data.rocks);
        Events.SetOil(data.oil);
        Events.SetIron(data.iron);
        Events.SetWater(data.water);
        Events.SetGold(data.gold);
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
            business.upgradeTimeLeft = businessSaveData.upgradeTimeLeft;
            businesses.Add(business);
        }
        return businesses;
    }
    

    private void OnApplicationQuit()
    {
        SaveSystem.Save();
    }
}