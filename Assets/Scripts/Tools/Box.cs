using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Box : MonoBehaviour, IPickableObject, IInteractable
{
    public PlayerScript player;

    public Image box_image;
    public TextMeshProUGUI box_text;


    public Image box_image2;
    public TextMeshProUGUI box_text2;

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
}
