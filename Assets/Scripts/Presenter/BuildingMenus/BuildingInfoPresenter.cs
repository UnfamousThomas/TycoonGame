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
    public TextMeshProUGUI upgradeTime;
    public TextMeshProUGUI levelText;
    public GameObject upgradeTimeLeftComponent;
    public TextMeshProUGUI upgradeTimeLeftText;
    public GameObject upgradeSection;
    public GameObject maxLevelReached;

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
        upgradeButton.onClick.AddListener(LevelUp);
        goldPresenter.gameObject.SetActive(false);
        ironPresenter.gameObject.SetActive(false);
        moneyPresenter.gameObject.SetActive(false);
        oilPresenter.gameObject.SetActive(false);
        stonePresenter.gameObject.SetActive(false);
        waterPresenter.gameObject.SetActive(false);
        gameObject.SetActive(false);

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
        gameObject.SetActive(true);
    }
    
    private void LevelUp()
    {
        Events.PlayAudioClipGroup(clickGroup);
        Events.StartUpgradeBusiness(_selectedBusiness);
    }
    
    private string FormatTime(float totalSecondsFloat)
    {
        int totalSeconds = (int)totalSecondsFloat;
        TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
        
        string result = "";

        if (timeSpan.Days > 0)
            result += $"{timeSpan.Days}d ";

        if (timeSpan.Hours > 0 || timeSpan.Days > 0)
            result += $"{timeSpan.Hours}h ";

        if (timeSpan.Minutes > 0 || timeSpan.Hours > 0 || timeSpan.Days > 0) // Display minutes if hours or days are displayed
            result += $"{timeSpan.Minutes}m ";

        result += $"{timeSpan.Seconds}s";

        return result.Trim();
    }
    private void onBusinessUpgraded(Business business)
    {
        if (_selectedBusiness != null && business == _selectedBusiness)
        {
            CheckIfUpgrade(business);
            UpdateResourcePresenters(business);
            UpdateLevel(business);
            UpdateNextTime(business);
        }
    }

    private void UpdateNextTime(Business business)
    {
        upgradeTime.text = FormatTime(business.calculateNextUpgradeTime());
    }

    private void UpdateLevel(Business business)
    {
        levelText.text = "Level: " + (business.getLevel() + 1);
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
        if ("Headquarters".Equals(buildingName.text))
            sellButton.gameObject.SetActive(false);
        else
        {
            sellButton.gameObject.SetActive(true);
        }
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
            upgradeSection.gameObject.SetActive(true);
            maxLevelReached.gameObject.SetActive(false);
        }
        else
        {
            upgradeButton.gameObject.SetActive(false);
            upgradeSection.gameObject.SetActive(false);
            maxLevelReached.gameObject.SetActive(true);
            upgradeTimeLeftComponent.gameObject.SetActive(false);
            return;
        }

        if (business.upgradeTimeLeft > 0)
        {
            upgradeSection.gameObject.SetActive(false);
            upgradeTimeLeftText.text = "Upgrade Time Left: " + FormatTime(business.upgradeTimeLeft);
            upgradeTimeLeftComponent.gameObject.SetActive(true);
        }
        else
        {
            upgradeTimeLeftComponent.gameObject.SetActive(false);
            upgradeSection.gameObject.SetActive(true);
        }

        if (!upgradeButton.interactable)
        {
            upgradeButton.image.color = Color.red;
        }
        else
        {
            upgradeButton.image.color = Color.green;
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
    
    private void Update()
    {
        if (Input.GetButton("ExitMenu"))
        {
            Exit();
        }
    }

    public void CloseFinished()
    {
        if (!openAnimation.enabled)
            gameObject.SetActive(false);
    }
}
