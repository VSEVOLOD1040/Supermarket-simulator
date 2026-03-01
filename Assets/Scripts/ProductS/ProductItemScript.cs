using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductItemScript : MonoBehaviour, IPickableObject, IInteractable
{

    public PlayerScript player;

    public ProductSO ThisProduct;

    public void Drop()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.transform.SetParent(null);


    }

    public void PickUp()
    {
        player.Pickup(gameObject, ItemSize.Product);
    }
    public void Interact(GameObject interactor = null)
    {
        PickUp();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
