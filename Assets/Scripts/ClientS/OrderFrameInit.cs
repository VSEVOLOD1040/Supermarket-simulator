using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderFrameInit : MonoBehaviour
{

    public Transform FrameParent;
    public GameObject OrderFramePrefab;

    public TextMeshProUGUI OrderNumberText;

    public void Init(Dictionary<string, int> order)
    {
        OrderNumberText.text = $"Order"; // ????????????
        foreach (var item in order)
        {
            GameObject frame = Instantiate(OrderFramePrefab, FrameParent);
            frame.GetComponent<OrderProductFrame>().Init(item.Key, item.Value);
        }
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
