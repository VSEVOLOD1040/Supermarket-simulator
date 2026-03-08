using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeNode : MonoBehaviour, ISaveble
{
    PlayerScript Player;
    public bool IsEnabled = false;
    public List<UpgradeNode> RequiredNodes = new List<UpgradeNode>();

    public int UpgradeCost = 1;
    
    public UnityEvent OnUpgrade;

    public UpgradeNodeData gameData;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerScript>();
        gameObject.GetComponent<Button>().onClick.AddListener(Open);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        if (Player.UpgradePoints - UpgradeCost >= 0)
        {
            
            foreach (UpgradeNode node in RequiredNodes)
            {
                if (node.IsEnabled == false)
                {
                    return;
                }
            }
            
                
        }
        else
        {
            return;
        }



        OnUpgrade?.Invoke();

        Player.UpgradePoints -= UpgradeCost;
        IsEnabled = true;
        Player.UpdateUI();
        gameObject.GetComponent<Image>().color = Color.green;   

        gameData.IsActivated = true;

        Debug.Log("Upgrade activated: " + gameObject.name);
    }

    public void Activate()
    {
        OnUpgrade?.Invoke();//можливо треба зберігати це окремо

        IsEnabled = true;
        Player.UpdateUI();
        gameObject.GetComponent<Image>().color = Color.green;

    }

    public object SaveData()
    {
        return gameData;
    }

    public void LoadData(string data)
    {

        UpgradeNodeData loadedData = JsonUtility.FromJson<UpgradeNodeData>(data);
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
            gameData = new UpgradeNodeData(false);
            //Debug.Log("No data found for UpgradeNode: " + gameObject.name);
        }
    }
}

[Serializable]
public class UpgradeNodeData
{
    public bool IsActivated;

    public UpgradeNodeData(bool isActivated)
    {
        IsActivated = isActivated;
    }
}

