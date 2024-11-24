using UnityEngine;

[CreateAssetMenu(menuName = "SpaceTycoon/Business")]
public class BusinessData : ScriptableObject
{
    public string businessName;
    public float cost;
    public float baseUpgradeCost;
    public float upgradeCostStep; // How much the cost increases with each upgrade.
    public int amountOfUpgrades;
    public float baseMoneyProduction;
    public float moneyProductionStep; // Same as upgrade step, but 
    public Business businessPrefab;
    public Sprite icon;
    public bool onlyOneAllowed;
    public float baseUpgradeTime;
    public float eachLevelTimeStep;
    public Animation upgradeAnimation;
}
