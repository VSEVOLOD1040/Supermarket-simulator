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
            Statistic.instance.TrashItemsCleaned += 1;
            Destroy(gameObject);
        }
        //else
        //{

        //}
    }

}
