using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Business : MonoBehaviour
{
    public BusinessData businessData;

    private int level = 1;
    private float _currentProduction = 0;
    private SpriteRenderer _spriteRenderer;
    public float upgradeTimeLeft = 0;
    private SpawnedResource _floorResource;
    
    private void Awake()
    {
        _currentProduction = businessData.baseProductionRate;
        Events.OnBusinessUpgradedFinish += OnBusinessUpgradedFinish;
        Events.OnBusinessUpgradedStart += OnBusinessUpgradedStart;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D[] nearbyCols = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach (var nearbyCol in nearbyCols)
        {
            SpawnedResource resource = nearbyCol.GetComponent<SpawnedResource>();
            if (resource != null)
            {
                _floorResource = resource;
            }
        }
    }

    private void OnDestroy()
    {
        Events.OnBusinessUpgradedFinish -= OnBusinessUpgradedFinish;
        Events.OnBusinessUpgradedStart -= OnBusinessUpgradedStart;
    }

    private void Update()
    {
        if (isBeingUpgraded())
        {
            upgradeTimeLeft -= Time.deltaTime;
            if (upgradeTimeLeft > 0)
            {
                _spriteRenderer.sprite = businessData.upgradeSprite;
            }
            else
            {
                _spriteRenderer.sprite = businessData.icon;
                Events.FinishUpgradeBusiness(this);
            }
        }
    }

    private void OnBusinessUpgradedFinish(Business business)
    {
        if (business == this)
        {
            level++;
            _currentProduction += (_currentProduction * businessData.productionStep);
        }
    }

    private void OnBusinessUpgradedStart(Business business)
    {
        if (business != this) return;
        upgradeTimeLeft = calculateNextUpgradeTime();
        Events.SetMoney(Events.RequestMoney() - business.calculateNextLevelCost());
    }

    private float calculateNextUpgradeTime()
    {
        return  (businessData.baseUpgradeTime) +
               (businessData.eachLevelTimeStep * level);
    }

    public bool isUpgradable()
    {
        if (level == businessData.amountOfUpgrades + 1) return false;
        return true;
    }

    public float calculateNextLevelCost()
    {
        //TODO change this to display correct data
        // The basic idea is that there are is List<List<ResourceFloatPair>> in businessdata, which refers to the base cost.
        // In there, inner list has a AND relationship, meaning you have to have all of the resources defined in ResourceFloatPairs.
        // The outer list is a OR relation ship, meaning you have to all the resources in one of the inner lists.
        // There is also a upgradeCostStep float, which is a float from [0] to [1], where each value in the ResourceFloatPairs is increased
        // by some percentage depending on that, so if 1, then 100% of previous price.
        return 1;
    }

    public ResourceType getProducedResource()
    {
        if (businessData.productionType == BusinessProductionType.MONEY)
        {
            return ResourceType.MONEY;
        }

        if (businessData.productionType == BusinessProductionType.BASED_ON_FLOOR)
        {
            return _floorResource.resource.resourceType;
        }
        Debug.Log("Unknown production type: " + businessData.productionType);
        return ResourceType.MONEY;
    }
    public float getCurrentProduction()
    {
        if (isBeingUpgraded())
        {
            return 0;
        }
        return _currentProduction;
    }

    public int getLevel()
    {
        return level;
    }

    public void setLevel(int level)
    {
        this.level = level;
        _currentProduction = GetProductionAtLevel(level);
    }
    
    float GetProductionAtLevel(int level)
    {
        float production = businessData.baseProductionRate;
        for (int i = 1; i <= level; i++)
        {
            production += (production * businessData.productionStep);
        }
        return production;
    }


    public bool isBeingUpgraded()
    {
        return upgradeTimeLeft > 0;
    }
}
