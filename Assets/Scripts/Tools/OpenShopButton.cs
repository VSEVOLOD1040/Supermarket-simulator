using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenShopButton : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSO audio;


    void OnMouseDown()
    {
        gameManager.OpenShop();
        GameObject.Find("GAME_MANAGER").GetComponent<TimeManager>().StartWorkDay();

        AudioManager.instance.PlayAudio(audio, transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
