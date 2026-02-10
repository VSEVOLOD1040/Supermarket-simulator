using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMessage : MonoBehaviour
{
    public TextMeshProUGUI Text;
    
    static public UIMessage instance;

    public float AmountOfIterations = 300;
    public float TimeBetweenIterations = 0.0001f;
    public float FontSizeChangePerIteration = 0.1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowMessage(string message, float duration = 2)
    {
        StartCoroutine(Message(message, duration));
    }
    public IEnumerator Message(string message, float duration = 2)
    {
        Text.gameObject.SetActive(true);
        Text.text = message;
        Text.fontSize = 0;

        for (int i = 0; i < AmountOfIterations; i++)
        {
            Text.fontSize += FontSizeChangePerIteration;
            yield return new WaitForSeconds(TimeBetweenIterations);
        }

        yield return new WaitForSeconds(duration);

        for (int i = (int)AmountOfIterations; i>0; i--)
        {
            Text.fontSize -= FontSizeChangePerIteration;
            yield return new WaitForSeconds(TimeBetweenIterations);
        }

        Text.text = "";
        Text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
