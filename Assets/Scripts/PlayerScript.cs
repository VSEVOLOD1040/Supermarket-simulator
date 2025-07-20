using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public GameObject CurrentItem;
    public GameObject BoxPositionGameObject;

    public void BoxPickup(GameObject box)
    {

        if (CheckInventory())
        {
            box.GetComponent<BoxCollider>().enabled = false;
            box.GetComponent<Rigidbody>().isKinematic = true;

            box.transform.SetParent(gameObject.transform, true);
            box.transform.localPosition = BoxPositionGameObject.transform.localPosition;
            box.transform.rotation = gameObject.transform.rotation;

            
            CurrentItem = box;
        }
    }

    public bool CheckInventory(string name = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            if (CurrentItem != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (CurrentItem != null && CurrentItem.name.Contains(name))

            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Raycast();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (CheckInventory("Box"))
            {
                CurrentItem.GetComponent<IInteractableBox>().Drop();
                CurrentItem = null;
            }
        }
    }

    public void Raycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        IInteractableBox box = null;
        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.gameObject.TryGetComponent<IInteractableBox>(out box))
            {
                box.PickUp();

            }
        }
    }
}
