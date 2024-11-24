using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Business : MonoBehaviour
{
    public BusinessData businessData;

    private int level = 1;
    private float _currentMoneyProduction = 0;
    private void Awake()
    {
        _currentMoneyProduction = businessData.baseMoneyProduction;
        Events.OnBusinessUpgraded += OnBusinessUpgraded;
    }

    private void OnDestroy()
    {
        Events.OnBusinessUpgraded -= OnBusinessUpgraded;
    }

    private void OnBusinessUpgraded(Business business)
    {
        if(business == this)
        {
            level++;
            _currentMoneyProduction += businessData.moneyProductionStep;
        }
    }

    public bool isUpgradable()
    {
        if (level == businessData.amountOfUpgrades + 1) return false;
        return true;
    }

    public float calculateNextLevelCost()
    {
        return businessData.baseUpgradeCost + (level * businessData.upgradeCostStep);
    }

    public float getCurrentProduction()
    {
        return _currentMoneyProduction;
    }

    public int getLevel()
    {
        return level;
    }
    
}
