using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TimeManager timeManager;

    public TextMeshProUGUI timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeManager = GameObject.FindObjectOfType<TimeManager>();

    }

    // Update is called once per frame
    void Update()
    {
        int hours = Mathf.FloorToInt(timeManager.CurrentTime); 
        int minutes = Mathf.FloorToInt((timeManager.CurrentTime - hours) * 60); 

        timeText.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }
}
