using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    public static bool isShopOpen = false;

    public int CurrentTimeCheck;
    public float CurrentTime;
    public float TimeSpeed = 1;

    public static Action OnWorkDayStart;
    public static Action OnWorkDayEnd;

    public float StartWorkTime = 0;
    public float EndWorkTime = 600;

    public Dictionary<float, Action> Events = new Dictionary<float, Action>();

    public List<UnityEvent> events = new List<UnityEvent>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(isShopOpen);
        isShopOpen = false;

        CurrentTimeCheck = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShopOpen)
        {

            if ((int)CurrentTime > CurrentTimeCheck + 1)
            {
                CurrentTimeCheck = (int)CurrentTime;
                ActivateEvent();
            }
            CurrentTime += Time.deltaTime * (TimeSpeed / 60);

        }

        //if (CurrentTime > StartWorkTime && isShopOpen == false)
        //{
        //    StartWorkDay()  ;

        //}
        if (CurrentTime >= EndWorkTime && isShopOpen == true)
        {
            EndWorkDay();
        }
        

        if (Input.GetKeyDown(KeyCode.F8))
        {
            Time.timeScale = 5;
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            Time.timeScale = 1;
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

    public void ActivateEvent()
    {
        Debug.Log("Activating Event at time: " + CurrentTime);
        events[(int)CurrentTime]?.Invoke();
    }

    public void TestEventDebug()
    {
               Debug.Log("Event Activated at time: " + CurrentTime);
    }



}
