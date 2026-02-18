using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBox : Box
{
    // Start is called before the first frame update
    public Dictionary<ProductSO, int> ProductsInside = new Dictionary<ProductSO, int>();

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    public void AddProduct(ProductSO product, int amount)
    {
        UIMessage.instance.ShowMessage($"Added {amount} {product.Name} to delivery box");
        if (ProductsInside.ContainsKey(product))
        {
            ProductsInside[product] += amount;
        }
        else
        {
            ProductsInside.Add(product, amount);
        }
    }
}
