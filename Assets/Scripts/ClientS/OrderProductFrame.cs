using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderProductFrame : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Image image;
    public MarketDataSO marketData;
    public void Init(string productName, int amount)
    {
        Text.text = $"{productName} : {amount}";
        image.sprite = marketData.GetProductByName(productName).image;
    }
}
