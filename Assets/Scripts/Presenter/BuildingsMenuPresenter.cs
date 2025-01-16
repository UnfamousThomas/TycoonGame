using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsMenuPresenter : MonoBehaviour
{
    private Button _button;
    public GameObject panel;

    private void Awake()
    {
        _button = GetComponent<Button>();
        if (_button != null)
        {
            _button.onClick.AddListener(Pressed);    
        }
    }

    public void Pressed()
    {
        panel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetButton("Build"))
        {
                panel.SetActive(true);
        }
        
    }
}
