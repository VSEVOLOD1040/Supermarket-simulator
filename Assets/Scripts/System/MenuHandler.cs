using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{


    public GameObject[] Frames;
    public void SwitchFrame(GameObject frameToOpen)
    {
        foreach (var frame in Frames)
        {
            frame.SetActive(false);
        }
        frameToOpen.SetActive(true);
    }

    public void play()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
