using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int playerID = 12345;
    public int coins = 1000;
    public int diamonds = 10;

    public Dictionary<string, string> currentItems = new Dictionary<string, string>()
    {
        // itemType : selectedItem
        { "window", "window" },
        { "wallhanging", "clock" },
        { "table1", "table" },
        { "table2", "" },
        { "table3", "" },
        { "table4", "" },
        { "table5", "" },
        { "table6", "" },
        { "oven1", "oven" },
        { "oven2", "" },
        { "oven3", "" },
        { "oven4", "" },
        { "oven5", "" },
        { "oven6", "" }
    };

    public Dictionary<string, bool> shop = new Dictionary<string, bool>()
    {
        // facilities : owned
        // tables
        { "table1", true },
        { "table2", false },
        { "table3", false },
        { "table4", false },
        { "table5", false },
        { "table6", false },
        // ovens
        { "oven1", true },
        { "oven2", false },
        { "oven3", false },
        { "oven4", false },
        { "oven5", false },
        { "oven6", false },
        // recipes
        { "americano", true },
        { "bearlatte", false},
        { "bubbletea", false},
        { "cake", false},
        { "muffin", false},
        { "toast", false},
        // decor
        // wallhangings
        { "clock", false},
        { "lights", false},
        { "painting", false},
        { "wallhanging", false},
        // windows
        { "archedwindow", false},
        { "heartwindow", false}
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
