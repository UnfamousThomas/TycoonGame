using System;
using System.Collections;
using System.Collections.Generic;
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
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {

        if (!isSwitching)
        {
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
}
