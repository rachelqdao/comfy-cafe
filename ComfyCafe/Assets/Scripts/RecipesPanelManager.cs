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

    // Start is called before the first frame update
    void Start()
    {   
        SerializeJson();

        // List of recipes by name
        recipes = new string[] {"americano", "bubbletea", "bearlatte", "toast", "muffin", "cake"};
        ownedRecipes = new Dictionary<string, bool>();
        // Get reference to CurrencyManager script
        currencyManager = GameObject.FindGameObjectWithTag("CurrencyManager").GetComponent<CurrencyManager>();

        // Read data to get owned recipes
        getOwnedRecipes();
        
        Debug.Log("owned americano: " + ownedRecipes["americano"]);
        Debug.Log("owned bubble tea: " + ownedRecipes["bubbletea"]);
        Debug.Log("owned bear latte: " + ownedRecipes["bearlatte"]);
        Debug.Log("owned toast: " + ownedRecipes["toast"]);
        Debug.Log("owned muffin: " + ownedRecipes["muffin"]);
        Debug.Log("owned cake: " + ownedRecipes["cake"]);

        // Configure buttons
        //addRecipeButtonListeners();
        //disableButtons();
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

    /*
    public void getOwnedRecipes() {
        if (data.recipes["americano"].owned == true) {
            ownedRecipes[0] = true;
        } else {
            ownedRecipes[0] = false;
        }

         if (data.recipes["bubbletea"].owned == true) {
            ownedRecipes[1] = true;
        } else {
            ownedRecipes[1] = false;
        }

        if (data.recipes["bearlatte"].owned == true) {
            ownedRecipes[2] = true;
        } else {
            ownedRecipes[2] = false;
        }

        if (data.recipes["toast"].owned == true) {
            ownedRecipes[3] = true;
        } else {
            ownedRecipes[3] = false;
        }

        if (data.recipes["muffin"].owned == true) {
            ownedRecipes[4] = true;
        } else {
            ownedRecipes[4] = false;
        }

        if (data.recipes["cake"].owned == true) {
            ownedRecipes[5] = true;
        } else {
            ownedRecipes[5] = false;
        }
    }
    */

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
