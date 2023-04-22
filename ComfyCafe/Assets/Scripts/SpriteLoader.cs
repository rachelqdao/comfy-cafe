using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;


public class SpriteLoader : MonoBehaviour
{
    private PlayerData playerData = new PlayerData();
    private IDataService dataSerivce = new JsonDataService();
    public SpriteRenderer windowSpriteRenderer;
    public Sprite newWindowSprite;
    private bool EncryptionEnabled;

    // Start is called before the first frame update
    void Start()
    {
        // window
        // windowSpriteRenderer.sprite = newWindowSprite;
        SerializeJson();
    }

    public void SerializeJson()
    {
        if (dataSerivce.SaveData("/playerData.json", playerData, EncryptionEnabled))
        {
            try
            {
                PlayerData data = dataSerivce.LoadData<PlayerData>("/playerData.json", EncryptionEnabled);
                Debug.Log(data.playerID);
                Debug.Log(data.currentItems["wallhanging"]);
            }
            catch (Exception e)
            {
                Debug.LogError("Could not read file.");
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
