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

    // Start is called before the first frame update
    void Start()
    {
        SerializeJson();
        recipes = new string[] {"americano", "bubbletea", "bearlatte", "toast", "muffin", "cake"};
    }

    void Update() {
        // constantly check for updates to owned recipes?
        SerializeJson();
    }

    public int getRecipeCookTime(String recipeName) {
        return data.recipes[recipeName].cookTime;
    }
    
    public int getRecipeEarnings(String recipeName) {
        return data.recipes[recipeName].cookTime;
    }

    public string[] getOwnedRecipes() {
        List<string> ownedRecipeList = new List<string>();

        for (int i = 0; i < recipes.Length; i++) {
            if (data.recipes[recipes[i]].owned == true) {
                ownedRecipeList.Add(recipes[i]);
            }
        }

        string[] ownedRecipes = ownedRecipeList.ToArray();
        return ownedRecipes;
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
