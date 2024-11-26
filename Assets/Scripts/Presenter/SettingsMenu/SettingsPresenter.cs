using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPresenter : MonoBehaviour
{
    public Button backButton;

    public Button audioButton;

    public RectTransform audioPanel;

    public String mainMenuScene;
    private void Awake()
    {
        backButton.onClick.AddListener(Back);
        audioButton.onClick.AddListener(AudioOpenClose);
    }
    
    void Back() {
        SceneManager.LoadScene(mainMenuScene);    
    }

    void AudioOpenClose()
    {
        audioPanel.gameObject.SetActive(!audioPanel.gameObject.activeSelf);
    }
}
