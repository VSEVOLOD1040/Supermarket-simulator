using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public void PlayAudio(AudioSO audio, Vector3 position = new Vector3())
    {
        if (position.x != 0 && position.y != 0 && position.z != 0)
        {
            //Debug.Log("Playing audio at position: " + position);
            AudioSource AudioSource = Instantiate(Resources.Load<GameObject>("Prefabs/AudioSource"), position, Quaternion.identity).GetComponent<AudioSource>();
            audio.Play(AudioSource);
            Destroy(AudioSource.gameObject, AudioSource.clip.length +1);
        }
    }

    public void PlayMusic(AudioSO audio, Vector3 position = new Vector3())
    {
        if (position.x != 0 && position.y != 0 && position.z != 0)
        {
            StartCoroutine(PlayMusicCoroutine(audio, Instantiate(Resources.Load<GameObject>("Prefabs/AudioSource"), position, Quaternion.identity).GetComponent<AudioSource>()));
        }
    }
    public IEnumerator PlayMusicCoroutine(AudioSO audio, AudioSource audioSource)
    {
        while (true)
        {
            audio.Play(audioSource);
            yield return new WaitForSeconds(audioSource.clip.length);
        }
    }
}
