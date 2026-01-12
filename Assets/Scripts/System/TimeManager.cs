using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static bool isShopOpen = false;
    public float CurrentTime;
    public float TimeSpeed = 1;

    public static Action OnWorkDayStart;
    public static Action OnWorkDayEnd;

    public float StartWorkTime = 0;
    public float EndWorkTime = 600;

    public Dictionary<float, Action> Events = new Dictionary<float, Action>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShopOpen)
        {
            CurrentTime += Time.deltaTime*(TimeSpeed/60);
        }

        if (CurrentTime > StartWorkTime && isShopOpen == false)
        {
            StartWorkDay()  ;

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
