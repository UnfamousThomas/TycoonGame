using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpScreen : MonoBehaviour
{
    private Business _business;
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI levelText;
    public Button levelUpButton;
    public Image levelUpImage;
    public Button exitButton;
    public Color allowedLevelup = Color.green;
    public Color notAllowedLevelup = Color.red;
    private void Awake()
    {
        Events.OnBusinessClicked += onBusinessClick;
        levelUpButton.onClick.AddListener(click);
        exitButton.onClick.AddListener(exit);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnBusinessClicked -= onBusinessClick;
    }

    private void onBusinessClick(Business business)
    {
        _business = business;
        buildingName.text = business.businessData.businessName.ToUpper();
        levelText.text = "LEVEL: " + business.getLevel().ToString();
        if (business.isUpgradable())
        {
            levelUpButton.gameObject.SetActive(true);
        }
        else
        {
            levelUpButton.gameObject.SetActive(false);
        }

        if (business.calculateNextLevelCost() >= Events.RequestMoney())
        {
            levelUpButton.interactable = false;
            levelUpImage.color = notAllowedLevelup;
        }
        else
        {
            levelUpButton.interactable = true;
            levelUpImage.color = allowedLevelup;
        }
        
        gameObject.SetActive(true);
    }
    
    

    private void click()
    {
        Events.FinishUpgradeBusiness(_business);
        exit();
    }
    

    private void exit()
    {
        gameObject.SetActive(false);
    }
}