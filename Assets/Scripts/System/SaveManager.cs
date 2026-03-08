using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class SaveManager : MonoBehaviour
{
    public string filePath;


    async private void Start()
    {
        

        filePath = Application.persistentDataPath + "/Save.json";

        Debug.Log(Application.persistentDataPath);

        await Task.Yield();
        await Task.Delay(100);

        await Load();

    }

    async public Task Save()
    {

        List<string> keys = new List<string>();
        List<string> values = new List<string>();

        //Dictionary<string, object> saveData = new Dictionary<string, object>();
        MonoBehaviour[] saveableObjects = GameObject.FindObjectsOfType<MonoBehaviour>(true);
        //Debug.Log(saveableObjects.Length);
        foreach (var saveable in saveableObjects)
        {
            if (saveable is ISaveble saveObj)
            {
                if (saveable.transform.parent )
                {
                    //if (saveable.gameObject.activeInHierarchy)
                    //{
                        keys.Add($"{saveable.transform.parent.name}_{saveable.name}");
                        values.Add(JsonUtility.ToJson(saveObj.SaveData()));
                        //saveData[$"{saveable.transform.parent.name}_{saveable.name}"] = saveObj.SaveData();
                    //}


                }
                else
                {
                    //if (saveable.gameObject.activeInHierarchy) { 
                        keys.Add(saveable.name);
                        values.Add(JsonUtility.ToJson(saveObj.SaveData()));
                    //}


                }
            }

            
        }



        string json = JsonUtility.ToJson(new SerializationWrapper(keys, values));


        await Task.Run(() =>
        {
            File.WriteAllText(filePath, json);
        });

    }

    async public Task Load()
    {
        Debug.Log("Loading save data...");
        Dictionary<string, string> saveData = new Dictionary<string, string>();

        string json = await Task.Run(() => File.ReadAllText(filePath));
        Debug.Log(File.ReadAllText(filePath));
        if (string.IsNullOrEmpty(json))
        {
            //Debug.Log("No save data found.");
            return;
        }

        saveData = JsonUtility.FromJson<SerializationWrapper>(json).ToDictionary();

        //Debug.Log("=====================");

        foreach (var data  in saveData)
        {
            //Debug.Log(data);
        }
        //Debug.Log("=====================");


        MonoBehaviour[] saveableObjects = GameObject.FindObjectsOfType<MonoBehaviour>(true);

        foreach (MonoBehaviour saveable in saveableObjects)
        {
            

            if (saveable is ISaveble saveObj)
            {
                string key = "";
                if (saveable.transform.parent)
                {
                    key = $"{saveable.transform.parent.name}_{saveable.name}";
                }
                else
                {
                    key = saveable.name;

                }
                if (saveData.ContainsKey(key))
                {
                    ////Debug.Log(saveable.name +saveData[key].GetType());
                    saveObj.LoadData(saveData[key]);
                }

            }
        }
    }

    float timer = 0f;
    bool isLoaded = false;
    //private async void Update()
    //{
    //    //if (Input.GetKeyDown(KeyCode.F5))
    //    //{
    //    //    await Save();
    //    //    //Debug.Log("Game Saved");
    //    //}

    //    //if (Input.GetKeyDown(KeyCode.F6))
    //    //{
    //    //    await Load();
    //    //    //Debug.Log("Game Loaded");
    //    //}

    //    //timer += Time.deltaTime;
    //    //if (timer >= 5f && isLoaded == false)
    //    //{
    //    //    Load();
    //    //    isLoaded = true;
    //    //}
        
    //}


}

[System.Serializable]
public class SerializationWrapper
{
    public List<string> keys = new();
    public List<string> values = new();

    public SerializationWrapper(List<string> Keys, List<string> Values)
    {
        //foreach (var kvp in dict)
        //{
        //    keys.Add(kvp.Key);
        //    values.Add(JsonUtility.ToJson(kvp.Value));
        //}

        keys = Keys;
        values = Values;
    }

    public Dictionary<string, string> ToDictionary()
    {
        var dict = new Dictionary<string, string>();

        for (int i = 0; i < keys.Count; i++)
        {
            dict[keys[i]] = values[i];
        }

        return dict;
    }

    
}
