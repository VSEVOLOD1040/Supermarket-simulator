using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BoxScript : MonoBehaviour, IInteractableBox
{
    public PlayerScript player;
    //public ProductSO product;

    public Image box_image;
    public TextMeshProUGUI box_text;


    public void Drop()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        

        gameObject.transform.SetParent(null);
    }

    public void PickUp()
    {
        player.BoxPickup(gameObject);
    }
    public void Init(ProductSO product)
    {
        box_image.sprite = product.image;
        box_text.text = $"{product.name} {product.Amount}/{product.MaxAmount}";
    }


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

}
