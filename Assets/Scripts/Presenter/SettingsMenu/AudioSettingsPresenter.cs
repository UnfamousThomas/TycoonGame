using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettingsPresenter : MonoBehaviour
{
    public SettingsContainer SettingsContainer;

    public AudioSettingsSlider AudioSettingsPrefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var setting in SettingsContainer.audioTypeSettings)
        {
            AudioSettingsSlider slider = Instantiate(AudioSettingsPrefab, transform, this);
            slider.initialValue = setting.Volume * 100;
            slider.audiosetting = setting;
            
        }
    }
}
