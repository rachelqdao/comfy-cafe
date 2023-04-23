using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Newtonsoft.Json;
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

    // Start is called before the first frame update
    void Start()
    {
        SerializeJson();

        // read data to get owned tables
        ownedTables = new bool[6];
        getOwnedTables();
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
        string tableName = "table" + i;
        Debug.Log("Table name: " + tableName);
        
        // subtract money from balance if enough money

            // write the new balance back to json

            // gray out the button

            // pass stuff to table manager to show up in restaurant

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
