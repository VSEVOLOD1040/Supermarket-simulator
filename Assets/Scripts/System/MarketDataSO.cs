using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MarketData", menuName = "Supermarket/Market Data")]
public class MarketDataSO : ScriptableObject
{
    [System.Serializable]
    public struct MarketEntry
    {
        public ProductSO product;
        public int marketPrice;
        public int batchSize;

        public bool IsEnabled;
    }

    public MarketEntry[] entries;

    private Dictionary<ProductSO, MarketEntry> entryLookup;

    private void OnEnable()
    {
        Init();
    }

    public int GetPrice(ProductSO product)
    {
        if (entryLookup.TryGetValue(product, out MarketEntry entry))
            return entry.marketPrice;

        return 0;
    }
    public void Init()
    {
        entryLookup = new Dictionary<ProductSO, MarketEntry>();
        foreach (var entry in entries)
        {
            if (entry.product != null)
                entryLookup[entry.product] = entry;
        }
    }
    public int GetBatchSize(ProductSO product)
    {
        Init();

        //Debug.Log("Getting batch size for product: " + product.Name);
        if (entryLookup.TryGetValue(product, out MarketEntry entry))
            return entry.batchSize;

        return 0;
    }
    public List<ProductSO> GetProducts()
    {
        List<ProductSO> products = new List<ProductSO>();
        foreach (var entry in entries)
        {
            if (entry.product != null && entry.IsEnabled)
                products.Add(entry.product);
        }
        return products;
    }

    public ProductSO GetProductByName(string ProductName)
    {
        List<ProductSO> products = new List<ProductSO>();
        foreach (var entry in entries)
        {
            if (entry.product != null && entry.product.Name == ProductName)
                return entry.product;

        }
        return null;
    }
    public void EnableProduct(string productName)
    {
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].product != null && entries[i].product.Name == productName)
            {
                MarketEntry updatedEntry = entries[i];
                updatedEntry.IsEnabled = true;
                entries[i] = updatedEntry;
                entryLookup[entries[i].product] = updatedEntry;
                break;
            }
        }
    }
}
