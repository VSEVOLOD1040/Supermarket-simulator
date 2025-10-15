using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public GameObject CurrentItem;
    public GameObject BixItemPositionGameObject;
    public GameObject SmallItemPositionGameObject;
    public GameObject ToolPosition;
    public TextMeshProUGUI UImoneytext;
    public GameManager gameManager;
    public int Money;
    public static bool RaycastAllowed;
    public void AddMoney(int money)
    {
        Money += money;
        gameManager.UpdateBalance(Money);

        UpdateUI();
    }
    public bool RemoveMoney(int money)
    {
        if (Money - money >= 0)
        {
            Money -= money;
            UpdateUI();
            gameManager.UpdateBalance(Money);

            return true;
        }
        else
        {
            return false;
        }

    }


    private void Start()
    {
        UpdateUI();
        RaycastAllowed = true;
    }

    public void Pickup(GameObject item, ItemSize size)
    {

        if (CheckInventory())
        {
            item.GetComponent<BoxCollider>().enabled = false; //потрібно переробити для всіх коллайдерів
            item.GetComponent<Rigidbody>().isKinematic = true;

            item.transform.SetParent(gameObject.transform, true);
            item.transform.rotation = gameObject.transform.rotation;

            switch (size)
            {
                case ItemSize.BigItem:
                    item.transform.localPosition = BixItemPositionGameObject.transform.localPosition;
                    break;
                case ItemSize.SmallItem:
                    item.transform.localPosition = SmallItemPositionGameObject.transform.localPosition;
                    break;
                case ItemSize.Tool:
                    item.transform.localPosition = ToolPosition.transform.localPosition;
                    break;

            }


            CurrentItem = item;
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
        if (RaycastAllowed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Raycast();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!CheckInventory())
                {
                    CurrentItem.GetComponent<IPickableObject>().Drop();
                    CurrentItem = null;
                }
            }
        }
        
    }

    public void Raycast()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        IInteractable InteractableObject = null;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.TryGetComponent<IInteractable>(out InteractableObject))
            {
                InteractableObject.Interact(gameObject);

            }
        }
    }

    public void UpdateUI()
    {
        UImoneytext.text = $"Money: {Money}";
    }
}
