using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeNode : MonoBehaviour
{
    PlayerScript Player;
    public bool IsEnabled = false;
    public List<UpgradeNode> RequiredNodes = new List<UpgradeNode>();

    public int UpgradeCost = 1;
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
        Debug.Log("Open()");
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

        Player.UpgradePoints -= UpgradeCost;
        IsEnabled = true;

        gameObject.GetComponent<Image>().color = Color.green;   
    }
}
