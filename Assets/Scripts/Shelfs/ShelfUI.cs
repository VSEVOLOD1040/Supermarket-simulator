using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ShelfUI : MonoBehaviour
{
    public GameObject Panel;
    public TextMeshProUGUI NewPriceText;
    public TextMeshProUGUI OldPriceText;
    public float Price;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePrice(float price)
    {
        NewPriceText.text = $"New price: {price.ToString()}$";
    }

    public void Add(float amount)
    {
        Price += amount;
        Price = (float)Math.Round(Price, 2);

        UpdatePrice(Price);

    }
    public void Remove(float amount)
    {
        Price -= amount;
        Price = (float)Math.Round(Price, 2);

        UpdatePrice(Price);
    }

    public float GetPrice()
    {
        return Price;
    }

    public void SwitchPanel(bool State, ProductSO product=null)
    {
        Panel.SetActive(State);
        EventBus.MouseLock.Invoke(!State);
        EventBus.CameraLookEnabled.Invoke(!State);
        PlayerScript.RaycastAllowed = !State;

        if (product != null)
        {
            OldPriceText.text = $"Current price: {product.Price.ToString()}";
            Price = product.Price;
            NewPriceText.text = $"New price: {product.Price.ToString()}";
        }

        


    }
}
