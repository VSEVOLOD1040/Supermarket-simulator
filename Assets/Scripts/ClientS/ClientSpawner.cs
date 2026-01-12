using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    public GameObject ClientPrefab;
    public Transform SpawnPoint;
    public float SpawnInterval = 5f;
    public float TimeToEndClientSpawn = 600f;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnClient");
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    Instantiate(ClientPrefab, SpawnPoint.position, Quaternion.identity);
        //}


    }

    IEnumerator SpawnClient()
    {

        print("Client Spawner Started");
        while (true)
        {
            yield return new WaitForSeconds(SpawnInterval);

            if (TimeManager.isShopOpen)
            {
                Instantiate(ClientPrefab, SpawnPoint.position, Quaternion.identity);

            }
        }
    }
}
