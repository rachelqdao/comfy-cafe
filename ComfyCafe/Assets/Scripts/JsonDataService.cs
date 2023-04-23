using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class JsonDataService : IDataService
{
    public bool SaveData<T>(string RelativePath, T data, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;
        //Debug.Log(path);

        try
        {
            if (File.Exists(path))
            {
                // Debug.Log("Player data file exists.");
                return true;
            }
            else
            {
                Debug.Log("Creating new player data file.");
                using FileStream stream = File.Create(path);
                stream.Close();
                File.WriteAllText(path, JsonConvert.SerializeObject(data));
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to create data file due to: {e.Message} {e.StackTrace}");
            return false;
        }
    }

    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        if (!File.Exists(path)) 
        {
            Debug.LogError($"Cannot load file at {path}, file does not exist.");
            throw new FileNotFoundException($"{path} does not exist.");
        }

        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
            throw e;
        }
    }
}
