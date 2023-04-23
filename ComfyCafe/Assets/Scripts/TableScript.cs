using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour
{
    public GameObject food;

    // Start is called before the first frame update
    void Start()
    {
        food.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayFood() {
        // TODO: pass in the name of a sprite to display?

        // render the sprite

        // set the food to be active
        food.SetActive(true);

    }

    public void hideFood() {
        // set the food to be inactive
        food.SetActive(false);
    }
}
