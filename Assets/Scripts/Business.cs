using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Business : MonoBehaviour
{
    public BusinessData businessData;

    public float CurrentMoneyProduction = 1;

    private void Awake()
    {
        Events.OnBusinessUpgraded += OnBusinessUpgraded;
    }

    private void OnDestroy()
    {
        Events.OnBusinessUpgraded -= OnBusinessUpgraded;
    }

    private void OnBusinessUpgraded(Business business)
    {
        CurrentMoneyProduction += businessData.moneyProductionStep;
    }
}
