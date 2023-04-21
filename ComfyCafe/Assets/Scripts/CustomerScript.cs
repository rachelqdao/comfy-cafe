using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public float moveSpeed = 5;
    public int tableAssignment;

    public TableManager tableManager;
    
    int pathIndex = 0;
    public GameObject[] path;
    public GameObject[] tablePath;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to TableManager script
        tableManager = GameObject.FindGameObjectWithTag("TableManager").GetComponent<TableManager>();
        tableAssignment = tableManager.checkTableAvailability();

        Debug.Log("Table assignment: " + tableAssignment);

        path = GameObject.FindGameObjectsWithTag("Path");

        if (tableAssignment == 0) {
            tablePath = new GameObject[] {
                path[0],
                path[1],
                path[2]
            };
        } else if (tableAssignment == 1) {
            tablePath = new GameObject[] {
                path[0],
                path[1],
                path[3]
            };
        } else if (tableAssignment == 2) {
            tablePath = new GameObject[] {
                path[0],
                path[4],
                path[5]
            };
        } else if (tableAssignment == 3) {
            tablePath = new GameObject[] {
                path[0],
                path[4],
                path[6]
            };
        } else if (tableAssignment == 4) {
            tablePath = new GameObject[] {
                path[0],
                path[7],
                path[8]
            };
        } else if (tableAssignment == 5) {
            tablePath = new GameObject[] {
                path[0],
                path[7],
                path[9]
            };
        } 

        // TODO: Eventually start queueing customers if this isn't a valid table
        
        // Generate a random sprite for the character
        int customerSpriteID = Random.Range(0, 5);
        spriteRenderer.sprite = spriteArray[customerSpriteID];
    }

    // Update is called once per frame
    void Update() {   
        
        move();
        // TODO: Delete character after done eating
    }

    public void move() {
        if (pathIndex <= tablePath.Length - 1) {
            transform.position = Vector2.MoveTowards(transform.position, tablePath[pathIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == tablePath[pathIndex].transform.position) {
                pathIndex += 1;
            }
        }
    }
}
