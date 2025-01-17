using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessCardPresenter : MonoBehaviour
{
    public BusinessData businessData;
    
    public TextMeshProUGUI nameText;
    public Image iconImage;

    public Button _button;
    public BusinessController businessController;
    public BuildingsMenuInfoPresenter buildingsMenuInfo;

    private void Awake()
    {
        _button = GetComponent<Button>();
        if (_button != null)
            _button.onClick.AddListener(Pressed);
    }

    private void Start()
    {
        buildingsMenuInfo.selectBusiness(businessData, false); //Just so correct data in the menu
    }

    private void Update()
    {
        if (businessData != null)
        {
            iconImage.sprite = businessData.icon;
            nameText.text = businessData.businessName;
        }
        

        if (businessController.isBusinessBuilt(businessData) &&
        businessData.onlyOneAllowed)
        {
            Destroy(gameObject);
        }
    }
    
    public void Pressed()
    {
        buildingsMenuInfo.selectBusiness(businessData, true);
    }

}