using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManagementScript : MonoBehaviour
{

    public PopUpManager popUpManager;

    public GameObject settingsPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        popUpManager = GameObject.FindGameObjectWithTag("UI").GetComponent<PopUpManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (popUpManager.isSettingsOpen == true) {
            settingsPanel.SetActive(true);
        } else {
            settingsPanel.SetActive(false);
        }
    }
}
