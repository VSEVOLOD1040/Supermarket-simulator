using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeNode : MonoBehaviour
{
    PlayerScript Player;
    public bool IsEnabled = false;
    public List<UpgradeNode> RequiredNodes = new List<UpgradeNode>();

    public int UpgradeCost = 1;
    
    public UnityEvent OnUpgrade;

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
    }
}
