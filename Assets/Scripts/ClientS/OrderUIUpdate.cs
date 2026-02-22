using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUIUpdate : MonoBehaviour
{

    public Transform OrdersTransform;
    public GameObject OrderPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI(List<Dictionary<string, int>> Orders)
    {
        foreach (Transform child in OrdersTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (var order in Orders)
        {
            GameObject orderFrame = Instantiate(OrderPrefab, OrdersTransform);
            orderFrame.GetComponent<OrderFrameInit>().Init(order);
        }



    }
}
