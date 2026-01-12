using cakeslice;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShopButton : MonoBehaviour
{
    public bool IsActive = false;
    // Start is called before the first frame update

    public static Action OnDayEnded;
    void Start()
    {
        TimeManager.OnWorkDayEnd += ActivateButton;
    }
    private void OnDestroy()
    {
        TimeManager.OnWorkDayEnd -= ActivateButton;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ActivateButton()
    {
        IsActive = true;
        gameObject.GetComponent<cakeslice.Outline>().color = 0;
    }

    private void OnMouseDown()
    {
        if (IsActive)
        {
            OnDayEnded?.Invoke();
            Debug.Log("Day Ended");
        }
    }
}
