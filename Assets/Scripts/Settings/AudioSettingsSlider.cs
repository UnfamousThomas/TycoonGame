using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsSlider : MonoBehaviour
{
    public AudioTypeSetting audiosetting;
    public TextMeshProUGUI text;
    public Slider slider;
    public float initialValue;
    void Start()
    {
        slider.value = initialValue;
        text.text = audiosetting.audioType.ToString().ToUpper();
    }

    private void Update()
    {
        audiosetting.Volume = slider.value / 100;
    }
}
