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

    bool finishedMovingToTable = false;
    bool finishedMovingToCenterFromTable = false;
    bool finishedLeaveRestaurant = false;

    // Start is called before the first frame update
    void Start()
    {   
        // Get reference to TableManager script
        tableManager = GameObject.FindGameObjectWithTag("TableManager").GetComponent<TableManager>();
        tableAssignment = tableManager.checkTableAvailability();

        // Debug.Log("Table assignment: " + tableAssignment);

        // Get all the waypoints
        path = GameObject.FindGameObjectsWithTag("Path");

        // Assign a path based on the table assignment
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
        moveToTable();
        moveToCenterFromTable();
        leaveRestaurant();
        deleteCustomer();
        // TODO: Delete character after done eating
    }

    public void moveToTable() {
        if (pathIndex <= tablePath.Length - 1) {
            transform.position = Vector2.MoveTowards(transform.position, tablePath[pathIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == tablePath[pathIndex].transform.position) {
                pathIndex += 1;
            }
        } 

        if (transform.position == tablePath[2].transform.position) {
            finishedMovingToTable = true;
        }
    }

    public void moveToCenterFromTable() {
        if (finishedMovingToTable == true && finishedMovingToCenterFromTable == false) {
            transform.position = Vector2.MoveTowards(transform.position, tablePath[1].transform.position, moveSpeed * Time.deltaTime);    
        
            if (transform.position == tablePath[1].transform.position) {
                finishedMovingToCenterFromTable = true;
            }
        }
    }

    public void leaveRestaurant() {
        if (finishedMovingToCenterFromTable == true && finishedLeaveRestaurant == false) {
            transform.position = Vector2.MoveTowards(transform.position, path[10].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == path[10].transform.position) {
                finishedLeaveRestaurant = true;
            }
        }
    }

    public void deleteCustomer() {
        if (finishedLeaveRestaurant == true) {
            Debug.Log("Destroy customer");
            Destroy(gameObject);
        }
    }
}
