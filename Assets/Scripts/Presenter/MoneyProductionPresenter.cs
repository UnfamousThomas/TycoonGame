using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyProductionPresenter : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Awake()
    {
        Events.OnSetMoneyProduction += OnSetMoneyProduction;
    }

    private void OnDestroy()
    {
        Events.OnSetMoneyProduction -= OnSetMoneyProduction;
    }

    public void OnSetMoneyProduction(float value)
    {
       text.text = value + " $/s";
    }
}
