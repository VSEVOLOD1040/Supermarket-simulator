using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager : MonoBehaviour
{

    public MarketDataSO marketData;
    public ProductsSupplierMenager Computer;

    // Start is called before the first frame update
    void Start()
    {
        UpdateProductSupplierList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateProductSupplierList()
    {
        Computer.available_products=marketData.GetProducts();
        
    }

    public void AddProduct(string NewProduct)
    {
        marketData.EnableProduct(NewProduct);
        UpdateProductSupplierList();
    }
}
