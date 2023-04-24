using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.IO;
using TMPro;
using UnityEditor;

public class CustomerClosenessManager : MonoBehaviour
{
    PlayerData data;
    private PlayerData playerData = new PlayerData();
    private IDataService dataService = new JsonDataService();
    private bool EncryptionEnabled;

    public GameObject[] customerPanels;
    public string[] customerNames = {"shiba", "havanese", "rilakkuma", "korilakkuma", "bunny", "calico", "panda"}; 

    // Start is called before the first frame update
    void Start()
    {
        SerializeJson();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addHeart(int customerID) {
        Debug.Log("CustomerID: " + customerID);

        // get the name of the customer based on ID

        string customerName = customerNames[customerID];
        string tokenPath = "customers." + customerName;

        string path = Application.persistentDataPath + "/playerData.json";
        string json = File.ReadAllText(path);
        JObject jObject = JsonConvert.DeserializeObject(json) as JObject;
        JToken jToken = jObject.SelectToken(tokenPath);
        jToken.Replace(data.customers[customerName] + 1);
        string updatedJsonString = jObject.ToString();
        File.WriteAllText(path, updatedJsonString);

        SerializeJson();
        GameObject customerPanel = customerPanels[customerID].transform.GetChild(0).gameObject;
        TextMeshProUGUI customerPanelText = customerPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        customerPanelText.SetText(data.customers[customerName].ToString());
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
