using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Media;
using UnityEngine;
using static UnityEditor.Progress;

public class ProductsSupplierMenager : MonoBehaviour
{
    public GameObject UI;
    public List<ProductSO> available_products;
    public GameObject box_prefab;
    public Transform position_where_this_script_must_spawn_a_box;
    public Transform UIParent;
    public GameObject UIProductPrefab;
    public PlayerScript player;
    public MarketDataSO marketData;
    private void OnMouseDown()
    {
        
        SwitchUI(true);
        
    }

    void Init()
    {
        foreach (ProductSO item in available_products)
        {
            GameObject slot = Instantiate(UIProductPrefab, UIParent);
            slot.GetComponent<ProductSlotSupplyInit>().Init(item, marketData.GetBatchSize(item), marketData.GetPrice(item));
            Debug.Log(slot.name);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SwitchUI(false);

        }
    }

    void SwitchUI(bool state)
    {
        if (UI.activeSelf != state)
        {
            if (state)
            {
                Init();
                UI.SetActive(true);
                EventBus.MouseLock.Invoke(false);
                EventBus.CameraLookEnabled.Invoke(false);

            }
            else
            {
                UI.SetActive(false);
                EventBus.MouseLock.Invoke(true);
                EventBus.CameraLookEnabled.Invoke(true);

                foreach (Transform child in UIParent.Cast<Transform>().ToArray())
                {
                    Destroy(child.gameObject);
                }

            }
        }
        
    }

    public void Order(ProductSO product)
    {
        
        if (player.RemoveMoney(marketData.GetPrice(product)))
        {
            GameObject box = Instantiate(box_prefab, position_where_this_script_must_spawn_a_box.position, Quaternion.identity);
            BoxScript boxScript = box.GetComponent<BoxScript>();
            boxScript.Init(product, marketData.GetBatchSize(product));

            if (product.Price == 0)
            {
                product.Price = marketData.GetPrice(product) / marketData.GetBatchSize(product);
            }
        }
        
    }
}
