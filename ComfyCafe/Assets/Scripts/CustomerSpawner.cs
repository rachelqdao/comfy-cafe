using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public float spawnRate = 3;
    private float timer = 0;
    public int numTables;
    public int maxCustomers;

    public TableManager tableManager;

    public GameObject[] customers;

    public GameObject promoButton;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Get number of tables available from JSON
        // Spawn enough customers that there are enough tables / queue spots for (tables + 5)
        
        tableManager = GameObject.FindGameObjectWithTag("TableManager").GetComponent<TableManager>();

        maxCustomers = tableManager.numOwnedTables + 5;
        customers = GameObject.FindGameObjectsWithTag("Customer");

        // spawn customer on start
        spawnCustomer();
    }

    // Update is called once per frame
    void Update()
    {           
        // TODO: Get number of tables available from JSON
        // Spawn enough customers that there are enough tables / queue spots for (tables + 5)
        maxCustomers = tableManager.numOwnedTables + 5;

        // Get all the customers in the restaurant atm
        customers = GameObject.FindGameObjectsWithTag("Customer");

        if (customers.Length < maxCustomers) {
            if (timer < spawnRate) {
                timer = timer + Time.deltaTime;
            } else {
                spawnCustomer();
                timer = 0;
            }
        }
    }

    public void spawnCustomer() {
        // set the customer sprite here
        if (customers.Length < maxCustomers) {
            GameObject customer = Instantiate(customerPrefab, transform.position, transform.rotation, GameObject.FindGameObjectWithTag("Background").transform);
        }
    }

}
