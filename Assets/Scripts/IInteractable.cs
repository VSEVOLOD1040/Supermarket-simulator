using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickableObject
{
    void PickUp();
    void Drop();
    

}

public enum ItemSize
{
    BigItem, SmallItem, Tool
}
