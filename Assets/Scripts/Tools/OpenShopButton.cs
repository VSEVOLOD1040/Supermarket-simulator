using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenShopButton : MonoBehaviour
{
    public GameManager gameManager;



    void OnMouseDown()
    {
        gameManager.OpenShop();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
