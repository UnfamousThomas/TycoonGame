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
       text.text = value + " $";
    }
}
