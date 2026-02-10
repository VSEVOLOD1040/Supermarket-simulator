using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderEmptyBox : MonoBehaviour
{
    public PlayerScript player;
    public float Price;

    public GameObject box_prefab;
    public Transform position_where_this_script_must_spawn_a_box;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Order()
    {

        if (player.RemoveMoney(Price))
        {
            GameObject box = Instantiate(box_prefab, position_where_this_script_must_spawn_a_box.position, Quaternion.identity);
            box.transform.SetParent(GameObject.Find("BOXES").transform);
            BoxScript boxScript = box.GetComponent<BoxScript>();
            //boxScript.Init(product, marketData.GetBatchSize(product));

            

            //Statistic.instance.AddProduct(product.Name, marketData.GetBatchSize(product));
        }

    }
}
