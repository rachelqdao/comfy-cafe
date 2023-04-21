using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    private bool[] tableAvailability;
    private int numTables = 6;
    private int lastAvailableTable = 0;

    public GameObject table1;
    public GameObject table2;
    public GameObject table3;
    public GameObject table4;
    public GameObject table5;
    public GameObject table6;

    // Start is called before the first frame update
    void Start()
    {
        // All tables available on start
        tableAvailability = new bool[] {true, true, true, true, true, true};

    }

    public int checkTableAvailability() {
        for (int i = 0; i < numTables; i++) {
            if (tableAvailability[i] == true) {
            
                tableAvailability[i] = false;
                lastAvailableTable = i;
                return i;
            }
        }
        
        return 0;
    }
    

    // update next available table
}
