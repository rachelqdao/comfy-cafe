using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.IO;
using TMPro;
using UnityEditor;

public class TableManager : MonoBehaviour
{

    PlayerData data;
    private PlayerData playerData = new PlayerData();
    private IDataService dataService = new JsonDataService();
    private bool EncryptionEnabled;

    public Dictionary<int, bool> tableAvailability;
    public int numTables;
    public bool[] queueAvailability;
    private int numQueue = 5;

    public GameObject[] tables; 
    public int numOwnedTables;

    // Start is called before the first frame update
    void Start()
    {   
        SerializeJson();
        tableAvailability = new Dictionary<int, bool>();

        configureTableManager();
    }


    public void configureTableManager() {
        
        // Read JSON into dictionary
        getOwnedTables();
        Debug.Log("Num owned tables" + tableAvailability.Count);

        int ownedTableCounter = 0;
        foreach(KeyValuePair<int, bool> entry in tableAvailability) {
            if (entry.Value == false) {
                tables[entry.Key].SetActive(false);
            } else {
                ownedTableCounter = ownedTableCounter + 1;
            }
        }

        Debug.Log("OWNED TABLE OCOUNTER: " + ownedTableCounter);

        numOwnedTables = ownedTableCounter;
        /*
        int counter = 0;
        tablesAvailable = new int[numOwnedTables];

        for (int i = 0; i < ownedTables.Length; i++) {
            if (ownedTables[i] == false) {
                tables[i].SetActive(false);
            } else {
                Debug.Log("table " + i + "available");
                tablesAvailable[counter] = i;
                counter = counter + 1;
            }
        }
            
        tableAvailability = new bool[numOwnedTables];
        Array.Fill(tableAvailability, true);
*/
        queueAvailability = new bool[] {true, true, true, true, true};
    }

    public void showTable() {

    }

    public int checkTableAvailability() {
        foreach(KeyValuePair<int, bool> entry in tableAvailability) {
            if (entry.Value == true) {
                tableAvailability[entry.Key] = false;
                return entry.Key;
            } 
        }
        return -1;
        /*
        
        for (int i = 0; i < numOwnedTables; i++) {
            if (tableAvailability[i] == true) {
                tableAvailability[i] = false;
                return 1;
                // return tablesAvailable[i];
            }
        }
        
        return -1;
        */
    }

    public int checkQueueAvailability() {
        for (int i = 0; i < numQueue; i++) {
            if (queueAvailability[i] == true) {
                queueAvailability[i] = false;
                return i;
            }
        }

        return -1;
    }

    public int checkQueuePositionBefore(int currentIndex) {
        if (queueAvailability[currentIndex - 1] == true) {

            queueAvailability[currentIndex - 1] = false;
            queueAvailability[currentIndex] = true;
            
            return currentIndex - 1;
        } else {
            return currentIndex;
        }
    }

    public void getOwnedTables() {

        if (data.items["table1"].owned == true) {
            tableAvailability.Add(0, true);
        } else {
            tableAvailability.Add(0, false);
        }

        if (data.items["table2"].owned == true) {
            tableAvailability.Add(1, true);
        } else {
            tableAvailability.Add(1, false);
        }

        if (data.items["table3"].owned == true) {
            tableAvailability.Add(2, true);
        } else {
            tableAvailability.Add(2, false);
        }

        if (data.items["table4"].owned == true) {
            tableAvailability.Add(3, true);
        } else {
            tableAvailability.Add(3, false);
        }

        if (data.items["table5"].owned == true) {
            tableAvailability.Add(4, true);
        } else {
            tableAvailability.Add(4, false);
        }

        if (data.items["table6"].owned == true) {
            tableAvailability.Add(5, true);
        } else {
            tableAvailability.Add(5, false);
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
