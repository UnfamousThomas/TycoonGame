using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessCardPresenter : MonoBehaviour
{
    public BusinessData businessData;
    
    public TextMeshProUGUI nameText;
    public Image iconImage;

    private Button _button;
    public BusinessController businessController;

    private void Awake()
    {
        _button = GetComponent<Button>();
        if (_button != null)
            _button.onClick.AddListener(Pressed);
    }

    private void Update()
    {
        if (businessData != null)
        {
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

        if (businessController.isBusinessBuilt(businessData) &&
        businessData.onlyOneAllowed)
        {
            Destroy(gameObject);
        }
    }
    
    public void Pressed()
    {
        //Events.SelectBusiness(businessData);
    }

}