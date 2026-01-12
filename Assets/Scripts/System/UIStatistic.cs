using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatistic : MonoBehaviour
{
    public GameObject statisticPanel;
    public TextMeshProUGUI moneyText;
    // Start is called before the first frame update
    void Awake()
    {
        
        CloseShopButton.OnDayEnded += OpenPanel;

    }

    private void OnDestroy()
    {
        CloseShopButton.OnDayEnded -= OpenPanel;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel()
    {   

        statisticPanel.SetActive(true);
        InitStatistic();
        Debug.Log("Statistic Panel Opened");
    }


    public void InitStatistic()
    {
        moneyText.text = "Money: "+Statistic.instance.Money.ToString();
    }

}
