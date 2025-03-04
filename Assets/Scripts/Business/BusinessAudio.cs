using System;
using UnityEngine;

public class BusinessAudio: MonoBehaviour
{
    public AudioClipGroup upgradeGroup;
    public AudioClipGroup buildingGroup;

    private void Awake()
    {
        Events.OnBusinessUpgradedFinish += OnBusinessUpgrade;
        Events.OnBusinessBuilt += onBusinessBuilt;
    }

    private void OnDestroy()
    {
        Events.OnBusinessUpgradedFinish -= OnBusinessUpgrade;
        Events.OnBusinessBuilt -= onBusinessBuilt;
    }

    void OnBusinessUpgrade(Business business)
    {
        if (business.businessData.businessName == "Headquarters")
        {
            Events.PlayAudioClipGroup(upgradeGroup);
        }
    }

    void onBusinessBuilt(Business business)
    {
        Events.PlayAudioClipGroup(buildingGroup);
        Debug.Log("build");
    }
}