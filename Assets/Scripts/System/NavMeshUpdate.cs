using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshUpdate : MonoBehaviour
{
    public NavMeshSurface NavMesh;
    // Start is called before the first frame update
    void Start()
    {
        NavMesh= gameObject.GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        NavMesh.BuildNavMesh();
    }
}
