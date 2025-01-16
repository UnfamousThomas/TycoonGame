using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string mainMenuSceneName;
    public GameObject businessBuilder;
    public GameObject chooseBuildings;
    public GameObject levelUpScreen;
    
    private void Update()
    {
        if (Input.GetButton("ExitMenu") && !IsMenuOpen())
        {
            SaveSystem.Save();
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }

    private bool IsMenuOpen()
    {
        return businessBuilder.activeSelf
               || chooseBuildings.activeSelf
               || levelUpScreen.activeSelf;
    }
}
