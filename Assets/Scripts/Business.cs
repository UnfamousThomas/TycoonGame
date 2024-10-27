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
        _currentMoneyProduction += businessData.moneyProductionStep;
    }

    private bool isUpgradable(float money)
    {
        if (level == businessData.amountOfUpgrades + 1) return false;
        if (money < calculateNextLevelCost()) return false;
        return true;
    }

    private float calculateNextLevelCost()
    {
        return businessData.baseUpgradeCost + (level - 1 * businessData.upgradeCostStep);
    }

    public float getCurrentProduction()
    {
        return _currentMoneyProduction;
    }
}
