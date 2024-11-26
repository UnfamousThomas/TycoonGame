using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPresenter : MonoBehaviour
{
    public Button playButton;

    public Button settingsButton;
    public Button exitButton;
    public string mainSceneName;
    public string settingsSceneName;
    public SettingsContainer settingsContainer;
    
    private void Awake()
    {
        exitButton.onClick.AddListener(exitGame);
        playButton.onClick.AddListener(startGame);
        settingsButton.onClick.AddListener(openSettings);
    }

    private void Update()
    {
        Debug.Log(settingsContainer.audioTypeSettings[0].audioType.ToString() + ": " + settingsContainer.audioTypeSettings[0].Volume);
    }

    void exitGame()
    {
        Application.Quit();
    }

    void startGame()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    void openSettings()
    {
        SceneManager.LoadScene(settingsSceneName);
    }
}
