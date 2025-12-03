using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTreePanel : MonoBehaviour
{
    public int CurrentLevel;
    public List<float> LevelPrices = new List<float>();

    public PlayerScript Player;

    public float MoneyEarnedThisLevel;
    public float UpgradeProgress;

    public Image Bar;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerScript>();
        Player.OnMoneyChanged += MoneyEarned;
        

        NewLevel(); // тут має бути перевірка, щоб не підвищувати рівень при перезаході в гру
    }


    public void MoneyEarned(float money)
    {
        MoneyEarnedThisLevel += money;

        UpgradeProgress = MoneyEarnedThisLevel / LevelPrices[CurrentLevel];

        if (UpgradeProgress >= 1)
        {
            UpgradeProgress = 1;
            NewLevel();
        }
        else
        {
            UpgradeProgressBar(UpgradeProgress);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpgradeProgressBar(float progress)
    {
        Bar.fillAmount = progress;
    }
    public void NewLevel()
    {
        CurrentLevel += 1;
        Player.UpgradePoints += 1;
        MoneyEarnedThisLevel = 0;
        UpgradeProgressBar(0);

        Player.CurrentLevel = CurrentLevel;
        Player.UpdateUI();
    }

    private void OnDestroy()
    {
        Player.OnMoneyChanged -= MoneyEarned;
    }
}
