using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    // sprites
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;

    public GameObject speechBubble;
    public Sprite speechBubbleSprite;

    public float timer = 0;

    // move speed
    public float moveSpeed = 5;

    // table management
    public TableManager tableManager;
    public int tableAssignment;
    public int queueAssignment;
    
    // pathing
    int pathIndex = 0;
    public GameObject[] path;
    public GameObject[] queue;
    public GameObject[][] tablePaths;
    public GameObject[] tablePath;

    bool finishedMovingToTable = false;
    bool finishedOrderingItem = false;
    bool finishedEating = false;
    bool finishedMovingToCenterFromTable = false;
    bool finishedLeaveRestaurant = false;

    bool movedToQueue = false;

    // Start is called before the first frame update
    void Start()
    {   
        // Get reference to TableManager script
        tableManager = GameObject.FindGameObjectWithTag("TableManager").GetComponent<TableManager>();
        tableAssignment = tableManager.checkTableAvailability();

        // Get all the path waypoints
        path = GameObject.FindGameObjectsWithTag("Path");

        // Get all the queue waypoints
        queue = GameObject.FindGameObjectsWithTag("Queue"); 

        GameObject[] table1Path = new GameObject[] {path[0], path[1], path[2]};
        GameObject[] table2Path = new GameObject[] {path[0], path[1], path[3]};
        GameObject[] table3Path = new GameObject[] {path[0], path[4], path[5]};
        GameObject[] table4Path = new GameObject[] {path[0], path[4], path[6]};
        GameObject[] table5Path = new GameObject[] {path[0], path[7], path[8]};
        GameObject[] table6Path = new GameObject[] {path[0], path[7], path[9]};

        tablePaths = new GameObject[][] {table1Path, table2Path, table3Path, table4Path, table5Path, table6Path};
    
        // Assign a path based on the table assignment
        if (tableAssignment == 0) {
            tablePath = table1Path;
        } else if (tableAssignment == 1) {
            tablePath = table2Path;
        } else if (tableAssignment == 2) {
            tablePath = table3Path;
        } else if (tableAssignment == 3) {
            tablePath = table4Path;
        } else if (tableAssignment == 4) {
            tablePath = table5Path;
        } else if (tableAssignment == 5) {
            tablePath = table6Path;
        } else {
            Debug.Log("No tables available -- move to queue");
            // Get a queue number
            queueAssignment = tableManager.checkQueueAvailability();
            Debug.Log("Queue Number: " + queueAssignment);
        }

        // TODO: Eventually start queueing customers if this isn't a valid table

        // Generate a random sprite for the character
        int customerSpriteID = Random.Range(0, 7);
        spriteRenderer.sprite = spriteArray[customerSpriteID];

        // Create a speech bubble  
        /*
        speechBubble = new GameObject("SpeechBubble");
        SpriteRenderer speechBubbleRenderer = speechBubble.AddComponent<SpriteRenderer>();
        speechBubbleRenderer.sprite = speechBubbleSprite;
        */
    }

    // Update is called once per frame
    void Update() {   
        if (tableAssignment >= 0) {
            moveToTable();
            orderItem();
            eat();
            moveToCenterFromTable();
            leaveRestaurant();
            deleteCustomer();
        } else {
            // move to correct place in queue
            moveToQueue();

            // if the queue position is 0, check if theres an available table
            if (queueAssignment == 0) {
                tableAssignment = tableManager.checkTableAvailability();
                
                if (tableAssignment >= 0) {
                    tableManager.queueAvailability[0] = true;
                    tablePath = tablePaths[tableAssignment];
                }
            // otherwise, check customer it can move up in the queue    
            } else {
                queueAssignment = tableManager.checkQueuePositionBefore(queueAssignment);
            }
        }
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


    public void orderItem() {
        if (finishedMovingToTable == true) {
            // TODO: make some kind of speech bubble user has to tap here?
            finishedOrderingItem = true;
        }
    }

    public void eat() {
        if (finishedOrderingItem == true) {
            // Timer for customers to sit at the table and eat
            if (timer < 15) {
                timer = timer + Time.deltaTime;
            } else {
                finishedEating = true;
                timer = 0;
            }
        }
    }

    public void moveToCenterFromTable() {
        if (finishedEating == true && finishedMovingToCenterFromTable == false) {
            transform.position = Vector2.MoveTowards(transform.position, tablePath[1].transform.position, moveSpeed * Time.deltaTime);    
        
            if (transform.position == tablePath[1].transform.position) {
                finishedMovingToCenterFromTable = true;
                tableManager.tableAvailability[tableAssignment] = true;
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
            Destroy(gameObject);
        }
    }

    public void moveToQueue() {
        transform.position = Vector2.MoveTowards(transform.position, queue[queueAssignment].transform.position, moveSpeed * Time.deltaTime);

        if (transform.position == queue[queueAssignment].transform.position) {
            movedToQueue = true;
        }
    }

    public void moveUpInQueue() {
        transform.position = Vector2.MoveTowards(transform.position, queue[queueAssignment].transform.position, moveSpeed * Time.deltaTime);
    }
}
