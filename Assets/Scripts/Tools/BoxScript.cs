using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class BoxScript : MonoBehaviour, IPickableObject, IInteractable
{

    public MarketDataSO marketData;
    public PlayerScript player;
    public ProductSO product;

    public Image box_image;
    public TextMeshProUGUI box_text;


    public Image box_image2;
    public TextMeshProUGUI box_text2;

    public int Amount;

    public int MaxAmount;

    public void Drop()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        

        gameObject.transform.SetParent(GameObject.Find("BOXES").transform);

    }

    public void PickUp()
    {
        player.Pickup(gameObject, ItemSize.BigItem);
    }
    public void Interact(GameObject interactor = null)
    {
        PickUp();
    }
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

        if (Amount <= 0)
        {
            player.GetComponent<PlayerScript>().CurrentItem = null;
            Destroy(gameObject);
        }
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
