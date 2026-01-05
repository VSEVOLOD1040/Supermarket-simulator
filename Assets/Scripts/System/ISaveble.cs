using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveble
{
    object SaveData();
    void LoadData(string data);
}
