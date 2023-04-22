using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManagementScript : MonoBehaviour
{
    public PopUpManager popUpManager;

    public GameObject shopPanel;
    public GameObject shopTab1;
    public GameObject shopTab2;
    public GameObject shopTab3;


    // Start is called before the first frame update
    void Start()
    {   
        popUpManager = GameObject.FindGameObjectWithTag("UI").GetComponent<PopUpManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (popUpManager.isShopOpen == true) {
            shopPanel.SetActive(true);
            shopTab1.SetActive(true);
            shopTab2.SetActive(true);
            shopTab3.SetActive(true);
        } else {
            shopPanel.SetActive(false);
            shopTab1.SetActive(false);
            shopTab2.SetActive(false);
            shopTab3.SetActive(false);
        }
    }
}
