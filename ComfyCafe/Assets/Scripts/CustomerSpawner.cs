using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public float spawnRate = 5f;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("customer spawner position: " + transform.position);
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
