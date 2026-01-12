using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour
{
    public static Statistic instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public int Money;
}
