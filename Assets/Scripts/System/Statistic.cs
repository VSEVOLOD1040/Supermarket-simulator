using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Statistic : MonoBehaviour, ISaveble
{
    public static Statistic instance;
    public StatisticData statisticData;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public int Money;

    public int CustomersServed;
    public int CustomersDeserved;

    public int MoneyEarned;
    public int MoneySpent;

    public int Profit; // MoneyEarned - MoneySpent

    public int ItemsSold;
    public int ItemsEarned;

    public int TrashItemsCleaned;

    public Dictionary<string, int> RemainingProductsInMarket = new Dictionary<string, int>();

    public void AddProduct(string productName, int amount)
    {
        ItemsEarned += amount;
        if (RemainingProductsInMarket.ContainsKey(productName))
        {
            RemainingProductsInMarket[productName] += amount;
        }
        else
        {
            RemainingProductsInMarket.Add(productName, amount);
        }
    }
    public void RemoveProduct(string productName, int amount)
    {
        ItemsSold += amount;

        if (RemainingProductsInMarket.ContainsKey(productName) && RemainingProductsInMarket[productName] - amount > 0)
        {
            RemainingProductsInMarket[productName] -= amount;
        }
        else
        {
            RemainingProductsInMarket.Remove(productName);
        }


    }

    public void UpdateProfit()
    {
        Profit = MoneyEarned - MoneySpent;
    }

    public object SaveData()
    {
        UpdateProfit();
        statisticData.CustomersServed += CustomersServed;
        statisticData.CustomersDeserved += CustomersDeserved;
        statisticData.MoneyEarned += MoneyEarned;
        statisticData.MoneySpent += MoneySpent;
        statisticData.Profit += Profit;
        statisticData.ItemsSold += ItemsSold;
        statisticData.ItemsEarned += ItemsEarned;
        statisticData.TrashItemsCleaned += TrashItemsCleaned;

        return statisticData;
    }

    public void LoadData(string data)
    {
        
    }
    [Serializable]
    public class StatisticData
    {
        public int CustomersServed;
        public int CustomersDeserved;
        public int MoneyEarned;
        public int MoneySpent;
        public int Profit; // MoneyEarned - MoneySpent
        public int ItemsSold;
        public int ItemsEarned;
        public int TrashItemsCleaned;
       

        public StatisticData(Statistic statistic)
        {
            CustomersServed = statistic.CustomersServed;
            CustomersDeserved = statistic.CustomersDeserved;
            MoneyEarned = statistic.MoneyEarned;
            MoneySpent = statistic.MoneySpent;
            Profit = statistic.Profit;
            ItemsSold = statistic.ItemsSold;
            ItemsEarned = statistic.ItemsEarned;
            TrashItemsCleaned = statistic.TrashItemsCleaned;

        }
    }
}