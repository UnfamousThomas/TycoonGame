using UnityEngine;

[CreateAssetMenu(menuName = "SpaceTycoon/Business")]
public class BusinessData : ScriptableObject
{
    public string businessName;
    public float cost;
    public float baseUpgradeCost;
    public float upgradeCostMultiplier;
    public int amountOfUpgrades;
    public float baseGoldProduction;
    public float goldProductionMultiplier;
    public Business businessPrefab;
    public Sprite icon;
}
