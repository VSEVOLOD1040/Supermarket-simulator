using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class UpgradeSign : MonoBehaviour, IInteractable, ISaveble
{

    public GameObject ObjectToActivate;
    public PlayerScript player;

    public TextMeshProUGUI text_Name;
    public TextMeshProUGUI text_Price;

    public bool IsActivated = false;

    public float Price;
    public string Name;

    public UpgradeSignData gameData;
    public void Interact(GameObject interactor)
    {
        if (player.RemoveMoney(Price))
        {
            Activate();
        }
    }

    public void Activate()
    {
        ObjectToActivate.SetActive(true);
        gameObject.SetActive(false);
        IsActivated = true;
        gameData.IsActivated = true;
    }
    public void Init()
    {
        text_Name.text = Name;
        text_Price.text = Price.ToString()+"$";
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerScript>();
        ObjectToActivate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public object SaveData()
    {
        return gameData;
    }

    public void LoadData(string data)
    {

        UpgradeSignData loadedData = JsonUtility.FromJson<UpgradeSignData>(data);
        if (loadedData != null)
        {
            gameData = loadedData;
            if (gameData.IsActivated)
            {
                Activate();
            }
        }
        else
        {
            gameData = new UpgradeSignData(false);
            //Debug.Log("No data found for UpgradeSign: " + gameObject.name);
        }
        
    }
}

[Serializable]
public class UpgradeSignData
{
    public bool IsActivated;

    public UpgradeSignData(bool isActivated)
    {
        IsActivated = isActivated;
    }
}
