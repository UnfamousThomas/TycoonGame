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
    public Button sellButton;
    public Color allowedLevelup = Color.green;
    public Color notAllowedLevelup = Color.red;
    private void Awake()
    {
        Events.OnBusinessClicked += OnBusinessClick;
        levelUpButton.onClick.AddListener(LevelUp);
        exitButton.onClick.AddListener(Exit);
        sellButton.onClick.AddListener(Sell);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnBusinessClicked -= OnBusinessClick;
    }

    private void Update()
    {
        Refresh();
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
}