using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPresenter : MonoBehaviour
{
    public Button exitButton;
    public float maxLevel = 5f;
    private void Awake()
    {
        exitButton.onClick.AddListener(onClick);
        Events.OnLevelChange += onLevelChange;
        gameObject.SetActive(false);
    }

    void onClick()
    {
        gameObject.SetActive(false);
    }

    void onLevelChange(float level)
    {
        if (level == maxLevel)
        {
            gameObject.SetActive(true);
        } 
    }
}
