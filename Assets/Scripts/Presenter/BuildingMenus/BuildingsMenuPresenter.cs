using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsMenuPresenter : MonoBehaviour
{
    private Button _button;
    public BuildingsMenuOpenerCloser openerCloser;

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
        openerCloser.OpenMenu();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Build"))
        {
            Pressed();
        }

        if (Input.GetButton("ExitMenu"))
        {
            openerCloser.CloseMenu();
        }
    }
}
