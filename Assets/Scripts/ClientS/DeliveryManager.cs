using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public int MaxProductAmountForOneProductType = 10;
    public static DeliveryManager Instance;

    public List<Dictionary<string, int>> Orders = new List<Dictionary<string, int>>();

    public MarketDataSO marketData;
    public float MinimalTimeBetweenOrders = 10f;
    public float MaxTimeBetweenOrders = 15f;

    public float PriceMultiplier = 1.5f;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Dictionary<string, int> GetRandomProducts(List<ProductSO> products)
    {
        Dictionary<string, int> result = new Dictionary<string, int>();

        if (products == null || products.Count == 0)
            return result;

        int countToSelect = Random.Range(2, products.Count + 1);
        List<ProductSO> tempList = new List<ProductSO>(products);
        Shuffle(tempList);

        for (int i = 0; i < countToSelect; i++)
        {
            ProductSO product = tempList[i];
            string productName = product.Name;

            int quantity = GetRandomAmount();

            if (!result.ContainsKey(productName))
                result.Add(productName, quantity);
        }

        return result;
    }

    private int GetRandomAmount()
    {
        int quantity = 1;
        float chance = 1f;

        while (quantity < MaxProductAmountForOneProductType)
        {
            chance *= 0.5f;
            if (Random.value < chance)
                quantity++;
            else
                break;
        }

        return quantity;
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    public IEnumerator GenerateOrders()
    {
        while (true)
        {   
            yield return new WaitForSeconds(Random.Range(MinimalTimeBetweenOrders, MaxTimeBetweenOrders));
            if (TimeManager.isShopOpen)
            {
                Orders.Add(GetRandomProducts(marketData.GetProducts()));
                UpdateUI();
            }
        }
    }

    public void UpdateUI()
    {
        Debug.Log("=== Order ===");
        foreach(var item in Orders[Orders.Count -1])
        {
            
            print(item.Key + item.Value);
        }
        Debug.Log("======");
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateOrders());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckOrder(Dictionary<string, int> order)
    {
        Dictionary<string, int> CurrentOrder = null;
        foreach (var item in Orders)
        {
            if (CheckDictionary(item, order))
            {
                Debug.Log("Check dictionary == true");

                CurrentOrder = item;
                break;
            }
        }

        if (CurrentOrder != null)
        {
            
            //GameObject.Find("Player").GetComponent<PlayerScript>().AddMoney(marketData.GetPrice(marketData.GetProductByName(CurrentOrder.Keys.GetEnumerator().Current)) * CurrentOrder.Values.GetEnumerator().Current);
            
            float moneyEarned = 0f;
            foreach (var item in CurrentOrder)
            {
                ProductSO product = marketData.GetProductByName(item.Key);
                moneyEarned += product.Price * item.Value * PriceMultiplier+8;
            }
            GameObject.Find("Player").GetComponent<PlayerScript>().AddMoney(moneyEarned);

            Orders.Remove(CurrentOrder);
            return true;
        }
        else
        {
            return false;
        }
        
    }
    public bool CheckDictionary(Dictionary<string, int> dict1, Dictionary<string, int> dict2)
    {
        foreach (var kvp in dict1)
        {
            if (dict2.ContainsKey(kvp.Key))
            {
                if (dict2[kvp.Key] < kvp.Value)
                    return false;
            }
            else
            {
                return false;
            }
        }
        return true;
    }
}
