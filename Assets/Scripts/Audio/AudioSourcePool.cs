
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioSourcePool: MonoBehaviour
    {

        public AudioSource audioSourcePrefab;
        private List<AudioSource> _audioSources = new();

        private void Awake()
        {
            Debug.Log("awake");
            Events.OnAudioClipGroupPlayed += PlayAudioGroup;
        }

        private void OnDestroy()
        {
            Events.OnAudioClipGroupPlayed -= PlayAudioGroup;
        }
        

        public AudioSource getSource()
        {
            foreach (var audioSource in _audioSources)
            {
                if (!audioSource.isPlaying) return audioSource;
            }

            AudioSource newSource = Instantiate(audioSourcePrefab, transform);
            _audioSources.Add(newSource);

            return newSource;
        }

        private void PlayAudioGroup(AudioClipGroup audioGroup)
        {
            Debug.Log("play");
            audioGroup.Play(getSource());
        }
    }