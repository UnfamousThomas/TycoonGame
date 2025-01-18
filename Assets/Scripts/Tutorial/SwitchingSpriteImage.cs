using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SwitchingSpriteImage : MonoBehaviour
{
    private Image _image;
    public float pulseSwitchTime = 1;

    public Sprite spriteOne;
    public Sprite spriteTwo;

    private float _timeSinceLastPulse = 0;
    public bool isSwitching = false;

    private Button _button;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        if (_button != null)
            _button.onClick.AddListener(ClickedImageButton);
    }

    private void ClickedImageButton()
    {
        Disable();
    }

    public Button getButton()
    {
        return _button;
    }


    void Update()
    {

        if (!isSwitching)
        {
            Debug.Log("disable");
            _image.sprite = spriteOne;
            return;
        }

        _timeSinceLastPulse += Time.deltaTime;
        if (_timeSinceLastPulse >= pulseSwitchTime)
        {
            _timeSinceLastPulse = 0;
            if (_image.sprite == spriteTwo)
            {
                _image.sprite = spriteOne;
            }
            else
            {
                _image.sprite = spriteTwo;
            }
        }
    }

    public void Enable()
    {
        isSwitching = true;
    }

    public void Disable()
    {
        isSwitching = false;
    }
}
