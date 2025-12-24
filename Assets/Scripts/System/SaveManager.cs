using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class SaveManager : MonoBehaviour
{
    public string filePath;


    private void Start()
    {
        filePath = Application.persistentDataPath + "/Save.json";

        Debug.Log(Application.persistentDataPath);

    }

    public void Save()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();
        MonoBehaviour[] saveableObjects = GameObject.FindObjectsOfType<MonoBehaviour>(true);
        Debug.Log(saveableObjects.Length);
        foreach (var saveable in saveableObjects)
        {
            if (saveable is ISaveble saveObj)
            {

                saveData[saveable.name] = saveObj.SaveData();
                Debug.Log(saveable.name);
            }

            
        }



        string json = JsonUtility.ToJson(new SerializationWrapper(saveData));
        File.WriteAllText(filePath, json);
    }

    public void Load()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();
        
        string json = File.ReadAllText(filePath);
        saveData = JsonUtility.FromJson<SerializationWrapper>(json).ToDictionary();

        MonoBehaviour[] saveableObjects = GameObject.FindObjectsOfType<MonoBehaviour>(true);

        foreach (MonoBehaviour saveable in saveableObjects)
        {
            

            if (saveable is ISaveble saveObj)
            {
                string key = saveable.name;
                if (saveData.ContainsKey(key))
                {
                    saveObj.LoadData(saveData[key]);
                }

            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Save();
            Debug.Log("Game Saved");
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            Load();
            Debug.Log("Game Loaded");
        }
    }


}

[System.Serializable]
public class SerializationWrapper
{
    public List<string> keys = new();
    public List<string> values = new();

    public SerializationWrapper(Dictionary<string, object> dict)
    {
        foreach (var kvp in dict)
        {
            keys.Add(kvp.Key);
            values.Add(JsonUtility.ToJson(kvp.Value));
        }
    }

    public Dictionary<string, object> ToDictionary()
    {
        var dict = new Dictionary<string, object>();

        for (int i = 0; i < keys.Count; i++)
        {
            dict[keys[i]] = JsonUtility.FromJson<object>(values[i]);
        }

        return dict;
    }

    
}
