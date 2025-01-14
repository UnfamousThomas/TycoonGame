using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessCardPresenter : MonoBehaviour
{
    public BusinessData businessData;
    
    public TextMeshProUGUI costText;
    public TextMeshProUGUI nameText;
    public Image iconImage;
    public GameObject panel;

    private Button _button;
    public BusinessController BusinessController;

    private void Awake()
    {
        _button = GetComponent<Button>();
        if (_button != null)
            _button.onClick.AddListener(Pressed);

        if (businessData != null)
        {
            costText.text = businessData.GetCostText();
            iconImage.sprite = businessData.icon;
            nameText.text = businessData.businessName;
        }

        if (!BusinessBuilder.CanBeBuilt(businessData))
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }

        Events.OnSetMoney += OnSetMoney;
    }

    private void Update()
    {
        if (BusinessController.isBusinessBuilt(businessData) &&
            businessData.onlyOneAllowed)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Events.OnSetMoney -= OnSetMoney;
    }

    public void Pressed()
    {
        panel.SetActive(false);
        Events.SelectBusiness(businessData);
    }

    //TODO fix if this causes lag because the logic was moved to Update().
    public void OnSetMoney(float value)
    {
        // if (value < businessData.cost)
        // {
        //     _button.interactable = false;
        // }
        // else
        // {
        //     _button.interactable = true;
        // }
    }
}
