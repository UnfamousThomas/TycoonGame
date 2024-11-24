using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BusinessController : MonoBehaviour
{
    
    private List<Business> _builtBusinesses = new();
    private List<BusinessData> _businessData = new();
    
    private void Awake()
    {
        Events.OnBusinessBuilt += OnBusinessBuilt;
        Events.OnBusinessUpgradedFinish += OnBusinessUpgradedFinish;
        Events.OnBusinessUpgradedFinish += OnBusinessUpgradedFinish;
        Events.OnBusinessSold += OnBusinessSold;
        Events.onLoadedBusinesses += onLoad;
        Events.OnRequestBusinesses += getBusinesses;
    }

    private void OnDestroy()
    {
        Events.OnBusinessBuilt -= OnBusinessBuilt;
        Events.OnBusinessUpgradedFinish -= OnBusinessUpgradedFinish;
        Events.OnBusinessUpgradedFinish -= OnBusinessUpgradedFinish;
        Events.OnBusinessSold -= OnBusinessSold;
        Events.onLoadedBusinesses -= onLoad;
        Events.OnRequestBusinesses -= getBusinesses;
    }

    private void Start()
    {
        InvokeRepeating(nameof(AddMoney), 0, 1);
    }


    private void Update()
    {
        checkForClick();
    }

    public void onLoad(List<Business> businesses)
    {
        foreach (var business in businesses)
        {
            _builtBusinesses.Add(business);
            _businessData.Add(business.businessData);
        }
    }

    private void checkForClick()
    {
        if(Input.GetMouseButtonDown(0)) {
                if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                {
                    //Ignore clicks that are inside a UI overlay.
                    return;
                }
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

    private void OnBusinessUpgradedFinish(Business business)
    {
        if (business.businessData.businessName == "Headquarters") //TODO needs better logic probably?
        {
            Events.SetLevel(Events.RequestLevel()+1);
        }
        Events.SetMoney(Events.RequestMoney() - business.calculateNextLevelCost());
    }
    

    private void OnBusinessSold(Business business)
    {
        _builtBusinesses.Remove(business);
        _businessData.Remove(business.businessData);
        Destroy(business.gameObject);
        
        Events.SetMoney(
            Events.RequestMoney() + business.businessData.cost * business.businessData.sellingPriceMultiplier
        );
    }

    public bool isBusinessBuilt(BusinessData data)
    {
        return _businessData.Contains(data);
    }

    private List<Business> getBusinesses()
    {
        return _builtBusinesses;
    }
    
    private void AddMoney()
    {
        float money = 0;
        foreach (Business business in _builtBusinesses)
        { 
            money += business.getCurrentProduction();
        }
        Events.SetMoney(Events.RequestMoney() + money);
        Events.SetMoneyProduction(money);
    }

    
}
