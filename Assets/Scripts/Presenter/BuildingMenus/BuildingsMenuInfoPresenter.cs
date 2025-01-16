using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class BuildingsMenuInfoPresenter : MonoBehaviour
{
    public Image icon;

    public TextMeshProUGUI businessName;
    public TextMeshProUGUI businessDescription;
    public TextMeshProUGUI businessBaseProd;

    public ResourceCostPresenter moneyPresenter;
    public ResourceCostPresenter rockPresenter;
    public ResourceCostPresenter ironPresenter;
    public ResourceCostPresenter goldPresenter;
    public ResourceCostPresenter waterPresenter;
    public ResourceCostPresenter oilPresenter;
    
    public Button buyButton;
    public Button exitButton;
    public RectTransform buildingsPanel;

    public BuildingsMenuOpenerCloser openerCloser;

    public AudioClipGroup clickGroup;
    private BusinessData _selectedBusiness;

    private void Awake()
    {
        buyButton.onClick.AddListener(BuyBusiness);
        exitButton.onClick.AddListener(Exit);
    }

    private void Update()
    {
        if(_selectedBusiness == null) return;
        if (!BusinessBuilder.CanBeBuilt(_selectedBusiness))
        {
            buyButton.interactable = false;
        }
        else
        {
            buyButton.interactable = true;
        }
    }

    private void Exit()
    {
        openerCloser.CloseMenu();
    }

    private void BuyBusiness()
    {
        Events.SelectBusiness(_selectedBusiness);
        Exit();
    }

    public void selectBusiness(BusinessData business, bool playAudio)
    {
        if (playAudio)
        {
            Events.PlayAudioClipGroup(clickGroup);
        }
        _selectedBusiness = business;
        icon.sprite = business.icon;
        businessDescription.text = business.description;
        businessName.text = business.businessName;
        businessBaseProd.text = business.baseProductionRate.ToString();
        UpdateResourcePresenters(business);
    }
    
    void UpdateResourcePresenters(BusinessData business)
    {
        goldPresenter.gameObject.SetActive(false);
        ironPresenter.gameObject.SetActive(false);
        moneyPresenter.gameObject.SetActive(false);
        waterPresenter.gameObject.SetActive(false);
        oilPresenter.gameObject.SetActive(false);
        rockPresenter.gameObject.SetActive(false);

        foreach (var costPair in business.cost)
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
                    rockPresenter.gameObject.SetActive(true);
                    rockPresenter.SetCost(costPair.value);
                    break;

                default:
                    Debug.LogWarning($"Unhandled resource type: {costPair.type}");
                    break;
            }
        }
    }
}
