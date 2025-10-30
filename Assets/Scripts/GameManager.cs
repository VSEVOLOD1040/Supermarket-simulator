using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerScript player;
    public SaveManager saveManager;
    public GameData gameData;

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        saveManager.Save(gameData);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
        {
            SaveData();
        }
    }
    public void UpdateBalance(float balance)
    {
        gameData.Balance = balance;
    }
    public void LoadData()
    {
        GameData loadedData = saveManager.Load();

        if (loadedData != null)
        {
            gameData = loadedData;
            player.Money = gameData.Balance;
            player.UpdateUI();
        }
        else
        {
            gameData = new GameData(500);

        }

    }
}