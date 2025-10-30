using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSlot : MonoBehaviour
{
    public ProductSO Product;
    // Start is called before the first frame update
    void Start()
    {
        Product = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetProduct(ProductSO NewProduct)
    {
        Product = NewProduct;
        GameObject product = Instantiate(NewProduct.prefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + NewProduct.HeighOffset, gameObject.transform.position.z), NewProduct.prefab.transform.rotation);
        product.transform.SetParent(gameObject.transform);
    }
}
