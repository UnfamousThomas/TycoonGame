using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string mainMenuSceneName;
    
    private void Update()
    {
        if (Input.GetButton("ExitMenu"))
        {
            //SaveSystem.Save();
            //SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
