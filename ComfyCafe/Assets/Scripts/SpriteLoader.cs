using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEditor;


public class SpriteLoader : MonoBehaviour
{
    private PlayerData playerData = new PlayerData();
    private IDataService dataService = new JsonDataService();
    private bool EncryptionEnabled;

    public TextMeshProUGUI coinBalance, diamondBalance;
    public SpriteRenderer windowSR, wallhangingSR, table1SR, table2SR, table3SR, table4SR, table5SR, table6SR, oven1SR, oven2SR, oven3SR, oven4SR, oven5SR, oven6SR;

    // Start is called before the first frame update
    void Start()
    {
        SerializeJson();
    }

    public void SerializeJson()
    {
        if (dataService.SaveData("/playerData.json", playerData, EncryptionEnabled))
        {
            try
            {
                PlayerData data = dataService.LoadData<PlayerData>("/playerData.json", EncryptionEnabled);
                // render text for coin and diamond balances
                // Debug.Log(data.coins.ToString());
                coinBalance.SetText(data.coins.ToString());
                diamondBalance.SetText(data.diamonds.ToString());
                // check and render sprite for certain item types
                // window
                windowSR.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/" + data.currentItems["window"] + ".png");
                Debug.Log("Assets/Sprites/" + data.currentItems["window"] + ".png");
                // wallhangings 
                wallhangingSR.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/" + data.currentItems["wallhanging"] + ".png");
                // can be converted to allow different table and oven variants in the future
                // tables
                /*
                if (data.currentItems["table2"] != "table")
                {
                    table2SR.enabled = false;
                }
                if (data.currentItems["table3"] != "table")
                {
                    table3SR.enabled = false;
                }
                if (data.currentItems["table4"] != "table")
                {
                    table4SR.enabled = false;
                }
                if (data.currentItems["table5"] != "table")
                {
                    table5SR.enabled = false;
                }
                if (data.currentItems["table6"] != "table")
                {
                    table6SR.enabled = false;
                }
                // ovens
                if (data.currentItems["oven2"] != "oven")
                {
                    oven2SR.enabled = false;
                }
                if (data.currentItems["oven3"] != "oven")
                {
                    oven3SR.enabled = false;
                }
                if (data.currentItems["oven4"] != "oven")
                {
                    oven4SR.enabled = false;
                }
                if (data.currentItems["oven5"] != "oven")
                {
                    oven5SR.enabled = false;
                }
                if (data.currentItems["oven6"] != "oven")
                {
                    oven6SR.enabled = false;
                }
                */
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
