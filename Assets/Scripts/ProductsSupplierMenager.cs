using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsSupplierMenager : MonoBehaviour
{
    public GameObject UI;
    public List<ProductSO> available_products;
    public GameObject box_prefab;
    public Transform position_where_this_script_must_spawn_a_box;
    private void OnMouseDown()
    {
        Init();
        SwitchUI(true);

    }

    void Init()
    {
        Debug.LogWarning("We are sorry, but initialization function isn't completed yet.");
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
        if (state)
        {
            UI.SetActive(true);
            EventBus.MouseLock.Invoke(false);
            EventBus.CameraLookEnabled.Invoke(false);

        }
        else
        {
            UI.SetActive(false);
            EventBus.MouseLock.Invoke(true);
            EventBus.CameraLookEnabled.Invoke(true);
        }
    }

    public void Order(ProductSO product)
    {
        GameObject box = Instantiate(box_prefab, position_where_this_script_must_spawn_a_box.position, Quaternion.identity);
        BoxScript boxScript = box.GetComponent<BoxScript>();
        boxScript.Init(product);
    }
}
