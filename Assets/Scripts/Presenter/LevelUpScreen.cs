using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpScreen : MonoBehaviour
{
    private Business _business;
    public TextMeshProUGUI buildingName;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI moneyProductionText;
    public TextMeshProUGUI levelRequiredText;
    public Button levelUpButton;
    public Image levelUpImage;
    public Button exitButton;
    public Button sellButton;
    public Color allowedLevelup = Color.green;
    public Color notAllowedLevelup = Color.red;
    private void Awake()
    {
        Events.OnBusinessClicked += OnBusinessClick;
        Events.OnBusinessUpgraded += onBusinessUpgraded;
        Events.OnSetMoney += onMoneyUpdate;
        levelUpButton.onClick.AddListener(LevelUp);
        exitButton.onClick.AddListener(Exit);
        sellButton.onClick.AddListener(Sell);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnBusinessClicked -= OnBusinessClick;
    }
    

    private void OnBusinessClick(Business business)
    {
        _business = business;
        buildingName.text = business.businessData.businessName.ToUpper();
        
        if (business.isUpgradable())
        {
            levelUpButton.gameObject.SetActive(true);
        }
        else
        {
            levelUpButton.gameObject.SetActive(false);
        }
        
        gameObject.SetActive(true);
        Refresh();
    }
    
    

    private void LevelUp()
    {
        Events.UpgradeBusiness(_business);
    }
    

    private void Exit()
    {
        gameObject.SetActive(false);
    }

    private void Sell()
    {
        Events.SellBusiness(_business);
        Exit();
    }

    private void Refresh()
    {
        levelText.text = "LEVEL: " + _business.getLevel();
        moneyProductionText.text = _business.getCurrentProduction() + " $/s";
        levelRequiredText.text = "LEVEL UP: " + _business.calculateNextLevelCost() + " $";

        if (_business.businessData.businessName.ToLower() == "headquarters")
        {
            sellButton.gameObject.SetActive(false);
        }
        
        if (_business.calculateNextLevelCost() >= Events.RequestMoney())
        {
            levelUpButton.interactable = false;
            levelUpImage.color = notAllowedLevelup;
        }
        else
        {
            levelUpButton.interactable = true;
            levelUpImage.color = allowedLevelup;
        }
    }

    private void onBusinessUpgraded(Business business)
    {
        if (_business != null && business == _business)
        {
            Refresh();
        }
    }

    private void onMoneyUpdate(float money)
    {
        if(_business != null) {
            Refresh();
        }
    }
}