using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashItemScript : MonoBehaviour, IInteractable
{


   
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(GameObject interactor = null)
    {
        if (interactor.GetComponent<PlayerScript>().CheckInventory("broom"))
        {


            Collider[] collisions = Physics.OverlapSphere(gameObject.transform.position, 0.25f);

            foreach (Collider col in collisions)
            {
                if (col.gameObject.transform.IsChildOf(GameObject.Find("broom").transform))
                {

                    Statistic.instance.TrashItemsCleaned += 1;
                    Destroy(gameObject);
                    break;

                }
            }


            
        }
        else
        {
            UIMessage.instance.ShowMessage("You need a broom to clean this!");
        }
    }

}
