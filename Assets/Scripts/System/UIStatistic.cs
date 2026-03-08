using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatistic : MonoBehaviour
{
    public GameObject statisticPanel;
    public TextMeshProUGUI moneyText;

    public Statistic statistic;

    public GameObject ProductUIPrefab;
    public Transform ProductRemainsParent;

    // Start is called before the first frame update
    void Awake()
    {
        
        CloseShopButton.OnDayEnded += OpenPanel;
    }
    private void Start()
    {
        statistic = Statistic.instance;

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
        GameObject.FindAnyObjectByType<PlayerController>().TurnOnCharacterMouseController(false);
        statisticPanel.SetActive(true);
        InitStatistic();
        //Debug.Log("Statistic Panel Opened");
    }


    public void InitStatistic()
    {
        statistic.UpdateProfit();
        moneyText.text = $"Money: {statistic.Money}\n" +
            $"Customers served: {statistic.CustomersServed}\n" +
            $"Customers deserved: {statistic.CustomersDeserved}\n\n" +
            $"Items earned: {statistic.ItemsEarned}\n" +
            $"Items sold: {statistic.ItemsSold}\n\n" +
            $"Trash cleaned: {statistic.TrashItemsCleaned}\n" +
            $"Profit: {statistic.Profit}";


        foreach (var item in statistic.RemainingProductsInMarket)
        {
            GameObject productUIObj = Instantiate(ProductUIPrefab, ProductRemainsParent);
            UIProductRemainsStatistic productUI = productUIObj.GetComponent<UIProductRemainsStatistic>();
            productUI.Init(item.Key, item.Value);
        }
    }

}
