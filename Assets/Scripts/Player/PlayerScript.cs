using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour, ISaveble
{
    public GameObject CurrentItem;
    public GameObject BixItemPositionGameObject;
    public GameObject SmallItemPositionGameObject;
    public GameObject ToolPosition;
    
    public TextMeshProUGUI UImoneytext;
    public TextMeshProUGUI UIlevel;
    public TextMeshProUGUI UIupgradePoints;

    public int CurrentLevel;

    public GameManager gameManager;
    public float Money;
    public static bool RaycastAllowed;

    public Action<float> OnMoneyChanged;

    public GameData gameData;

    public int UpgradePoints;
    public void AddMoney(float money)
    {
        Money += money;
        Money = (float)Math.Round(Money, 2);

        UpdateBalance(Money);
        OnMoneyChanged?.Invoke(money);
        UpdateUI();

        Statistic.instance.MoneyEarned += (int)money;
        Statistic.instance.Money = (int)Money;

    }
    public bool RemoveMoney(float money)
    {
        if (Money - money >= 0)
        {
            Money -= money;
            Money = (float)Math.Round(Money, 2);
            UpdateUI();
            UpdateBalance(Money);

            Statistic.instance.MoneySpent += (int)money;
            Statistic.instance.Money = (int)Money;

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
        UIlevel.text = $"Level: {CurrentLevel}";
        UIupgradePoints.text = $"Upgrade Points: {UpgradePoints}";
    }

    public object SaveData()
    {
        return gameData;
    }
    public void UpdateBalance(float balance)
    {
        gameData.Balance = balance;
    }
    public void LoadData(string Data)
    {
        GameData loadedData = JsonUtility.FromJson<GameData>(Data);

        if (loadedData != null)
        {
            gameData = loadedData;
            Money = loadedData.Balance;
            UpgradePoints = loadedData.UpgradePoints;
            UpdateUI();
        }
        else
        {
            gameData = new GameData(500,1);

        }

    }
}
