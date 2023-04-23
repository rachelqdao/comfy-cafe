using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TableScript : MonoBehaviour
{
    public GameObject recipe;
    public SpriteRenderer recipeSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        recipe.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayFood(string recipeName) {
        // render the sprite
        recipeSpriteRenderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/" + recipeName + ".png");
        Debug.Log("Path: " + "Assets/Sprites/" + recipeName + ".png" );
        // set the food to be active
        recipe.SetActive(true);
    }

    public void hideFood() {
        // set the food to be inactive
        recipe.SetActive(false);
    }
}
