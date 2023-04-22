using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{   
    public bool isShopOpen = false;
    public bool isCustomerClosenessOpen = false;
    public bool isSettingsOpen = false; 

    public GameObject xButton;
    public GameObject rightButton;
    public GameObject leftButton; 

    public GameObject transparencyPanel;

    public bool buttonStateSaved;
    public bool rightButtonActive;
    public bool leftButtonActive;

    // Start is called before the first frame update
    void Start()
    {
        buttonStateSaved = false;
        rightButtonActive = false;
        leftButtonActive = false;
        transparencyPanel.SetActive(false);
        xButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void shopNavBarOnClick() {
        if (isShopOpen == false) {
            hideMovementButtons();

            // Open the shop if it is not open
            isShopOpen = true;

            // Close the other popups if they are open
            isCustomerClosenessOpen = false;
            isSettingsOpen = false;
        } else {
            // Close the shop if it is open
            isShopOpen = false;
            showMovementButtons();
        }

        Debug.Log("Shop: " + isShopOpen + ", Customers: " + isCustomerClosenessOpen + ", Settings :" + isSettingsOpen);
    }

    public void customerNavBarOnClick() {
        if (isCustomerClosenessOpen == false) {
            hideMovementButtons();

            // Open customer closeness if it is not open
            isCustomerClosenessOpen = true;

            // Close the other popups if they are open
            isShopOpen = false;
            isSettingsOpen = false;
        } else {
            // Close customer closeness if it is open
            isCustomerClosenessOpen = false;
            showMovementButtons();
        }
        
        Debug.Log("Shop: " + isShopOpen + ", Customers: " + isCustomerClosenessOpen + ", Settings :" + isSettingsOpen);

    }

    public void settingsOnClick() {
        if (isSettingsOpen == false) {
            hideMovementButtons();

            // Open customer closeness if it is not open
            isSettingsOpen = true;

            // Close the other popups if they are open
            isShopOpen = false;
            isCustomerClosenessOpen = false;
        } else {
            // Close customer closeness if it is open
            isSettingsOpen = false;
            showMovementButtons();
        }

        Debug.Log("Shop: " + isShopOpen + ", Customers: " + isCustomerClosenessOpen + ", Settings :" + isSettingsOpen);

    }

    public void xButtonOnClick() {
        // Close everything
        isShopOpen = false;
        isCustomerClosenessOpen = false;
        isSettingsOpen = false;
        showMovementButtons();
    }

    public void hideMovementButtons() {
        // Remember which movement button was active it hasn't already been saved

        
        if (buttonStateSaved == false) {
            
            Debug.Log("Saving button state");
            buttonStateSaved = true;

            if (rightButton.activeSelf == true) {
                rightButtonActive = true;
                leftButtonActive = false;
            } else {
                rightButtonActive = false;
                leftButtonActive = true;
            }
        }
        

        // Hide movement buttons
        rightButton.SetActive(false);
        leftButton.SetActive(false);
        transparencyPanel.SetActive(true);
        xButton.SetActive(true);
    }

    public void showMovementButtons() {
        Debug.Log("Restoring button state");
        buttonStateSaved = false;
        rightButton.SetActive(rightButtonActive);
        leftButton.SetActive(leftButtonActive);
        transparencyPanel.SetActive(false);
        xButton.SetActive(false);
    }

}