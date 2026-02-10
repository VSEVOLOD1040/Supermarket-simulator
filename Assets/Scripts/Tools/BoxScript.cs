using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class BoxScript : Box
{

    public MarketDataSO marketData;
    public ProductSO product;

    public int Amount;

    public int MaxAmount;
    public void Init(ProductSO product, int amount)
    {
        this.product = product;
        Amount = amount;
        MaxAmount= marketData.GetBatchSize(product);
        box_text.text = product.Name;
        box_image.sprite = product.image;

        box_text2.text = product.Name;
        box_image2.sprite = product.image;
        UpdateAmountUI();
    }

    public void TakeProduct()
    {
        Amount--;
        UpdateAmountUI();

        //if (Amount <= 0)
        //{
        //    player.GetComponent<PlayerScript>().CurrentItem = null;
        //    Destroy(gameObject);
        //}
    }

    void UpdateAmountUI()
    {
        box_text.text = $"{product.name} {Amount}/{MaxAmount}";

    }
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

}
