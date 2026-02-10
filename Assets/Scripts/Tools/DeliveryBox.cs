using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBox : Box
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
    }
}
