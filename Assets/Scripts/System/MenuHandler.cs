using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GameObject MainFrame;
    public GameObject SettingsFrame;
    public GameObject AboutFrame;
    public GameObject ExitFrame;

    public void SwitchFrame(GameObject frameToOpen)
    {
        MainFrame.SetActive(false);
        SettingsFrame.SetActive(false);
        AboutFrame.SetActive(false);
        ExitFrame.SetActive(false);
        frameToOpen.SetActive(true);
    }

    public void play()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
