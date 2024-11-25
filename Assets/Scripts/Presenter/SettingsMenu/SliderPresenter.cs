using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderPresenter : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI valueText;

    // Update is called once per frame
    void Update()
    {
        valueText.text = slider.value.ToString();
    }
}
