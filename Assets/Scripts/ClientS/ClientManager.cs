using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{

    public static ClientManager instance;
    public TrashScript trashScript;

    public int maxProductAmount = 10;


    public float Rating = 100;
    public float MaxRating = 100;
    public float RatingTrashDecreaseRate = 5f;

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
        //DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Rating = MaxRating;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalculateRating()
    {
        int TrashCount = trashScript.GetTrashCount();
        Rating = MaxRating - TrashCount * RatingTrashDecreaseRate;
        if (Rating < 0)
        {
            Rating = 0;
        }

       

        GameObject.FindAnyObjectByType<UIRAting>().ChangeUpdatedRatingOnUI(Rating);

        return Rating;
    }

    public int GetMaxProductAmount()
    {

        int Rating = (int)CalculateRating();

        int max_products = 0;
        if (WillClientEnter())
        {
            max_products = (int)(Rating / maxProductAmount);

        }

        Debug.Log($"Rating: {Rating}, Max Products: {max_products}");

        return max_products;


        
    }

    public bool WillClientEnter()
    {
        int Rating = (int)CalculateRating();
        int randomValue = Random.Range(0, (int)MaxRating);
        if (randomValue < Rating)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
