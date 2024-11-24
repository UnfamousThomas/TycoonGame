using System;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{

    private float _money = 0;
    public float initialMoney = 10;
    private void Awake()
    {
        Events.OnSetMoney += SetMoney;
        Events.OnRequestMoney += getMoney;
    }
    
    private void OnDestroy()
    {
        Events.OnSetMoney -= SetMoney;
        Events.OnRequestMoney -= getMoney;
    }
    

    private void SetMoney(float money)
    {
        _money = money;
    }

    private float getMoney()
    {
        return _money;
    }
    
}
