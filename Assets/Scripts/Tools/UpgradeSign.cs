using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        return IsActivated;
    }

    public void LoadData(object data)
    {

        Debug.Log("Loading UpgradeSign Data");
        //if ((bool)data == true)
        //{
        //    Activate();


        //}
    }
}
