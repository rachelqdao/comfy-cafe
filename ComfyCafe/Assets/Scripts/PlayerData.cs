using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int playerID = 12345;
    public int coins = 500;
    public int diamonds = 10;

    public Dictionary<string, string> currentItems = new Dictionary<string, string>()
    {
        // itemType : selectedItem
        { "window", "window" },
        { "wallhanging", "clock" }
    };

    public Dictionary<string, Item> items = new Dictionary<string, Item>()
    {
        // facilities : owned
        // tables
        { "table1", new Item() {
            cost = 0,
            owned = true
        }},
        { "table2", new Item() {
            cost = 100,
            owned = false
        }},
        { "table3", new Item() {
            cost = 150,
            owned = false
        }},
        { "table4", new Item() {
            cost = 200,
            owned = false
        }},
        { "table5", new Item() {
            cost = 250,
            owned = false
        }},
        { "table6", new Item() {
            cost = 300,
            owned = false
        }},
        // ovens
        { "oven1", new Item() {
            cost = 0,
            owned = true
        }},
        { "oven2", new Item() {
            cost = 200,
            owned = false
        }},
        { "oven3", new Item() {
            cost = 250,
            owned = false
        }},
        { "oven4", new Item() {
            cost = 300,
            owned = false
        }},
        { "oven5", new Item() {
            cost = 350,
            owned = false
        }},
        { "oven6", new Item() {
            cost = 400,
            owned = false
        }},
        // decor
        // wallhangings
        { "clock", new Item() {
            cost = 0,
            owned = true
        }},
        { "lights", new Item() {
            cost = 100,
            owned = false
        }},
        { "painting", new Item() {
            cost = 200,
            owned = false
        }},
        { "wallhanging", new Item() {
            cost = 300,
            owned = false
        }},
        // windows
        { "window", new Item() {
            cost = 0,
            owned = true
        }},
        { "archedwindow", new Item() {
            cost = 400,
            owned = false
        }},
        { "heartwindow", new Item() {
            cost = 500,
            owned = false
        }}
    };

    public Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>()
    {
        { "americano", new Recipe() {
            cost = 0,
            owned = true,
            cookTime = 5,
            earnings = 10
        }},
        { "bearlatte", new Recipe() {
            cost = 500,
            owned = false,
            cookTime = 10,
            earnings = 20
        }},
        { "bubbletea", new Recipe() {
            cost = 400,
            owned = false,
            cookTime = 7,
            earnings = 15
        }},
        { "cake", new Recipe() {
            cost = 500,
            owned = false,
            cookTime = 10,
            earnings = 20
        }},
        { "muffin", new Recipe() {
            cost = 400,
            owned = false,
            cookTime = 7,
            earnings = 15
        }},
        { "toast", new Recipe() {
            cost = 0,
            owned = true,
            cookTime = 5,
            earnings = 10
        }},
    }; 

    public Dictionary<string, int> customers = new Dictionary<string, int>() 
    {
        // customers : heartPoints
        { "bunny", 0 },
        { "calico", 0 },
        { "havanese", 0 },
        { "korilakkuma", 0 },
        { "panda", 0 },
        { "rilakkuma", 0 },
        { "shiba", 0 }
    };
}
