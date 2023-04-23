using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using System;
using System.IO;
using TMPro;
using UnityEditor;

public class OrderManager : MonoBehaviour
{
    PlayerData data;
    private PlayerData playerData = new PlayerData();
    private IDataService dataService = new JsonDataService();
    private bool EncryptionEnabled;

    private string[] recipes;
    public string[] ownedRecipes;

    // Start is called before the first frame update
    void Start()
    {
        SerializeJson();
        recipes = new string[] {"americano", "bubbletea", "bearlatte", "toast", "muffin", "cake"};
        updateOwnedRecipes();
    }

    void Update() {
        //SerializeJson();
    }

    public int getRecipeCookTime(String recipeName) {
        return data.recipes[recipeName].cookTime;
    }
    
    public int getRecipeEarnings(String recipeName) {
        return data.recipes[recipeName].earnings;
    }

    public string[] getOwnedRecipes() {
        return ownedRecipes;
    }

    public void updateOwnedRecipes() {
        SerializeJson();
        List<string> ownedRecipeList = new List<string>();

        for (int i = 0; i < recipes.Length; i++) {
            if (data.recipes[recipes[i]].owned == true) {
                ownedRecipeList.Add(recipes[i]);
            }
        }

        ownedRecipes = ownedRecipeList.ToArray();
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
