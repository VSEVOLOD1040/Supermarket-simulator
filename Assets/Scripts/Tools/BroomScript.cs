using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomScript : MonoBehaviour, IInteractable, IPickableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject interactor)
    {

        if (interactor != null)
        {
            interactor.GetComponent<PlayerScript>().Pickup(gameObject, ItemSize.Broom);
        }
    }

    public void PickUp()
    {
        throw new System.NotImplementedException();
    }

    public void Drop()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;



        gameObject.transform.SetParent(null);
    }
}
