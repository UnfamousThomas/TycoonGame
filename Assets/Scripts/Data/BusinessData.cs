﻿using System.Collections.Generic;
using UnityEngine;

public enum BusinessProductionType
{
    BASED_ON_FLOOR,
    MONEY
}

[CreateAssetMenu(menuName = "SpaceTycoon/Business")]
public class BusinessData : ScriptableObject
{
    public string businessName;
    public float cost;
    public List<List<ResourceFloatPair>> baseUpgradeCost; //Internal list is a AND relationship, outer is OR.
    
    public float upgradeCostStep; // Percentage to increase each resource cost per level
    public int amountOfUpgrades;

    public BusinessProductionType productionType;
    public float baseProductionRate;
    public float productionStep; //Percentage to increase production each level
    
    public bool canBuildAnywhere;
    public List<ResourceType> canBeBuiltOn;
    
    public Business businessPrefab;
    public Sprite icon;
    public Sprite upgradeSprite;
    
    public bool onlyOneAllowed;
    
    public float baseUpgradeTime;
    public float eachLevelTimeStep;
    
    public float sellingPriceMultiplier; // What percentage of the cost will be returned when selling.
}