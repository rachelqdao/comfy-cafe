using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TableManager : MonoBehaviour
{
    public bool[] tableAvailability;
    public int numTables;
    public bool[] queueAvailability;
    private int numQueue = 5;

    public GameObject[] tables; 

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Get number of tables available from JSON        
        numTables = 2;

        for (int i = 0; i < tables.Length; i++) {
            if (i >= numTables) {
                tables[i].SetActive(false);
            }
        }

        tableAvailability = new bool[numTables];
        Array.Fill(tableAvailability, true);
        queueAvailability = new bool[] {true, true, true, true, true};

    }

    public int checkTableAvailability() {
        for (int i = 0; i < numTables; i++) {
            if (tableAvailability[i] == true) {
                tableAvailability[i] = false;
                return i;
            }
        }
        
        return -1;
    }

    public int checkQueueAvailability() {
        for (int i = 0; i < numQueue; i++) {
            if (queueAvailability[i] == true) {
                queueAvailability[i] = false;
                return i;
            }
        }

        // Should never get here b/c only can spawn 11 customers at once
        return -1;
    }

    public int checkQueuePositionBefore(int currentIndex) {
        if (queueAvailability[currentIndex - 1] == true) {

            queueAvailability[currentIndex - 1] = false;
            queueAvailability[currentIndex] = true;
            
            return currentIndex - 1;
        } else {
            return currentIndex;
        }
    }

}
