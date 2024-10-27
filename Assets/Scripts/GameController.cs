using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float initialMoney = 10;
    
    private float _money;
    private List<Business> _builtBusinesses = new();
    private float level = 0;
    private List<BusinessData> _businessData = new();
    
    private void Awake()
    {
        Events.OnSetMoney += OnSetMoney;
        Events.OnRequestMoney += OnGetMoney;
        Events.OnLevelChange += OnLevelCompleted;
        Events.OnGameCompleted += OnGameCompleted;
        Events.OnBusinessBuilt += OnBusinessBuilt;
        Events.OnBusinessUpgraded += onBusinessUpgraded;
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
        Events.OnLevelChange -= OnLevelCompleted;
        Events.OnGameCompleted -= OnGameCompleted;
        Events.OnBusinessBuilt -= OnBusinessBuilt;
        Events.OnBusinessUpgraded -= onBusinessUpgraded;
    }
    private void Update()
    {
        checkForClick();
    }

    private void checkForClick()
    {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePos, 0.5f);
            foreach (var collider in colliders)
            {
                Business business = collider.GetComponentInParent<Business>();
                if (business != null)
                {
                    Events.ClickBusiness(business, _money);
                    break;
                }
            }
        }
    }
    
    private void OnSetMoney(float money)
    {
        _money = money;
    }

    private float OnGetMoney()
    {
        return _money;
    }

    private void OnLevelCompleted(float level)
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
            money += business.getCurrentProduction();
        }
        Events.SetMoney(Events.RequestMoney() + money);
    }

    private void OnBusinessBuilt(Business business)
    {
        _builtBusinesses.Add(business);
        if (business.businessData.businessName == "Headquarters")
        {
            Events.SetLevel(1);
        }
        if (!_businessData.Contains(business.businessData))
        {
            _businessData.Add(business.businessData);
        }
    }

    private void onBusinessUpgraded(Business business)
    {
        if (business.businessData.businessName == "Headquarters")
        {
            Events.SetLevel(level+1);
        }
    }

    public bool isBusinessBuilt(BusinessData data)
    {
        return _businessData.Contains(data);
    }
}
