using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class RecipesPanelManager : MonoBehaviour
{

    PlayerData data;
    private PlayerData playerData = new PlayerData();
    private IDataService dataService = new JsonDataService();
    private bool EncryptionEnabled;

    public GameObject[] recipePanels;
    public Button[] recipeButtons;
    
    private string[] recipes;
    public Dictionary<string, bool> ownedRecipes;
    public int clickedButtonIndex;

    public CurrencyManager currencyManager;
    public OrderManager orderManager;

    // Start is called before the first frame update
    void Start()
    {   
        SerializeJson();

        // List of recipes by name
        recipes = new string[] {"americano", "bubbletea", "bearlatte", "toast", "muffin", "cake"};
        ownedRecipes = new Dictionary<string, bool>();

        // Get reference to CurrencyManager script
        currencyManager = GameObject.FindGameObjectWithTag("CurrencyManager").GetComponent<CurrencyManager>();

        // Get reference to OrderManager script
        orderManager = GameObject.FindGameObjectWithTag("OrderManager").GetComponent<OrderManager>();


        // Read data to get owned recipes
        getOwnedRecipes();
        
        /*
        Debug.Log("owned americano: " + ownedRecipes["americano"]);
        Debug.Log("owned bubble tea: " + ownedRecipes["bubbletea"]);
        Debug.Log("owned bear latte: " + ownedRecipes["bearlatte"]);
        Debug.Log("owned toast: " + ownedRecipes["toast"]);
        Debug.Log("owned muffin: " + ownedRecipes["muffin"]);
        Debug.Log("owned cake: " + ownedRecipes["cake"]);
        */

        // Configure buttons
        addRecipeButtonListeners();
        disableButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getOwnedRecipes() {
        for (int i = 0; i < recipes.Length; i++) {
            if (data.recipes[recipes[i]].owned == true) {
                ownedRecipes[recipes[i]] = true;
            } else {
                ownedRecipes[recipes[i]] = false;
            }
        }
    }

    public void disableButtons() {
        for (int i = 0; i < recipes.Length; i++) {
            if (ownedRecipes[recipes[i]] == true) {
                recipeButtons[i].interactable = false;
                GameObject panelCanvas = recipePanels[i].transform.GetChild(0).gameObject;

                GameObject panelCoin = panelCanvas.transform.GetChild(2).gameObject;
                GameObject panelCost = panelCanvas.transform.GetChild(3).gameObject;
                GameObject panelOwned = panelCanvas.transform.GetChild(4).gameObject;

                panelCoin.SetActive(false);
                panelCost.SetActive(false);
                panelOwned.SetActive(true);
            }
        }
    }

    public void addRecipeButtonListeners() {
        for (int i = 0; i < recipeButtons.Length; i++) {
            int x = i;
            recipeButtons[i].onClick.RemoveAllListeners();
            recipeButtons[i].onClick.AddListener(() => buyRecipe(x));
        }
    }

    public void buyRecipe(int i) {
        Debug.Log("Bought recipe: " + recipes[i]);

        SerializeJson();
        if (data.coins > data.recipes[recipes[i]].cost) {

            // subtract coins
            currencyManager.subtractCoins(data.recipes[recipes[i]].cost);

            // set the button and panel
            recipeButtons[i].interactable = false;
            GameObject panelCanvas = recipePanels[i].transform.GetChild(0).gameObject;

            GameObject panelCoin = panelCanvas.transform.GetChild(2).gameObject;
            GameObject panelCost = panelCanvas.transform.GetChild(3).gameObject;
            GameObject panelOwned = panelCanvas.transform.GetChild(4).gameObject;

            panelCoin.SetActive(false);
            panelCost.SetActive(false);
            panelOwned.SetActive(true);

            // write back to json
            string tokenPath = "recipes." + recipes[i] + ".owned";

            string path = Application.persistentDataPath + "/playerData.json";
            string json = File.ReadAllText(path);
            JObject jObject = JsonConvert.DeserializeObject(json) as JObject;
            JToken jToken = jObject.SelectToken(tokenPath);
            jToken.Replace(true);
            string updatedJsonString = jObject.ToString();
            File.WriteAllText(path, updatedJsonString);
            SerializeJson();

            // Set order manager to be able to order the new recipe
            orderManager.updateOwnedRecipes();
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
