using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public float moveSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        int customerSpriteID = Random.Range(0, 5);
        spriteRenderer.sprite = spriteArray[customerSpriteID];
        Debug.Log("Customer Sprite ID: " + customerSpriteID);
    }

    // Update is called once per frame
    void Update()
    {   
        // TODO: Change to move per table -- right now just moves to the left
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        // TODO: Delete after done eating
    }
}
