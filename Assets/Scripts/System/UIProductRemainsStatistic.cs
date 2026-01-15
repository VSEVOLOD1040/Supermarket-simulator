using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIProductRemainsStatistic : MonoBehaviour
{
    public MarketDataSO MarketData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Image ProductIcon;
    public TextMeshProUGUI ProductName;
    public TextMeshProUGUI ProductAmount;


    public void Init(string Name, int Amount)
    {
        ProductSO product  = MarketData.GetProductByName(Name);
        Sprite icon = product.image;

        ProductIcon.sprite = icon;
        ProductName.text = Name;
        ProductAmount.text = Amount.ToString();

    }
}
