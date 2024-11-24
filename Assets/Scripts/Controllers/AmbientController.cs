using System;
using System.Collections.Generic;
using UnityEngine;

public class AmbientController : MonoBehaviour
{

    public AudioClipGroup ambientMusic;
    public float delayBetweenInSeconds = 6 * 60;
    private float _lastPlay = 0;

    private void Update()
    {
        if (Time.time - _lastPlay > delayBetweenInSeconds || _lastPlay <= 0)
        {
            _lastPlay = Time.time;
            Events.PlayAudioClipGroup(ambientMusic);
        }
    }
}
