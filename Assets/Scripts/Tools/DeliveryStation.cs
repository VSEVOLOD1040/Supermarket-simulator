using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BoxShelfSlot;

public class DeliveryStation : MonoBehaviour, IInteractable
{
    public GameObject Box;
    public void Interact(GameObject interactor = null)
    {
        //print(interactor);
        if (interactor != null)
        {
            GameObject item = interactor.GetComponent<PlayerScript>().CurrentItem;
            //print(item);
            if (item == null)
            {

                if (Box != null)
                {

                    Box.GetComponent<Rigidbody>().isKinematic = false;
                    Box.GetComponent<BoxCollider>().enabled = true;
                    Box box_script = Box.GetComponent<Box>();
                    box_script.PickUp();
                    Box = null;
                }

            }
            else if (item.TryGetComponent<DeliveryBox>(out DeliveryBox delivery_box_script))
            {
                if (Box == null)
                {
                    delivery_box_script.Drop();

                    Setbox(item);
                    interactor.GetComponent<PlayerScript>().CurrentItem = null;
                }


            }
            else if (item.TryGetComponent<BoxScript>(out BoxScript box_script))
            {
                if (Box != null)
                {
                    ProductSO product = box_script.product;
                    if (Box.TryGetComponent<DeliveryBox>(out DeliveryBox delivery_box))
                    {
                        if (box_script.TakeProduct())
                        {
                            delivery_box.AddProduct(product, 1);
                        }
                    }
                    
                }


            }




        }

    }

    public void Setbox(GameObject box)
    {
        Box box_script = box.GetComponent<Box>();

        box_script.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        box_script.gameObject.GetComponent<BoxCollider>().enabled = false;
        box_script.transform.position = gameObject.transform.position;
        box_script.transform.rotation = gameObject.transform.rotation;

        box_script.transform.SetParent(gameObject.transform);
        Box = box_script.gameObject;
    }


}
