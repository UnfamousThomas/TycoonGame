using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchingColorImage : MonoBehaviour
{
    private Image _image;
    public float pulseSwitchTime = 1;

    public Color colorOne = Color.white;
    public Color colorTwo = Color.blue;
    
    private float _timeSinceLastPulse = 0;
    public bool isGlowing = false;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {

        if (!isGlowing)
        {
            _image.color = colorOne;
            return;
        }

        _timeSinceLastPulse += Time.deltaTime;
        if (_timeSinceLastPulse >= pulseSwitchTime)
        {
            _timeSinceLastPulse = 0;
            if (_image.color == colorTwo)
            {
                _image.color = colorOne;
            }
            else
            {
                _image.color = colorTwo;
            }
        }
    }
}
