using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    // This script is on the CANVAS //


    public GameObject PauseMenu;

    public GameObject LeaveWithoutSavingButton;
    public GameObject LeaveWithoutSavingButton2;

    public GameObject MainFrame;
    public GameObject SettingsFrame;

    public GameObject Computer;

    public GameObject Player;

    public IEnumerator Pause()
    {
        if (!Computer.activeInHierarchy)
        {
           Player.GetComponent<UIRaycaster>().enabled = false;

            yield return new WaitForSeconds(0.01f);

            CloseSettings();
            PauseMenu.SetActive(true);

            Player.GetComponent<PlayerController>().TurnOnCharacterMouseController(false);
            EventSystem.current.SetSelectedGameObject(null);
           // Debug.Log(EventSystem.current.currentSelectedGameObject);
            Time.timeScale = 0f;
        }
        
    }
    public void Resume()
    {
        Player.GetComponent<PlayerController>().TurnOnCharacterMouseController(true);

        PauseMenu.SetActive(false);
        Time.timeScale = 1f;

        Player.GetComponent<UIRaycaster>().enabled = true;

    }
    async public void SaveGame()
    {
        await GameObject.Find("SAVE_MANAGER").GetComponent<SaveManager>().Save();
        //////Debug.Log("Game Saved");    

    }
    async public void LoadMenu(bool save)
    {
        if (save)
        {

            await GameObject.Find("SAVE_MANAGER").GetComponent<SaveManager>().Save();
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
        Player = GameObject.Find("Player");
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
                StartCoroutine(Pause());
            }
        }
    }
}
