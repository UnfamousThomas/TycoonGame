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
    public ScalingAnimation openAnimation;
    public ScalingAnimation closeAnimation;
    public AudioClipGroup clickGroup;

    private void Awake()
    {
        Events.OnBusinessClicked += OnBusinessClick;
        Events.OnBusinessUpgradedFinish += onBusinessUpgraded;
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
        openAnimation.enabled = true;
        Events.PlayAudioClipGroup(clickGroup);
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
        Events.PlayAudioClipGroup(clickGroup);
        Events.StartUpgradeBusiness(_business);
    }
    

    private void Exit()
    {
        Events.PlayAudioClipGroup(clickGroup);
        closeAnimation.enabled = true;
    }

    private void Sell()
    {
        Events.SellBusiness(_business);
        Events.PlayAudioClipGroup(clickGroup);
        Exit();
    }

    private void Refresh()
    {
        levelText.text = "LEVEL: " + _business.getLevel();
        moneyProductionText.text = _business.getCurrentProduction() + " $/s";
        levelRequiredText.text = "LEVEL UP: " + System.Environment.NewLine + _business.GetCostText();

        if (_business.businessData.businessName.ToLower() == "headquarters")
        {
            sellButton.gameObject.SetActive(false);
        }
        else
        {
            sellButton.gameObject.SetActive(true);
        }
        
        if (!_business.CanBeUpgraded())
        {
            levelUpButton.interactable = false;
            levelUpImage.color = notAllowedLevelup;
        }
        if (_business.isBeingUpgraded() && _business.upgradeTimeLeft > 0)
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
    
    public void CloseFinished()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButton("ExitMenu"))
        {
            Exit();
        }
    }
}