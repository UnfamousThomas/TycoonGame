using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInfoPresenter : MonoBehaviour
{
    
    public Image buildingImage;
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI level;
    public Button exitButton;
    public Button sellButton;
    public Button upgradeButton;
    public RectTransform upgradePanel;
    public TextMeshProUGUI upgradeTime;

    public ResourceCostPresenter goldPresenter;
    public ResourceCostPresenter ironPresenter;
    public ResourceCostPresenter moneyPresenter;
    public ResourceCostPresenter waterPresenter;
    public ResourceCostPresenter oilPresenter;
    public ResourceCostPresenter stonePresenter;

    public ScalingAnimation openAnimation;
    public ScalingAnimation closeAnimation;
    public AudioClipGroup clickGroup;
    
    private Business _selectedBusiness;
    private void Awake()
    {
        Events.OnBusinessClicked += OnBusinessClicked;
        Events.OnBusinessUpgradedFinish += onBusinessUpgraded;
        Events.OnSetMoney += onResourceUpdate;
        Events.OnSetGold += onResourceUpdate;
        Events.OnSetIron += onResourceUpdate;
        Events.OnSetOil += onResourceUpdate;
        Events.OnSetWater += onResourceUpdate;
        Events.OnSetRocks += onResourceUpdate;
        
        exitButton.onClick.AddListener(OnExitClicked);
        sellButton.onClick.AddListener(Sell);
        goldPresenter.gameObject.SetActive(false);
        ironPresenter.gameObject.SetActive(false);
        moneyPresenter.gameObject.SetActive(false);
        oilPresenter.gameObject.SetActive(false);
        stonePresenter.gameObject.SetActive(false);
        waterPresenter.gameObject.SetActive(false);
        
        upgradePanel.gameObject.SetActive(false);
    }
    

    private void OnDestroy()
    {
        Events.OnBusinessUpgradedFinish -= onBusinessUpgraded;
        Events.OnBusinessClicked -= OnBusinessClicked;
        Events.OnSetMoney -= onResourceUpdate;
        Events.OnSetGold -= onResourceUpdate;
        Events.OnSetIron -= onResourceUpdate;
        Events.OnSetOil -= onResourceUpdate;
        Events.OnSetWater -= onResourceUpdate;
        Events.OnSetRocks -= onResourceUpdate;
    }
    

    private void OnBusinessClicked(Business business)
    {
        _selectedBusiness = business;
        BusinessData businessData = business.businessData;
        buildingImage.sprite = businessData.icon;
        buildingName.text = businessData.name;
        level.text = "Level: " + business.getLevel();
        UpdateResourcePresenters(business);
        upgradeTime.text = FormatTime(business.calculateNextUpgradeTime());
        openAnimation.enabled = true;
        
        CheckIfUpgrade(business);
    }
    
    private string FormatTime(float totalSeconds)
    {
        float days = totalSeconds / (24 * 3600);
        totalSeconds %= 24 * 3600;
        float hours = totalSeconds / 3600;
        totalSeconds %= 3600;
        float minutes = totalSeconds / 60;
        float seconds = totalSeconds % 60;

        string formattedTime = "";
        if (days > 0)
            formattedTime += days + "d";
        if (hours > 0)
            formattedTime += hours + "h";
        if (minutes > 0)
            formattedTime += minutes + "m";
        if (seconds > 0 || formattedTime == "") 
            formattedTime += seconds + "s";

        return formattedTime;
    }
    private void onBusinessUpgraded(Business business)
    {
        if (_selectedBusiness != null && business == _selectedBusiness)
        {
            CheckIfUpgrade(business);
            UpdateResourcePresenters(business);
        }
    }
    
    private void onResourceUpdate(float value)
    {
        if(_selectedBusiness != null) {
            CheckIfUpgrade(_selectedBusiness);
        }
    }

    private void Sell()
    {
        Events.SellBusiness(_selectedBusiness);
        Events.PlayAudioClipGroup(clickGroup);
        Exit();
    }

    private void OnExitClicked()
    {
        Events.PlayAudioClipGroup(clickGroup);
        Exit();
    }
    private void Exit()
    {
        closeAnimation.enabled = true;
    }
    
    private void CheckIfUpgrade(Business business)
    {
        if (business.CanBeUpgraded())
        {
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
        }
        
        if (business.isUpgradable())
        {
            upgradeButton.gameObject.SetActive(true); 
        }
        else
        {
            upgradeButton.gameObject.SetActive(false); 
        }

        if (business.upgradeTimeLeft > 0)
        {
            upgradeButton.interactable = false;
        }

        if (!upgradeButton.interactable)
        {
            upgradeButton.image.color = Color.red;
        }
        else
        {
            upgradeButton.image.color = Color.white;
        }
    }

    void UpdateResourcePresenters(Business business)
    {
        goldPresenter.gameObject.SetActive(false);
        ironPresenter.gameObject.SetActive(false);
        moneyPresenter.gameObject.SetActive(false);
        waterPresenter.gameObject.SetActive(false);
        oilPresenter.gameObject.SetActive(false);
        stonePresenter.gameObject.SetActive(false);

        foreach (var costPair in business.calculateNextLevelCost())
        {
            switch (costPair.type)
            {
                case ResourceType.MONEY:
                    moneyPresenter.gameObject.SetActive(true);
                    moneyPresenter.SetCost(costPair.value);
                    break;

                case ResourceType.GOLD:
                    goldPresenter.gameObject.SetActive(true);
                    goldPresenter.SetCost(costPair.value);
                    break;

                case ResourceType.IRON:
                    ironPresenter.gameObject.SetActive(true);
                    ironPresenter.SetCost(costPair.value);
                    break;

                case ResourceType.WATER:
                    waterPresenter.gameObject.SetActive(true);
                    waterPresenter.SetCost(costPair.value);
                    break;

                case ResourceType.OIL:
                    oilPresenter.gameObject.SetActive(true);
                    oilPresenter.SetCost(costPair.value);
                    break;

                case ResourceType.ROCK:
                    stonePresenter.gameObject.SetActive(true);
                    stonePresenter.SetCost(costPair.value);
                    break;

                default:
                    Debug.LogWarning($"Unhandled resource type: {costPair.type}");
                    break;
            }
        }
    }
}
