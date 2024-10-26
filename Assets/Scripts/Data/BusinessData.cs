using UnityEngine;

[CreateAssetMenu(menuName = "SpaceTycoon/Business")]
public class BusinessData : ScriptableObject
{
    public string businessName;
    public float baseCost;
    public float costMultiplier;
    public int amountOfUpgrades;
    public float baseGoldProduction;
    public float goldProductionMultiplier;
    public Business businessPrefab;
}
