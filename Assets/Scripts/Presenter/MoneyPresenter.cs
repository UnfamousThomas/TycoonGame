using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPresenter : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Awake()
    {
        Events.OnSetMoney += OnSetMoney;
    }

    private void OnDestroy()
    {
        Events.OnSetMoney -= OnSetMoney;
    }

    public void OnSetMoney(float value)
    {
       text.text = FormatMoney(value) + " $";
    }
    
    private string FormatMoney(float value)
    {
        if (value >= 1_000_000_000)
            return (value / 1_000_000_000f).ToString("0.##") + " bln";
        else if (value >= 1_000_000)
            return (value / 1_000_000f).ToString("0.##") + " mln";
        else if (value >= 1_000)
            return (value / 1_000f).ToString("0.##") + "k";
        else
            return value.ToString("0.##");
    }
}
