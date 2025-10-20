using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Product", menuName = "Supermarket/Product")]
public class ProductSO : ScriptableObject
{
    public string Name;

    public int Size;
    public GameObject prefab;
    public Sprite image;
    public float Price;
    public float HeighOffset;
}
