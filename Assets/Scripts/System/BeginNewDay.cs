using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginNewDay : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ToMenu()
    {
        GameObject.FindAnyObjectByType<SaveManager>().Save();

        SceneManager.LoadScene(0);
    }
}
