using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float initialMoney = 10;
    
    private float _money;
    private List<Business> _builtBusinesses = new();
    
    private void Awake()
    {
        Events.OnSetMoney += OnSetMoney;
        Events.OnRequestMoney += OnGetMoney;
        Events.OnLevelCompleted += OnLevelCompleted;
        Events.OnGameCompleted += OnGameCompleted;
        Events.OnBusinessBuilt += OnBusinessBuilt;

    }

    public void Start()
    {
        Events.SetMoney(initialMoney);
        InvokeRepeating(nameof(AddMoney), 0, 1);
    }

    private void OnDestroy()
    {
        Events.OnSetMoney -= OnSetMoney;
        Events.OnRequestMoney -= OnGetMoney;
        Events.OnLevelCompleted -= OnLevelCompleted;
        Events.OnGameCompleted -= OnGameCompleted;
        Events.OnBusinessBuilt -= OnBusinessBuilt;
    }

    private void OnSetMoney(float money)
    {
        _money = money;
    }

    private float OnGetMoney()
    {
        return _money;
    }

    private void OnLevelCompleted()
    {
        // TODO next level menu, if there are no levels left, make GameCompleted display something
    }

    private void OnGameCompleted()
    {
        // TODO display end screen that says for example "Game Won! Completed all levels!"
    }

    private void AddMoney()
    {
        float money = 0;
        foreach (Business business in _builtBusinesses)
        { 
            money += business.CurrentMoneyProduction;
        }
        Events.SetMoney(Events.RequestMoney() + money);
    }

    private void OnBusinessBuilt(Business business)
    {
        _builtBusinesses.Add(business);
    }
}
