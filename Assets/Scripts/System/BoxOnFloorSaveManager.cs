using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using static BoxShelfSlot;

public class BoxOnFloorSaveManager : MonoBehaviour, ISaveble
{

    public List<BoxOnFloorData> BoxData;
    public BoxOnFloorSaveData saveData;
    public MarketDataSO marketData;
    public void LoadData(string data)
    {
        BoxOnFloorSaveData loadedData = JsonUtility.FromJson<BoxOnFloorSaveData>(data);

        if (loadedData != null)
        {
            saveData = loadedData;

            foreach (BoxOnFloorData boxData in loadedData.boxes)
            {
                Debug.Log($"Loading box: {boxData.productName}, Amount: {boxData.amount}, Position: {boxData.position}, Rotation: {boxData.rotation}");
                GameObject box = Instantiate(Resources.Load<GameObject>("Prefabs/Box"));
                box.transform.SetParent(GameObject.Find("BOXES").transform);

                box.transform.position = boxData.position;
                box.transform.rotation = boxData.rotation;
                ProductSO product = marketData.GetProductByName(boxData.productName);
                box.GetComponent<BoxScript>().Init(product, boxData.amount);
            }


        }
    }

    public object SaveData()
    {
        Debug.Log("Saving boxes on floor...");
        foreach (Transform box in GameObject.Find("BOXES").transform)
        {
            Debug.Log($"Saving box: {box.name}, Position: {box.position}, Rotation: {box.rotation}");
            BoxOnFloorData boxData = InitBoxData(box);
            
            BoxData.Add(boxData);
        }

        saveData.boxes=BoxData;

        return saveData;
    }

    public BoxOnFloorData InitBoxData(Transform box)
    {
        BoxScript boxScript = box.GetComponent<BoxScript>();
        BoxOnFloorData boxData = new BoxOnFloorData(boxScript.product.Name, boxScript.Amount, box.transform.position, box.transform.rotation);
        return boxData;
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

[Serializable]
public class BoxOnFloorData
{
    public string productName;
    public int amount;
    public Vector3 position;
    public Quaternion rotation;
    public BoxOnFloorData(string productName, int amount, Vector3 position, Quaternion rotation)
    {
        this.productName = productName;
        this.amount = amount;
        this.position = position;
        this.rotation = rotation;
    }
}
[Serializable]
public class BoxOnFloorSaveData
{
    public List<BoxOnFloorData> boxes;
    public BoxOnFloorSaveData(List<BoxOnFloorData> boxes)
    {
        this.boxes = boxes;
    }
}