using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour, IInteractable
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
        if (interactor != null)
        {
            GameObject item = interactor.GetComponent<PlayerScript>().CurrentItem;
            if (item == null)
            {
                UIMessage.instance.ShowMessage("You have nothing to throw away!");

                return;

            }
            if (item.TryGetComponent<BoxScript>(out BoxScript box_script))
            {
                if (box_script != null)
                { 
                    if (box_script.Amount <= 0)
                    {
                        box_script.Drop();
                        interactor.GetComponent<PlayerScript>().CurrentItem = null;
                        Destroy(box_script.gameObject);
                    }
                    
                }


            }




        }

    }
}
