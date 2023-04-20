using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBackground : MonoBehaviour
{
    
    public Camera mainCamera;
    public RectTransform canvasElement;    
    public float moveDuration;
    public GameObject rightButton;
    public GameObject leftButton; 
    
    // Start is called before the first frame update
    void Start()
    {
        leftButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void panRight() {

        // Calculate the width of the camera in world units
        float cameraWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;
        
        // Get the current position of the canvas element
        Vector2 currentPosition = canvasElement.anchoredPosition;

        // Calculate the new position of the canvas element
        float newX = currentPosition.x - cameraWidth;
        Vector2 newPosition = new Vector2(newX, currentPosition.y);

        rightButton.SetActive(false);
        leftButton.SetActive(true);

        // Set the position of the canvas element to the new position
        StartCoroutine(MoveCanvasElement(newPosition, moveDuration));
    }

    public void panLeft() {

        // Calculate the width of the camera in world units
        float cameraWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;
        
        // Get the current position of the canvas element
        Vector2 currentPosition = canvasElement.anchoredPosition;

        // Calculate the new position of the canvas element
        float newX = currentPosition.x + cameraWidth;
        Vector2 newPosition = new Vector2(newX, currentPosition.y);

        rightButton.SetActive(true);
        leftButton.SetActive(false);

        // Set the position of the canvas element to the new position
        StartCoroutine(MoveCanvasElement(newPosition, moveDuration));
    }

    IEnumerator MoveCanvasElement(Vector2 newPosition, float duration)
    {
        Vector2 startPosition = canvasElement.anchoredPosition;
        float elapsedTime = 0;
        
        Debug.Log("Move Duration: " + moveDuration);

        while (elapsedTime < duration)
        {
            canvasElement.anchoredPosition = Vector2.Lerp(startPosition, newPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasElement.anchoredPosition = newPosition;
    }
}
