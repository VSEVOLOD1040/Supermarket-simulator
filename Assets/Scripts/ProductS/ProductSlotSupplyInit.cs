using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;


public class ProductSlotSupplyInit : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI name;
    public TextMeshProUGUI amount;
    public Button button;
    public void Init(ProductSO product, int Amount, int Price)
    {
        ProductsSupplierMenager computer = GameObject.Find("Computer").GetComponent<ProductsSupplierMenager>();
        image.sprite = product.image;
        name.text = product.name;
        amount.text = $"{Price}$ / {Amount} pcs";
        button.onClick.AddListener(() => computer.Order(product));
    }
}
