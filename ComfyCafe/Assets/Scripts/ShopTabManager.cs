using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTabManager : MonoBehaviour
{
    
    public bool isFacilitiesOpen;
    public bool isRecipesOpen;
    public bool isDecorationsOpen;

    public Button facilitiesTab;
    public Button recipesTab;
    public Button decorationsTab;

    private Image facilitiesTabImage;
    private Image recipesTabImage;
    private Image decorationsTabImage;

    // Start is called before the first frame update
    void Start()
    {
        facilitiesTabImage = facilitiesTab.GetComponent<Image>();
        recipesTabImage = recipesTab.GetComponent<Image>();
        decorationsTabImage = decorationsTab.GetComponent<Image>();

        // Open shop to facilities tab on start
        isFacilitiesOpen = true;
        isRecipesOpen = false;
        isDecorationsOpen = false;

        facilitiesTabImage.color = Color.white;
        recipesTabImage.color = Color.gray;
        decorationsTabImage.color = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacilitiesOpen == true) {
            facilitiesTabImage.color = Color.white;
            recipesTabImage.color = Color.gray;
            decorationsTabImage.color = Color.gray;
        } else if (isRecipesOpen == true) {
            facilitiesTabImage.color = Color.gray;
            recipesTabImage.color = Color.white;
            decorationsTabImage.color = Color.gray;
        } else if (isDecorationsOpen == true) {
            facilitiesTabImage.color = Color.gray;
            recipesTabImage.color = Color.gray;
            decorationsTabImage.color = Color.white;
        }
    }

    public void facilitiesTabOnClick() {
        if (isFacilitiesOpen == false) {
            isFacilitiesOpen = true;
            isRecipesOpen = false;
            isDecorationsOpen = false;
        }
    }

    public void recipesTabOnClick() {
        if (isRecipesOpen == false) {
            isFacilitiesOpen = false;
            isRecipesOpen = true;
            isDecorationsOpen = false;
        }
    }

    public void decorationTabOnClick() {
        if (isDecorationsOpen == false) {
            isFacilitiesOpen = false;
            isRecipesOpen = false;
            isDecorationsOpen = true;
        }
    }
}
