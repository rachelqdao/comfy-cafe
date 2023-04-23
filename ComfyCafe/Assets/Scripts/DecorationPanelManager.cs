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

public class DecorationPanelManager : MonoBehaviour
{
    PlayerData data;
    private PlayerData playerData = new PlayerData();
    private IDataService dataService = new JsonDataService();
    private bool EncryptionEnabled;

    public GameObject[] wallhangingPanels;
    public Button[] wallhangingButtons;
    public bool[] ownedWallhangings;
    public int clickedButtonIndex;

    public GameObject[] windowPanels;
    public Button[] windowButtons;
    public bool[] ownedWindows;

    public CurrencyManager currencyManager;
    public SpriteRenderer wallhangingSR, windowSR;
    //public WallhangingManager wallhangingManager;
    //public WindowManager windowManager;

    // Start is called before the first frame update
    void Start()
    {
        SerializeJson();

        // Get reference to CurrencyManager script
        currencyManager = GameObject.FindGameObjectWithTag("CurrencyManager").GetComponent<CurrencyManager>();

        // Get reference to wallhangingManager script
        //wallhangingManager = GameObject.FindGameObjectWithTag("WallhangingManager").GetComponent<WallhangingManager>();
        // Get reference to windowManager script
        //windowManager = GameObject.FindGameObjectWithTag("WindowManager").GetComponent<WindowManager>();

        // Read data to get owned decor
        ownedWallhangings = new bool[4];
        getOwnedWallhangings();

        ownedWindows = new bool[3];
        getOwnedWindows();

        // Configure buttons
        addWallhangingButtonListeners();
        addWindowButtonListeners();
        setOwnedButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getOwnedWallhangings()
    {
        if (data.items["clock"].owned == true)
        {
            ownedWallhangings[0] = true;
        }
        else
        {
            ownedWallhangings[0] = false;
        }

        if (data.items["lights"].owned == true)
        {
            ownedWallhangings[1] = true;
        }
        else
        {
            ownedWallhangings[1] = false;
        }

        if (data.items["painting"].owned == true)
        {
            ownedWallhangings[2] = true;
        }
        else
        {
            ownedWallhangings[2] = false;
        }

        if (data.items["wallhanging"].owned == true)
        {
            ownedWallhangings[3] = true;
        }
        else
        {
            ownedWallhangings[3] = false;
        }
    }

    public void getOwnedWindows()
    {
        if (data.items["window"].owned == true)
        {
            ownedWindows[0] = true;
        }
        else
        {
            ownedWindows[0] = false;
        }

        if (data.items["archedwindow"].owned == true)
        {
            ownedWindows[1] = true;
        }
        else
        {
            ownedWindows[1] = false;
        }

        if (data.items["heartwindow"].owned == true)
        {
            ownedWindows[2] = true;
        }
        else
        {
            ownedWindows[2] = false;
        }
    }

    public void addWallhangingButtonListeners()
    {
        for (int i = 0; i < wallhangingButtons.Length; i++)
        {
            int x = i;
            wallhangingButtons[i].onClick.RemoveAllListeners();

            wallhangingButtons[i].onClick.AddListener(() => wallhangingOnClick(x));
        }
    }

    public void wallhangingOnClick(int i)
    {
        string[] wallhangings = { "clock", "lights", "painting", "wallhanging"};
        string wallhangingName = wallhangings[i];
        Debug.Log("Wallhanging name: " + wallhangingName);

        // buy if unowned
        if (ownedWallhangings[i] == false)
        {
            // Check if enought money
            if (data.coins >= data.items[wallhangingName].cost)
            {
                // subtract money from balance if enough money

                // update ownedWallhangings
                ownedWallhangings[i] = true;

                // write the new balance back to json
                currencyManager.subtractCoins(data.items[wallhangingName].cost);

                GameObject panelCanvas = wallhangingPanels[i].transform.GetChild(0).gameObject;

                GameObject panelCoin = panelCanvas.transform.GetChild(2).gameObject;
                GameObject panelCost = panelCanvas.transform.GetChild(3).gameObject;
                GameObject panelOwned = panelCanvas.transform.GetChild(4).gameObject;

                panelCoin.SetActive(false);
                panelCost.SetActive(false);
                panelOwned.SetActive(true);

                Debug.Log("Buying wallhanging");
                string path1 = Application.persistentDataPath + "/playerData.json";
                string json1 = File.ReadAllText(path1);
                JObject jObject1 = JsonConvert.DeserializeObject(json1) as JObject;
                string tokenPath1 = "items." + wallhangingName + ".owned";
                JToken jToken1 = jObject1.SelectToken(tokenPath1);
                jToken1.Replace(true);
                string updatedJsonString1 = jObject1.ToString();
                File.WriteAllText(path1, updatedJsonString1);
                SerializeJson();
            }
        }

        if (ownedWallhangings[i] == true)
        {
            // change wallhanging to clicked wallhanging
            string path = Application.persistentDataPath + "/playerData.json";
            string json = File.ReadAllText(path);
            JObject jObject = JsonConvert.DeserializeObject(json) as JObject;
            wallhangingSR.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/" + wallhangingName + ".png");
            string tokenPath = "currentItems.wallhanging";
            JToken jToken = jObject.SelectToken(tokenPath);
            jToken.Replace(wallhangingName);
            string updatedJsonString = jObject.ToString();
            File.WriteAllText(path, updatedJsonString);
            SerializeJson();

            setOwnedButtons();
        }
    }

    public void addWindowButtonListeners()
    {
        for (int i = 0; i < windowButtons.Length; i++)
        {
            int x = i;
            windowButtons[i].onClick.RemoveAllListeners();

            windowButtons[i].onClick.AddListener(() => windowOnClick(x));
        }
    }

    public void windowOnClick(int i)
    {
        string[] windows = { "window", "archedwindow", "heartwindow" };
        string windowName = windows[i];
        Debug.Log("Window name: " + windowName);

        // buy if unowned
        if (ownedWindows[i] == false)
        {
            // Check if enought money
            if (data.coins >= data.items[windowName].cost)
            {
                // subtract money from balance if enough money

                // update ownedWallhangings
                ownedWindows[i] = true;

                // write the new balance back to json
                currencyManager.subtractCoins(data.items[windowName].cost);

                GameObject panelCanvas = windowPanels[i].transform.GetChild(0).gameObject;

                GameObject panelCoin = panelCanvas.transform.GetChild(2).gameObject;
                GameObject panelCost = panelCanvas.transform.GetChild(3).gameObject;
                GameObject panelOwned = panelCanvas.transform.GetChild(4).gameObject;

                panelCoin.SetActive(false);
                panelCost.SetActive(false);
                panelOwned.SetActive(true);

                Debug.Log("Buying window");
                string path1 = Application.persistentDataPath + "/playerData.json";
                string json1 = File.ReadAllText(path1);
                JObject jObject1 = JsonConvert.DeserializeObject(json1) as JObject;
                string tokenPath1 = "items." + windowName + ".owned";
                JToken jToken1 = jObject1.SelectToken(tokenPath1);
                jToken1.Replace(true);
                string updatedJsonString1 = jObject1.ToString();
                File.WriteAllText(path1, updatedJsonString1);
                SerializeJson();
            }
        }

        if (ownedWindows[i] == true)
        {
            // change wallhanging to clicked wallhanging
            string path = Application.persistentDataPath + "/playerData.json";
            string json = File.ReadAllText(path);
            JObject jObject = JsonConvert.DeserializeObject(json) as JObject;
            windowSR.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/" + windowName + ".png");
            string tokenPath = "currentItems.window";
            JToken jToken = jObject.SelectToken(tokenPath);
            jToken.Replace(windowName);
            string updatedJsonString = jObject.ToString();
            File.WriteAllText(path, updatedJsonString);
            SerializeJson();

            setOwnedButtons();
        }
    }

    public void setOwnedButtons()
    {
        string[] wallhangings = { "clock", "lights", "painting", "wallhanging" };
        string[] windows = { "window", "archedwindow", "heartwindow" };

        for (int i = 0; i < ownedWallhangings.Length; i++)
        {
            GameObject panelCanvas = wallhangingPanels[i].transform.GetChild(0).gameObject;

            GameObject panelCoin = panelCanvas.transform.GetChild(2).gameObject;
            GameObject panelCost = panelCanvas.transform.GetChild(3).gameObject;
            GameObject panelOwned = panelCanvas.transform.GetChild(4).gameObject;

            // remove price for owned 
            if (ownedWallhangings[i] == true)
            {
                panelCoin.SetActive(false);
                panelCost.SetActive(false);
            }

            // set selected
            if (wallhangings[i] == data.currentItems["wallhanging"])
            {
                panelOwned.SetActive(true);
            }
            else
            {
                panelOwned.SetActive(false);
            }
        }

        for (int i = 0; i < ownedWindows.Length; i++)
        {
            GameObject panelCanvas = windowPanels[i].transform.GetChild(0).gameObject;

            GameObject panelCoin = panelCanvas.transform.GetChild(2).gameObject;
            GameObject panelCost = panelCanvas.transform.GetChild(3).gameObject;
            GameObject panelOwned = panelCanvas.transform.GetChild(4).gameObject;

            // remove price for owned 
            if (ownedWindows[i] == true)
            {
                panelCoin.SetActive(false);
                panelCost.SetActive(false);
            }

            // set selected
            if (windows[i] == data.currentItems["window"])
            {
                panelOwned.SetActive(true);
            }
            else
            {
                panelOwned.SetActive(false);
            }
        }
    }

    public void SerializeJson()
    {

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
