using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class SaveManager : MonoBehaviour
{
    public string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/Save.json";
    }

    public void Save()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();
        ISaveble[] saveableObjects = GameObject.FindObjectsOfType<MonoBehaviour>(true) as ISaveble[];

        foreach (ISaveble saveable in saveableObjects)
        {
            //string key = saveable.GetType().ToString() + "_" + saveable.GetHashCode();
            
            saveData[saveable.GetType().FullName] = saveable.SaveData();
            Debug.Log(saveable.GetType().FullName);
        }

        string json = JsonUtility.ToJson(saveData);
    }

    public void Load()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();
        saveData = JsonUtility.FromJson<Dictionary<string, object>>(File.ReadAllText(filePath));

        ISaveble[] saveableObjects = GameObject.FindObjectsOfType<MonoBehaviour>(true) as ISaveble[];

        foreach (ISaveble saveable in saveableObjects)
        {
            string key = saveable.GetType().FullName;
            if (saveData.ContainsKey(key))
            {
                saveable.LoadData(saveData[key]);
            }
        }
    }

}