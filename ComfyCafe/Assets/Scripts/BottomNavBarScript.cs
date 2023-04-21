using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomNavBarScript : MonoBehaviour
{

    public GameObject rightButton;
    public GameObject leftButton; 
    public bool rightButtonActive;
    public bool leftButtonActive;

    public GameObject xButton;
    public GameObject shopPanel;
    public GameObject shopTab1;
    public GameObject shopTab2;
    public GameObject shopTab3;
    public GameObject transparencyPanel;
    
    // Start is called before the first frame update
    void Start()
    {   
        Debug.Log("Right button active: " + rightButtonActive);
        Debug.Log("Left button active: " + leftButtonActive);

        // Hide shop UI on start
        xButton.SetActive(false);
        shopPanel.SetActive(false);
        shopTab1.SetActive(false);
        shopTab2.SetActive(false);
        shopTab3.SetActive(false);
        transparencyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onShopOpen() {
        // Remember which movement button was active
        if (rightButton.active == true) {
            rightButtonActive = true;
            leftButtonActive = false;
        } else {
            rightButtonActive = false;
            leftButtonActive = true;
        }

        // Hide movement buttons
        rightButton.SetActive(false);
        leftButton.SetActive(false);

        // Bring up shop UI
        xButton.SetActive(true);
        shopPanel.SetActive(true);
        shopTab1.SetActive(true);
        shopTab2.SetActive(true);
        shopTab3.SetActive(true);
        transparencyPanel.SetActive(true);
    }

    public void onShopClose() {
        // Show movement buttons
        rightButton.SetActive(rightButtonActive);
        leftButton.SetActive(leftButtonActive);

        // Hide shop UI
        xButton.SetActive(false);
        shopPanel.SetActive(false);
        shopTab1.SetActive(false);
        shopTab2.SetActive(false);
        shopTab3.SetActive(false);
        transparencyPanel.SetActive(false);
    }
}
