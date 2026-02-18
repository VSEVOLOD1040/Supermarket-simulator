using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    // This script is on the CANVAS //


    public GameObject PauseMenu;

    public GameObject LeaveWithoutSavingButton;
    public GameObject LeaveWithoutSavingButton2;

    public GameObject MainFrame;
    public GameObject SettingsFrame;

    public void Pause()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().TurnOnCharacterMouseController(false);
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().TurnOnCharacterMouseController(true);

        PauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }
    public void SaveGame()
    {
        GameObject.Find("GAME_MANAGER").GetComponent<SaveManager>().Save();
        //////Debug.Log("Game Saved");    

    }
    public void LoadMenu(bool save)
    {
        if (save)
        {
            GameObject.Find("GAME_MANAGER").GetComponent<SaveManager>().Save();
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void LeaveWithoutSaving()
    {
        LeaveWithoutSavingButton.SetActive(false);
        LeaveWithoutSavingButton2.SetActive(true);
    }
    public void DONOT_LeaveWithoutSaving()
    {
        LeaveWithoutSavingButton.SetActive(true);
        LeaveWithoutSavingButton2.SetActive(false);
    }
    public void OpenSettings()
    {
        MainFrame.SetActive(false);

        SettingsFrame.SetActive(true);
    }
    public void CloseSettings()
    {
        MainFrame.SetActive(true);

        SettingsFrame.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseMenu.activeInHierarchy)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
}
