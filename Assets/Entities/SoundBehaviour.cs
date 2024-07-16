using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _playedMusic;
    private int _clipIndex;

    public void SetAudioSource (AudioSource audioSource)
    {
        _audioSource = audioSource;
    }

    public void PlaySound(List<AudioClip> audioClips)
    {
        if (audioClips.Count == 0) return;
        
        try
        {
            var audioClip = audioClips[Random.Range(0, audioClips.Count)];
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }        
        catch (NullReferenceException)
        {
            Debug.Log("AudioSource not assigned");
        }
    }
}
