using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Business : MonoBehaviour
{
    public BusinessData businessData;

    private int level = 1;
    private float _currentMoneyProduction = 0;
    private float _timeWhenUpgradingFinished = 0;
    
    private void Awake()
    {
        _currentMoneyProduction = businessData.baseMoneyProduction;
        Events.OnBusinessUpgradedFinish += OnBusinessUpgradedFinish;
        Events.OnBusinessUpgradedStart += OnBusinessUpgradedStart;
    }

    private void OnDestroy()
    {
        Events.OnBusinessUpgradedFinish -= OnBusinessUpgradedFinish;
        Events.OnBusinessUpgradedStart -= OnBusinessUpgradedStart;
    }

    private void Update()
    {
        if (_timeWhenUpgradingFinished > Time.time)
        {
            if (!businessData.upgradeAnimation.isPlaying)
            {
                businessData.upgradeAnimation.Play();
            } 
        }
        else
        {
            businessData.upgradeAnimation.Stop();
        }
    }

    private void OnBusinessUpgradedFinish(Business business)
    {
        if(business == this)
        {
            level++;
            _currentMoneyProduction += businessData.moneyProductionStep;
        }
    }

    private void OnBusinessUpgradedStart(Business business)
    {
        if(business != this) return;
        _timeWhenUpgradingFinished = calculateNextUpgradeFinishTime();

    }

    private float calculateNextUpgradeFinishTime()
    {
        return Time.time + (businessData.baseUpgradeTime) +
               (businessData.eachLevelTimeStep * level);
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
        if (_timeWhenUpgradingFinished > Time.time)
        {
            return 0;
        }
        return _currentMoneyProduction;
    }

    public int getLevel()
    {
        return level;
    }

    public void setLevel(int level)
    {
        this.level = level;
        _currentMoneyProduction = businessData.baseMoneyProduction + (level-1) * businessData.upgradeCostStep;
    }
}
