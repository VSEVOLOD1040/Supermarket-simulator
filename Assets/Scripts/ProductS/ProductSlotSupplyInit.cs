using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;


public class ProductSlotSupplyInit : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI name;
    public TextMeshProUGUI amount;
    public Button button;
    public TextMeshProUGUI RemainingAmountText;
    public int RemainigAmount;
    public void Init(ProductSO product, int Amount, int Price)
    {
        ProductsSupplierMenager computer = GameObject.Find("Computer").GetComponent<ProductsSupplierMenager>();
        image.sprite = product.image;
        name.text = product.name;
        amount.text = $"{Price}$ / {Amount} pcs";
        
        AmountUpdate(product);

        button.onClick.AddListener(() => computer.Order(product));
        button.onClick.AddListener(() => AmountUpdate(product));

    }
    public void AmountUpdate(ProductSO product)
    {
        RemainingAmountText.text = Statistic.instance.RemainingProductsInMarket.ContainsKey(product.Name) ?
            Statistic.instance.RemainingProductsInMarket[product.Name].ToString() : "0";
    }
}
