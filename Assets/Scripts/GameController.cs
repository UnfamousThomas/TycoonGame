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
        Events.OnBusinessBuilt += OnBusinessBuilt;
        Events.OnBusinessUpgraded += onBusinessUpgraded;
        Events.OnBusinessSold += OnBusinessSold;
        Events.OnLevelChange += onLevelChange;
        Events.OnRequestMoney += OnGetMoney;
    }

    public void Start()
    {
        Events.SetMoney(initialMoney);
        InvokeRepeating(nameof(AddMoney), 0, 1);
    }

    private void OnDestroy()
    {
        Events.OnSetMoney -= OnSetMoney;
        Events.OnBusinessBuilt -= OnBusinessBuilt;
        Events.OnBusinessUpgraded -= onBusinessUpgraded;
        Events.OnBusinessSold -= OnBusinessSold;
        Events.OnLevelChange -= onLevelChange;
        Events.OnRequestMoney -= OnGetMoney;
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
                    Events.ClickBusiness(business);
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
        if (business.businessData.businessName == "Headquarters") //todo same as below
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
        if (business.businessData.businessName == "Headquarters") //TODO needs better logic probably?
        {
            Events.SetLevel(level+1);
        }
        Events.SetMoney(Events.RequestMoney() - business.calculateNextLevelCost());
    }

    private void OnBusinessSold(Business business)
    {
        _builtBusinesses.Remove(business);
        _businessData.Remove(business.businessData);
        Destroy(business.gameObject);
        
        Events.SetMoney(Events.RequestMoney() + business.businessData.cost);
    }

    private void onLevelChange(float level)
    {
        this.level = level;
    }

    public bool isBusinessBuilt(BusinessData data)
    {
        return _businessData.Contains(data);
    }
}
