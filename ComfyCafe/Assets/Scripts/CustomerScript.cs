using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CustomerScript : MonoBehaviour
{   
    // customer sprites
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;

    // button to take customer order
    public Button speechBubble;

    // wait timer for food
    public float timer = 0;

    // move speed
    public float moveSpeed = 5;

    // table management
    public TableManager tableManager;
    public int tableAssignment;
    public int queueAssignment;
    public TableScript tableScript;

    // order management
    public OrderManager orderManager;
    public string recipe;

    // currency management
    public CurrencyManager currencyManager;
    
    // pathing
    int pathIndex = 0;
    public GameObject[] path;
    public GameObject[] queue;
    public GameObject[][] tablePaths;
    public GameObject[] tablePath;

    // triggers to move through pathing
    bool finishedMovingToTable = false;
    bool finishedOrderingItem = false;
    bool finishedEating = false;
    bool finishedMovingToCenterFromTable = false;
    bool finishedLeaveRestaurant = false;

    bool coinsAdded = false;

    // Start is called before the first frame update
    void Start()
    {   
        // Get reference to TableManager script
        tableManager = GameObject.FindGameObjectWithTag("TableManager").GetComponent<TableManager>();
        tableAssignment = tableManager.checkTableAvailability();

        // Get reference to OrderManager script
        orderManager = GameObject.FindGameObjectWithTag("OrderManager").GetComponent<OrderManager>();


        // Get reference to CurrencyManager script
        currencyManager = GameObject.FindGameObjectWithTag("CurrencyManager").GetComponent<CurrencyManager>();

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
        if (tableAssignment != -1) {
            tablePath = tablePaths[tableAssignment];
            getTableReference(tableAssignment);
        } else {
            // Get a queue number
            queueAssignment = tableManager.checkQueueAvailability();
        }

        // Generate a random sprite for the customer
        int customerSpriteID = UnityEngine.Random.Range(0, 7);
        spriteRenderer.sprite = spriteArray[customerSpriteID];

        // TODO: Generate a random food for the customer
        string[] ownedRecipes = orderManager.getOwnedRecipes();
        int ownedRecipeID = UnityEngine.Random.Range(0, ownedRecipes.Length);
        recipe = ownedRecipes[ownedRecipeID];
        // Debug.Log("Customer random recipe: " + ownedRecipes[ownedRecipeID]);

        // Hide Speech Bubble
        speechBubble.gameObject.SetActive(false);
        speechBubble.onClick.AddListener(takeOrder);
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
                    getTableReference(tableAssignment);
                }
            // otherwise, check customer it can move up in the queue    
            } else {
                queueAssignment = tableManager.checkQueuePositionBefore(queueAssignment);
            }
        }
    }

    public void getTableReference(int tableAssignment) {
        if (tableAssignment == 0) {
            tableScript = GameObject.FindGameObjectWithTag("Table1").GetComponent<TableScript>();
        } else if (tableAssignment == 1) {
            tableScript = GameObject.FindGameObjectWithTag("Table2").GetComponent<TableScript>();
        } else if (tableAssignment == 2) {
            tableScript = GameObject.FindGameObjectWithTag("Table3").GetComponent<TableScript>();
        } else if (tableAssignment == 3) {
            tableScript = GameObject.FindGameObjectWithTag("Table4").GetComponent<TableScript>();
        } else if (tableAssignment == 4) {
            tableScript = GameObject.FindGameObjectWithTag("Table5").GetComponent<TableScript>();
        } else if (tableAssignment == 5) {
            tableScript = GameObject.FindGameObjectWithTag("Table6").GetComponent<TableScript>();
        }
    }

    public void moveToTable() {
        spriteRenderer.sortingOrder = tableAssignment + 3;  

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
            speechBubble.gameObject.SetActive(true);
        }
    }

    public void eat() {
        if (finishedOrderingItem == true) {
            speechBubble.gameObject.SetActive(false);

            // TODO: timer should be based on the food they order
            // GET THE TIME TO COOK RECIPE CUSTOMER ORDERED HERE
            int timeToCook = orderManager.getRecipeCookTime(recipe);
            int timeToEat = timeToCook + 5;        // +5 seconds for eating

            // Debug.Log("time to eat (should be 10): " + timeToEat);

            if (timer < timeToCook) {
                // wait while food is cooking
                timer = timer + Time.deltaTime;
            } else if (timer > timeToCook && timer < timeToEat) {
                // display food and eat
                tableScript.displayFood(recipe);
                timer = timer + Time.deltaTime;
            } else {
                // gtfo
                // TODO: add money to currency
                // Debug.Log("Cost of " + recipe + ": " + orderManager.getRecipeEarnings(recipe));
                
                addToCoinBalance();
                tableScript.hideFood();
                finishedEating = true;
            }
        }
    }

    public void addToCoinBalance() {
        if (finishedEating == true && coinsAdded == false) {
            coinsAdded = true;
            currencyManager.addCoins(orderManager.getRecipeEarnings(recipe));
        }
    }

    public void moveToCenterFromTable() {
        if (finishedEating == true && finishedMovingToCenterFromTable == false) {
            transform.position = Vector2.MoveTowards(transform.position, tablePath[1].transform.position, moveSpeed * Time.deltaTime);    
        
            if (transform.position == tablePath[1].transform.position) {
                finishedMovingToCenterFromTable = true;

                // TODO: FIX THIS AGIAN LOL
                tableManager.tableAvailability[tableAssignment] = true;
                /*
                for (int i = 0; i < tableManager.numOwnedTables; i++) {
                    if (tableManager.tablesAvailable[i] == tableAssignment) {
                        tableManager.tableAvailability[i] = true;
                    }
                }
                */
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
    }

    public void takeOrder() {
        finishedOrderingItem = true;
    }
}
