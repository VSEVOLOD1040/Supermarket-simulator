using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    public PlayerScript player;
    public SaveManager saveManager;
    public GameData gameData;

    public float CurrentTime;

    bool isShopOpen = false;
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

        if (isShopOpen)
        {
            CurrentTime += Time.deltaTime;
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

    public void OpenShop()
    {
        isShopOpen = true;
    }

}