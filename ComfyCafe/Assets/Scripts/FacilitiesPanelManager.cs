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

public class FacilitiesPanelManager : MonoBehaviour
{
    PlayerData data;
    private PlayerData playerData = new PlayerData();
    private IDataService dataService = new JsonDataService();
    private bool EncryptionEnabled;

    public GameObject[] tablePanels;
    public Button[] tableButtons;
    public bool[] ownedTables;
    public int clickedButtonIndex;

    public CurrencyManager currencyManager;
    public TableManager tableManager;


    // Start is called before the first frame update
    void Start()
    {
        SerializeJson();

        // Get reference to CurrencyManager script
        currencyManager = GameObject.FindGameObjectWithTag("CurrencyManager").GetComponent<CurrencyManager>();

        // Get reference to tableManager script
        tableManager = GameObject.FindGameObjectWithTag("TableManager").GetComponent<TableManager>();

        // Read data to get owned tables
        ownedTables = new bool[6];
        getOwnedTables();

        // Configure buttons
        addTableButtonListeners();
        disableButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getOwnedTables() {
        if (data.items["table1"].owned == true) {
            ownedTables[0] = true;
        } else {
            ownedTables[0] = false;
        }

        if (data.items["table2"].owned == true) {
            ownedTables[1] = true;
        } else {
            ownedTables[1] = false;
        }

        if (data.items["table3"].owned == true) {
            ownedTables[2] = true;
        } else {
            ownedTables[2] = false;
        }

        if (data.items["table4"].owned == true) {
            ownedTables[3] = true;
        } else {
            ownedTables[3] = false;
        }

        if (data.items["table5"].owned == true) {
            ownedTables[4] = true;
        } else {
            ownedTables[4] = false;
        }

        if (data.items["table6"].owned == true) {
            ownedTables[5] = true;
        } else {
            ownedTables[5] = false;
        }
    }

    public void disableButtons() {
        for (int i = 0; i < ownedTables.Length; i++) {
            if (ownedTables[i] == true) {
                tableButtons[i].interactable = false;
            }
        }
    }

    public void addTableButtonListeners() {
        for (int i = 0; i < tableButtons.Length; i++) {
            int x = i;
            tableButtons[i].onClick.RemoveAllListeners();
            tableButtons[i].onClick.AddListener(() => buyTable(x));
        }
    }

    public void buyTable(int i) {
        int j = i + 1;
        string tableName = "table" + j;
        Debug.Log("Table name: " + tableName);
        

        // Check if enought money
        if (data.coins > data.items[tableName].cost) {
            // subtract money from balance if enough money

            // write the new balance back to json
            currencyManager.subtractCoins(data.items[tableName].cost);

            // gray out the button
            tableButtons[i].interactable = false;

            // pass stuff to table manager to show up in restaurant?
            Debug.Log("Buying table");
            string tokenPath = "items." + tableName + ".owned";
            
            string path = Application.persistentDataPath + "/playerData.json";
            string json = File.ReadAllText(path);
            JObject jObject = JsonConvert.DeserializeObject(json) as JObject;
            JToken jToken = jObject.SelectToken(tokenPath);
            jToken.Replace(true);
            string updatedJsonString = jObject.ToString();
            File.WriteAllText(path, updatedJsonString);
            SerializeJson();

            // TODO: reset table manager to show new table + make the table available
            // tableManager.reconfigureTableManager();
            tableManager.tableAvailability[i] = true;
            tableManager.numOwnedTables = tableManager.numOwnedTables + 1;
            tableManager.tables[i].SetActive(true);

        }       
    }

    public void SerializeJson() {

        if (dataService.SaveData("/playerData.json", playerData, EncryptionEnabled))
        {
            try
            {
                data = dataService.LoadData<PlayerData>("/playerData.json", EncryptionEnabled);
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
