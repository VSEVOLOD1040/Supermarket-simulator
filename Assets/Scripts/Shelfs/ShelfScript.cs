using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ShelfScript : MonoBehaviour, IInteractable, ISaveble
{
    public ShelfUI shelfUI;
    public Button SetPriceButton;
    public ProductSO current_product;
    public TextMeshProUGUI PriceTag;
    public MarketDataSO marketDataSO;

    public ShelfData shelfData;

    public void Interact(GameObject interactor = null)
    {
        if (interactor != null)
        {
            GameObject item = interactor.GetComponent<PlayerScript>().CurrentItem; // Можливо переробити на більш гнучку логіку

            if (item == null)
            {
                return;
            }

            if (item.TryGetComponent<BoxScript>(out BoxScript box_script))
            {
                foreach (var slot in slots)
                {
                    if (slot.GetComponent<ProductSlot>().Product == null)
                    {
                        if (box_script.Amount > 0) // можливо використовувати перевірку через TakeProduct()
                        {
                            if (current_product == null)
                            {
                                current_product = box_script.product;
                                UpdatePriceText();

                            }

                            if (box_script.product == current_product)
                            {
                                slot.GetComponent<ProductSlot>()?.SetProduct(box_script.product);

                                shelfData.Amount = GetProductAmount();
                                shelfData.ProductName = current_product.Name;

                                box_script.TakeProduct();

                                break;
                            }
                        }
                       

                    }
                }
            }
            if (item.TryGetComponent<PriceToolScript>(out PriceToolScript priceToolScript))
            {
                //Debug.Log(current_product);
                if (current_product != null)
                {
                    shelfUI.SwitchPanel(true, current_product);
                    SetPriceButton.onClick.AddListener(SetPrice);
                }
                
            }
            
        }
    }
    public void UpdatePriceText()
    {
        if (current_product != null)
        {
            PriceTag.text = $"{current_product.Price}$";

        }

    }
    public void SetPrice()
    {
        if (current_product != null)
        {
            current_product.Price = shelfUI.GetPrice();
            SetPriceButton.onClick.RemoveListener(SetPrice);
            shelfUI.SwitchPanel(false);
            EventBus.UpdatePriceTags();


        }
    }

    public ProductSO TakeProduct(int Amount, out int TakenAmount)
    {
        TakenAmount = 0;
        //Debug.Log("TakeProduct " + Amount);
        ProductSO product = null;
        for (int i = 0; i < Amount; i++)
        {
            foreach (var slot in slots)
            {
                if (slot.GetComponent<ProductSlot>().Product != null)
                {
                    product = slot.GetComponent<ProductSlot>().Product;
                    slot.GetComponent<ProductSlot>().Product = null;
                    Destroy(slot.transform.GetChild(0).gameObject);
                    TakenAmount++;

                    shelfData.Amount = GetProductAmount();

                    if (CheckIfShelfEmpty())
                    {
                        current_product = null;
                        PriceTag.text = "";

                        shelfData.Amount = 0;
                        shelfData.ProductName = "";
                    }
                    break;
                }
            }
        }


        if (product)
        {
            Statistic.instance.RemoveProduct(product.Name, TakenAmount);
            return product;

        }

        return null;

    }

    public int GetProductAmount()
    {
        int count = 0;
        foreach (var slot in slots)
        {
            if (slot.GetComponent<ProductSlot>().Product != null)
            {
                
                count++;
            }
        }
        return count;
    }

    public bool CheckIfShelfEmpty()
    {
        foreach (var slot in slots)
        {
            if (slot.GetComponent<ProductSlot>().Product != null)
            {
                return false;
            }
        }
        return true;
    }

    public List<GameObject> slots;
    public GameObject SlotsParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        EventBus.UpdatePriceTags += UpdatePriceText;
    }
    private void OnDestroy()
    {
        EventBus.UpdatePriceTags -= UpdatePriceText;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public object SaveData()
    {
        //if (current_product == null)
        //{
        //    return new ShelfData("",0);
        //}

        return shelfData;

    }

    public void LoadData(string data)
    {   

        ShelfData loaded_data = JsonUtility.FromJson<ShelfData>(data);
        //Debug.Log(loaded_data);

        for (int i = 0; i < loaded_data.Amount; i++)
        {
            //Debug.Log("Data loaded for Shelf: " + loaded_data.ProductName);

            if (i < slots.Count)
            {
                slots[i].GetComponent<ProductSlot>().SetProduct(marketDataSO.GetProductByName(loaded_data.ProductName));

            }
        }

        shelfData = loaded_data;
    }

    private void OnEnable()
    {
        slots = new List<GameObject>();

        foreach (Transform child in SlotsParent.transform)
        {
            slots.Add(child.gameObject);
        }

        for (int i = 0; i < shelfData.Amount; i++)
        {
            //Debug.Log("Data loaded for Shelf: " + shelfData.ProductName);

            slots[i].GetComponent<ProductSlot>().SetProduct(marketDataSO.GetProductByName(shelfData.ProductName));
        }
    }
}
[Serializable]
public class ShelfData
{
    public string ProductName;
    public int Amount;

    public ShelfData(string product, int amount)
    {
        ProductName = product;
        Amount = amount;
    }
}
