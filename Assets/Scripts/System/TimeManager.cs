using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool isShopOpen = false;
    public float CurrentTime;
    public float TimeSpeed = 1;

    public Action OnWorkDayStart;
    public Action OnWorkDayEnd;

    float StartWorkTime;
    float EndWorkTime;

    public Dictionary<float, Action> Events = new Dictionary<float, Action>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (isShopOpen)
        //{
            CurrentTime += Time.deltaTime*(TimeSpeed/60);
        //}

        if (CurrentTime >= StartWorkTime && isShopOpen == false)
        {
            StartWorkDay();
        }
        if (CurrentTime >= EndWorkTime && isShopOpen == true)
        {
            EndWorkDay();
        }
    }

    public void OpenShop()
    {
        isShopOpen = true;
    }

    public void StartWorkDay()
    {

        OnWorkDayStart?.Invoke();
        OpenShop();
    }

    public void EndWorkDay() 
    {
        OnWorkDayEnd?.Invoke();
        isShopOpen = false;
    }

}
