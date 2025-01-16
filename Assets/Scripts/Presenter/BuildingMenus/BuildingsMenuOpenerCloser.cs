using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsMenuOpenerCloser : MonoBehaviour
{
    public ScalingAnimation openingAnimation;
    public ScalingAnimation closingAnimation;
    public AudioClipGroup clickGroup;
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        Events.PlayAudioClipGroup(clickGroup);
        closingAnimation.enabled = false;
        openingAnimation.enabled = true;
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        Events.PlayAudioClipGroup(clickGroup);
        openingAnimation.enabled = false;
        closingAnimation.enabled = true;
        closingAnimation.animationFinishEvent.AddListener(SetInactive);
    }

    private void SetInactive()
    {
        if (!openingAnimation.enabled)
        {
            gameObject.SetActive(false);
        }
    }
}
