using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "AudioSO", menuName = "Audio", order = 1)]
public class AudioSO : ScriptableObject
{
    
    public List<AudioClip> audioClips;

    [Range(0, 1)]
    public float volume = 1f;
    
    [Range(-3, 3)]
    public float pitch = 1f;
    
    public void Play(AudioSource audio)
    {
        audio.clip = audioClips[Random.Range(0,audioClips.Count-1)];
        audio.volume = volume;
        audio.pitch = pitch;
        audio.Play();
    }
}

