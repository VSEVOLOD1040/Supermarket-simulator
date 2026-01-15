using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    public static Statistic instance;

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

}
