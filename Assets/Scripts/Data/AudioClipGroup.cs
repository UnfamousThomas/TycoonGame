using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "SpaceTycoon/AudioGroup")]
public class AudioClipGroup : ScriptableObject
{
    [Range(0, 2)]
    public float volumeMin = 1;
    [Range(0, 2)]
    public float volumeMax = 1;
    [Range(0, 2)]
    public float pitchMin = 1;
    [Range(0, 2)]
    public float pitchMax = 1;
    [Range(0, 2)]
    public float cooldown = 0.1f;

    public List<AudioClip> clips;
    private float _timestamp;

    private void OnEnable()
    {
        _timestamp = 0;
    }
    
    public void Play(AudioSource source)
    {
        if (_timestamp > Time.time) return;
        if (clips.Count <= 0) return;
        _timestamp = Time.time + cooldown;
        
        source.volume = Random.Range(volumeMin, volumeMax);
        source.pitch = Random.Range(pitchMin, pitchMax);
        source.clip = clips[Random.Range(0, clips.Count)];
        source.Play();
    }
}