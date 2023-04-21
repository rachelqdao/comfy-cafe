using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public float spawnRate = 3;
    private float timer = 0;

    public GameObject[] customers;

    public GameObject promoButton;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("customer spawner position: " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {           
        
        customers = GameObject.FindGameObjectsWithTag("Customer");
        //Debug.Log("num customers = " + customers.Length);

        if (customers.Length < 11) {
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
        if (customers.Length < 11) {
            GameObject customer = Instantiate(customerPrefab, transform.position, transform.rotation, GameObject.FindGameObjectWithTag("Background").transform);
        }
    }

}
