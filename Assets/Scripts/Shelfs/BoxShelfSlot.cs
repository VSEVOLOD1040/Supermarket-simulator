using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Playables;


public class BoxShelfSlot : MonoBehaviour, IInteractable, ISaveble
{

    public GameObject Box;
    public BoxShelfData gameData;
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
            else if (item.TryGetComponent<Box>(out Box box_script))
            {
                if (Box == null)
                {
                    box_script.Drop();

                    Setbox(item);
                    interactor.GetComponent<PlayerScript>().CurrentItem = null;
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

        box_script.FixSizeIssue();
    }
    public object SaveData()
    {
        if (Box != null)
        {
            return new BoxShelfData(Box.GetComponent<BoxScript>().product, Box.GetComponent<BoxScript>().Amount);

        }
        else
        {
            return null;
        }
    }

    public void LoadData(string data)
    {

        BoxShelfData loadedData = JsonUtility.FromJson<BoxShelfData>(data);

        if (loadedData != null)
        {
            gameData = loadedData;


            GameObject box = Instantiate(Resources.Load<GameObject>("Prefabs/Box"));
            box.GetComponent<BoxScript>().Init(gameData.Product, gameData.Amount);
            Setbox(box);

        }


        /////////
        
        

    }
    [Serializable]
    public class BoxShelfData
    {
        public ProductSO Product;
        public int Amount;

        public BoxShelfData(ProductSO product, int amount)
        {
            Product = product;
            Amount = amount;
        }
    }
}
