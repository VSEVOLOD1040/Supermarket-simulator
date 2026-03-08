using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsFrame : MonoBehaviour
{
    public Slider SFX;
    public Slider Music;

    public List<AudioSO> SFXAudioSO;
    public List<AudioSO> MusicAudioSO;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    { 
        SFX.value=SFXAudioSO[0].volume;
        Music.value = MusicAudioSO[0].volume;

    }

    public void ChangeSFXVolume()
    {
        foreach (AudioSO audio in SFXAudioSO)
        {
            audio.volume = SFX.value;
        }
    }
    public void ChangeMusicVolume()
    {
        foreach (AudioSO audio in MusicAudioSO)
        {
            audio.volume = Music.value;
        }

        GameObject.FindObjectsOfType<AudioSource>().ToList<AudioSource>().ForEach(audioSource =>
        {

                audioSource.volume = Music.value;

        });
    }
}
