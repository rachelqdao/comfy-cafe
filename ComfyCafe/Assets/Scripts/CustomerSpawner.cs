using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public float spawnRate = 3;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        if (timer < spawnRate) {
            timer = timer + Time.deltaTime;
        } else {
            spawnCustomer();
            timer = 0;
        }
    }

    public void spawnCustomer() {
        // set the customer sprite here
        GameObject customer = Instantiate(customerPrefab, transform.position, transform.rotation, GameObject.FindGameObjectWithTag("Background").transform);

    }

}
