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


    public GameObject SetProduct(ProductSO NewProduct)
    {
        Product = NewProduct;
        GameObject product = Instantiate(NewProduct.prefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + NewProduct.HeighOffset, gameObject.transform.position.z), Quaternion.identity);
        product.transform.SetParent(gameObject.transform, false);
        product.transform.localRotation = Quaternion.identity;
        product.transform.localPosition = Vector3.zero + new Vector3(0, NewProduct.HeighOffset, 0);
        return product;
    }
}
