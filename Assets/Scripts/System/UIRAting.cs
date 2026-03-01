using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRAting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TextMeshProUGUI Text;
    public Image image;


    public void ChangeUpdatedRatingOnUI(float rating)
    {
        Text.text = "Rating: " + rating.ToString("0"); // ?? 


        float angle = Mathf.Lerp(180f, 0f, rating / 100f);
        if (angle > 0)
        {
            angle = -angle;
        }
        image.transform.rotation = Quaternion.Euler(0f, 0f, angle);


    }

}
