using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class CompleteDelivery : MonoBehaviour, IInteractable
{
    public GameObject Box;
    public DoorScript Door;
    public void Interact(GameObject interactor = null)
    {
        if (!Door.IsOpen) { return; }
        //print(interactor);
        if (interactor != null)
        {
            GameObject item = interactor.GetComponent<PlayerScript>().CurrentItem;
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
                if (delivery_box_script != null)
                {
                    delivery_box_script.Drop();

                    StartCoroutine(Setbox(item));
                    
                    interactor.GetComponent<PlayerScript>().CurrentItem = null;
                }


            }
        }

    }

    public IEnumerator Setbox(GameObject box)
    {
        Box box_script = box.GetComponent<Box>();

        box_script.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        box_script.gameObject.GetComponent<BoxCollider>().enabled = false;
        box_script.transform.position = gameObject.transform.position;
        box_script.transform.rotation = gameObject.transform.rotation;

        box_script.transform.SetParent(gameObject.transform);
        Box = box_script.gameObject;

        Door.GetComponent<BoxCollider>().enabled = false;
        Door.Close();
        yield return new WaitForSeconds(2f);

        if (CheckDelivery())
        {

            
            

   
            UIMessage.instance.ShowMessage("Delivery complete! You earned some money!");

            Destroy(Box);
            Box = null;
        }
        else
        {
            UIMessage.instance.ShowMessage("This order doesn't exist!");

        }

        Door.GetComponent<BoxCollider>().enabled = true;
    }

    public bool CheckDelivery()
    {
        List<Dictionary<string, int>> Orders = DeliveryManager.Instance.Orders;

        Dictionary<string, int> current_order = new Dictionary<string, int>();

        foreach (var Product in Box.GetComponent<DeliveryBox>().ProductsInside)
        {
            current_order.Add(Product.Key.Name, Product.Value);
            Debug.Log(Product.Key.Name+" "+Product.Value);
        }

        return DeliveryManager.Instance.CheckOrder(current_order);

    }
}
