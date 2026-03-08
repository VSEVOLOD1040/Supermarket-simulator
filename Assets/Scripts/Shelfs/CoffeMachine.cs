using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeMachine : ShelfScript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int CoffeAmount;
    public int MaxCoffeAmount;

    public ProductSO CoffeProduct;
    
    public ProductSO CoffeCupPrefab;

    public CoffeSetFillLevel coffeCylinder;
    
    public int CoffeAmountInOnePack = 2;

    public override void Interact(GameObject interactor = null)
    {
        if (interactor != null)
        {
            GameObject item = interactor.GetComponent<PlayerScript>().CurrentItem; // Можливо переробити на більш гнучку логіку

            if (item == null) return;

            

            if (item.TryGetComponent<PriceToolScript>(out PriceToolScript priceToolScript))
            {
                //Debug.Log(current_product);
                if (current_product != null)
                {
                    shelfUI.SwitchPanel(true, current_product);
                    SetPriceButton.onClick.AddListener(SetPrice);
                }

            }
            if (item.TryGetComponent<ProductItemScript>(out ProductItemScript product_item))
            {
                if (product_item.ThisProduct != CoffeProduct) return;
                if (CoffeAmount + CoffeAmountInOnePack > MaxCoffeAmount) return;

                product_item.ThisProduct = CoffeCupPrefab;


                float price = CoffeCupPrefab.Price;
                if (price <= 0)
                {
                    price = marketDataSO.GetPrice(CoffeCupPrefab);
                }
                product_item.ThisProduct.Price = price;

                GameObject slot = slots[0];
                //if (slot.GetComponent<ProductSlot>().Product == null)
                //{

                    if (current_product == null)
                    {
                        current_product = product_item.ThisProduct;

                    }

                    if (product_item.ThisProduct == current_product)
                    {
                        GameObject NewProduct = slot.GetComponent<ProductSlot>()?.SetProduct(product_item.ThisProduct);

                    NewProduct.SetActive(false);

                    CoffeAmount+= CoffeAmountInOnePack;
                        shelfData.Amount = CoffeAmount;
                        shelfData.ProductName = current_product.Name;

                        product_item.Drop();
                        Destroy(product_item.gameObject);

                    }
                     UpdateVisualCoffeAmount();

                     UpdatePriceText();


                //}

            }
        }
    }

    public override ProductSO TakeProduct(int Amount, out int TakenAmount)
    {
        if (CoffeAmount <= 0)
        {
            TakenAmount = 0;
            return null;
        }
        CoffeAmount -= 1;
        Debug.Log("Coffe taken");
        ProductSO product_taken = base.TakeProduct(Amount, out TakenAmount);

        if (CoffeAmount > 0)
        {
            GameObject slot = slots[0];

            GameObject NewProduct = slot.GetComponent<ProductSlot>()?.SetProduct(CoffeCupPrefab);

            NewProduct.SetActive(false);
            
        }
        current_product = CoffeCupPrefab;
        shelfData.Amount = CoffeAmount;
        shelfData.ProductName = CoffeCupPrefab.Name;
        UpdatePriceText();
        UpdateVisualCoffeAmount();
        return product_taken;
    }

    public void UpdateVisualCoffeAmount()
    {
        Debug.Log(CoffeAmount +" "+ MaxCoffeAmount);
        Debug.Log((float)CoffeAmount / (float)MaxCoffeAmount);
        coffeCylinder.SetFillLevel((float)CoffeAmount / (float)MaxCoffeAmount);
    }

    public override void LoadData(string data)
    {

        ShelfData loaded_data = JsonUtility.FromJson<ShelfData>(data);

        if (loaded_data == null) return;

        CoffeAmount = loaded_data.Amount;

        if (CoffeAmount > 0)
        {
            GameObject product = slots[0].GetComponent<ProductSlot>().SetProduct(marketDataSO.GetProductByName(loaded_data.ProductName));
            product.SetActive(false);
        }
        UpdatePriceText();
        UpdateVisualCoffeAmount();

        current_product = CoffeCupPrefab;

        shelfData = loaded_data;
    }
}
//[Serializable]
//public class CoffeMachineData
//{
//    public int Amount;

//    public CoffeMachineData(int amount)
//    {
//        Amount = amount;
//    }
//}
