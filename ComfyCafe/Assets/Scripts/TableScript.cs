using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TableScript : MonoBehaviour
{
    public GameObject recipe;
    public SpriteRenderer recipeSpriteRenderer;

    // Timer stuff
    public GameObject SliderCanvas;
    public Slider timerSlider;


    // Start is called before the first frame update
    void Start()
    {
        recipe.SetActive(false);
        SliderCanvas.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {   

    }
    
    public void displayTimer(float timer, int cookTime) {
        SliderCanvas.SetActive(true);
        timerSlider.maxValue = cookTime;
        timerSlider.value = timer;
    }

    public void hideTimer() {
        SliderCanvas.SetActive(false);
    }

    public void displayFood(string recipeName) {
        // render the sprite
        recipeSpriteRenderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/" + recipeName + ".png");
        // set the food to be active
        recipe.SetActive(true);
    }

    public void hideFood() {
        // set the food to be inactive
        recipe.SetActive(false);
    }
}
