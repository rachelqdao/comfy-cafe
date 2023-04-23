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

    public GameObject facilitiesPanel;
    public GameObject recipesPanel;
    public GameObject decorationsPanel;

    // Start is called before the first frame update
    void Start()
    {   
        // Get refs to all the tab images
        facilitiesTabImage = facilitiesTab.GetComponent<Image>();
        recipesTabImage = recipesTab.GetComponent<Image>();
        decorationsTabImage = decorationsTab.GetComponent<Image>();

        // Get refs to the individual shop panels
        /*
        facilitiesPanel = GameObject.FindGameObjectWithTag("FacilitiesPanel");
        recipesPanel = GameObject.FindGameObjectWithTag("RecipesPanel");
        decorationsPanel = GameObject.FindGameObjectWithTag("DecorationsPanel");
        */

        // Open shop to facilities tab on start
        isFacilitiesOpen = true;
        isRecipesOpen = false;
        isDecorationsOpen = false;


        /*
        // Show proper tabs
        facilitiesTabImage.color = Color.white;
        recipesTabImage.color = Color.gray;
        decorationsTabImage.color = Color.gray;

        // Show proper panels
        facilitiesPanel.SetActive();
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacilitiesOpen == true) {
            facilitiesTabImage.color = Color.white;
            recipesTabImage.color = Color.gray;
            decorationsTabImage.color = Color.gray;

            facilitiesPanel.SetActive(true);
            recipesPanel.SetActive(false);
            decorationsPanel.SetActive(false);
            
        } else if (isRecipesOpen == true) {
            facilitiesTabImage.color = Color.gray;
            recipesTabImage.color = Color.white;
            decorationsTabImage.color = Color.gray;

            facilitiesPanel.SetActive(false);
            recipesPanel.SetActive(true);
            decorationsPanel.SetActive(false);

        } else if (isDecorationsOpen == true) {
            facilitiesTabImage.color = Color.gray;
            recipesTabImage.color = Color.gray;
            decorationsTabImage.color = Color.white;

            facilitiesPanel.SetActive(false);
            recipesPanel.SetActive(false);
            decorationsPanel.SetActive(true);
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
