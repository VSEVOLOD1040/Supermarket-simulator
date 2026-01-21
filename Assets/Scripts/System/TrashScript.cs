using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{

    public BoxCollider boxCollider;
    public float YPosition = 0.5f;
    public float SpawnInterval = 2f;
    public bool isSpawnWorks = false;
    public float OverLapSphereRadius = 0.3f;

    public GameObject[] TrashPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        isSpawnWorks = true;
        StartCoroutine(SpawnTrash());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTrash()
    {
        while ( isSpawnWorks)
        {
            yield return new WaitForSeconds(SpawnInterval);

            Spawn();

        }
    }

    public void Spawn()
    {
        Vector3 spawnPosition = GetRandomPosition(boxCollider.bounds);
        GameObject trash = TrashPrefabs[Random.Range(0, TrashPrefabs.Length)];
        trash = Instantiate(trash, spawnPosition, Quaternion.identity);

        Collider[] collisions = Physics.OverlapSphere(trash.transform.position, OverLapSphereRadius);

        if (collisions.Length > 1)
        {
            foreach (Collider col in collisions)
            {
                if (col.gameObject != trash && col.gameObject.tag != "Player" && col.gameObject.tag != "Client" && col.gameObject.tag != "Floor")
                {
                    
                    Destroy(trash);
                    Spawn();
                    break;

                }
            }
        }
    }
    public Vector3 GetRandomPosition(Bounds bounds)
    {

        Vector3 result = new Vector3();

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        result.x = x;
        result.y = YPosition;
        result.z = z;

        return result;
    }

}
