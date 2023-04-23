using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Newtonsoft.Json;
using System;
using System.IO;
using TMPro;
using UnityEditor;

public class CurrencyScript : MonoBehaviour
{
    public TextMeshProUGUI coinBalance;
    public TextMeshProUGUI diamondBalance;

    PlayerData data;
    private PlayerData playerData = new PlayerData();
    private IDataService dataService = new JsonDataService();
    private bool EncryptionEnabled;

    // Start is called before the first frame update
    void Start()
    {
        SerializeJson();
    }

    // Update is called once per frame
    void Update()
    {
        SerializeJson();
    }

    public void addCoins(int amount) {
        
    }

    public void subtractCoins(int amounts) {

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
