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

    public void Save(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(filePath, json);
        Debug.Log("Saved: " + filePath);
    }

    public GameData Load()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            return data;
        }

        return null;
    }

}