using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerClosenessManagementScript : MonoBehaviour
{   
    public PopUpManager popUpManager;

    public GameObject customerClosenessPanel;

    // Start is called before the first frame update
    void Start()
    {
        popUpManager = GameObject.FindGameObjectWithTag("UI").GetComponent<PopUpManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (popUpManager.isCustomerClosenessOpen == true) {
            customerClosenessPanel.SetActive(true);
        } else {
            customerClosenessPanel.SetActive(false);
        }
    }

}
