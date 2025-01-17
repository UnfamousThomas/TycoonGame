using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public SwitchingSpriteImage plusButton;

    public RectTransform basePanel;
    public TextMeshProUGUI text;

    public Button closeWelcomeButton;
    
    public AudioClipGroup clickSoundGroup;

    private bool _solarPanelBuilt;
    private bool _headquartersBuilt;
    private bool _buildMenuFirstOpened;
    private bool _upgradePanelSeen;
    private bool tutorialFinished = false;
    private bool didTutorial = false;
    private bool _minerBuilt;
    private bool _minerUpgraded;

    private void Awake()
    {
        SaveData save = SaveSystem.Load();
        if (save == null)
        {
            tutorialFinished = false;
        } else {
            tutorialFinished = save.tutorialFinished;
        }
        didTutorial = !tutorialFinished;
        if (didTutorial)
        {
            closeWelcomeButton.onClick.AddListener(ClosePanel);
            Button clickButton = plusButton.getButton();
            if (clickButton != null)
            {
                clickButton.onClick.AddListener(OpenedBuildMenu);
            }

            Events.OnBusinessBuilt += onBusinessBuilt;
            Events.OnBusinessClicked += onBusinessSelected;
            Events.OnBusinessUpgradedStart += onBusinessUpgrade;
        }

        Events.onTutorialFinishedRequest += isFinished;
        Events.OnSetTutorialFinished += setTutorialFinished;
    }

    private bool isFinished()
    {
        return tutorialFinished;
    }
    
    public void setTutorialFinished(bool value)
    {
        tutorialFinished = value;
    }

    private void OnDestroy()
    {
        if(didTutorial) {
            Events.OnBusinessBuilt -= onBusinessBuilt;
            Events.OnBusinessClicked -= onBusinessSelected;
            Events.OnBusinessUpgradedStart -= onBusinessUpgrade;
        }
        Events.onTutorialFinishedRequest -= isFinished;
        Events.OnSetTutorialFinished -= setTutorialFinished;
    }

    private void OpenedBuildMenu()
    {
        if(!_buildMenuFirstOpened) {
            text.text =
                "Here you can view and buy different buildings, which earn you resources. Some of them earn money, some of them can only be placed on a certain resource. Feel free to explore later! For now though, please build a HQ.";
            basePanel.gameObject.SetActive(true);
        }
        _buildMenuFirstOpened = true;
    }

    private void onBusinessSelected(Business business)
    {
        if(_solarPanelBuilt && business.businessData.businessName == "Solar Panel" && !_upgradePanelSeen) {
            text.text =
                "Here you can view different statistics about your building, such as level." +
                " As well as what you need to upgrade it and how long it would take. Note that production of resources for said building " +
                "is halted during the upgrade. Now, exit out of the details menu and build a miner on some rocks you can find on the map.";
            basePanel.gameObject.SetActive(true);
            _upgradePanelSeen = true;
        }
    }

    private void ClosePanel()
    {
        basePanel.gameObject.SetActive(false);
        Events.PlayAudioClipGroup(clickSoundGroup);
    }

    private void Start()
    {
        if(!tutorialFinished) {
            plusButton.Enable();
        }
        else
        {
            basePanel.gameObject.SetActive(false);
        }
    }

    public void onBusinessBuilt(Business business)
    {
        if (business.businessData.businessName == "Headquarters")
        {
            headquartersBuilt(business);
        }

        if (business.businessData.businessName == "Solar Panel" && !_solarPanelBuilt && _headquartersBuilt)
        {
            solarPanelBuilt(business);
        }
        if (business.businessData.businessName == "Miner" && _solarPanelBuilt && _headquartersBuilt && _upgradePanelSeen && !_minerBuilt)
        {
            minerBuilt(business);
        }
    }

    public void onBusinessUpgrade(Business business)
    {
        if (business.businessData.businessName == "Miner" && !_solarPanelBuilt && _headquartersBuilt && _upgradePanelSeen && _minerBuilt && !_minerUpgraded)
        {
            minerUpgrade(business);
        }
    }

    private void minerUpgrade(Business business)
    {
        text.text =
            "Congratulations! Your miner is now upgrading, you can click on it to learn when it is ready. From now on you are on your own, as I go have to go see my space family. Good luck!";
        _minerUpgraded = true;
        tutorialFinished = true;
    }

    private void minerBuilt(Business business)
    {
        text.text =
            "Congratulations! You have build your third business, a miner. As you might have seen, this can only be placed on certain" +
             " resource nodes that can be found in the world: STONE, IRON, GOLD nodes. Oil nodes use a oil refinery and water nodes use a " +
             "water pump. Now, try to upgrade the miner (once you have enough resources)!";
        basePanel.gameObject.SetActive(true);
        _minerBuilt = true;
    }

    private void solarPanelBuilt(Business business)
    {
        text.text =
            "Congratulations! You have build your second business, a solar panel. Please click on it to view information.";
        basePanel.gameObject.SetActive(true);
        _solarPanelBuilt = true;
    }

    private void headquartersBuilt(Business business)
    {
        text.text =
            "Congratulations! You have build your first business, the headquarters." +
            "You will be building other buildings in the future." +
            "For now, please build a solar panel.";
        basePanel.gameObject.SetActive(true);
        _headquartersBuilt = true;
    }
}
