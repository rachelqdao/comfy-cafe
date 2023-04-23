using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.IO;
using TMPro;
using UnityEditor;

public class CurrencyManager : MonoBehaviour
{
    public TextMeshProUGUI coinBalanceText;
    public TextMeshProUGUI diamondBalanceText;

    PlayerData data;
    private PlayerData playerData = new PlayerData();
    private IDataService dataService = new JsonDataService();
    private bool EncryptionEnabled;

    public int coinBalance;
    public int diamondBalance;

    // Start is called before the first frame update
    void Start()
    {
        SerializeJson();
        coinBalance = data.coins;
        diamondBalance = data.diamonds;
        Debug.Log("coin balance: " + coinBalance);
        Debug.Log("diamond balance: " + diamondBalance);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addCoins(int amount) {
        string path = Application.persistentDataPath + "/playerData.json";
        string json = File.ReadAllText(path);
        JObject jObject = JsonConvert.DeserializeObject(json) as JObject;
        JToken jToken = jObject.SelectToken("coins");
        jToken.Replace(data.coins + amount);
        string updatedJsonString = jObject.ToString();
        File.WriteAllText(path, updatedJsonString);

        SerializeJson();
        coinBalanceText.SetText(data.coins.ToString());
    }

    public void subtractCoins(int amount) {
        // Debug.Log("Subtracting " + amount + "coins");
        string path = Application.persistentDataPath + "/playerData.json";
        string json = File.ReadAllText(path);
        JObject jObject = JsonConvert.DeserializeObject(json) as JObject;
        JToken jToken = jObject.SelectToken("coins");
        jToken.Replace(data.coins - amount);
        string updatedJsonString = jObject.ToString();
        File.WriteAllText(path, updatedJsonString);

        SerializeJson();
        coinBalanceText.SetText(data.coins.ToString());
    }

    public void addDiamonds(int amount) {

    }

    public void subtractDiamonds(int amount) {

    }

    public void SerializeJson()
    {
        if (dataService.SaveData("/playerData.json", playerData, EncryptionEnabled))
        {
            try
            {
                data = dataService.LoadData<PlayerData>("/playerData.json", EncryptionEnabled);
                // render text for coin and diamond balances
                // Debug.Log(data.recipes["americano"].earnings.ToString());
            }
            catch (Exception e)
            {
                Debug.LogError($"Could not read file. {e.Message} {e.StackTrace}");
            }
        }
        else
        {
            Debug.LogError("Could not create new data file.");
        }
    }
}
