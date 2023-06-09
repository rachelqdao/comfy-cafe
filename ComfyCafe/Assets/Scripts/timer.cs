using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Slider timerSlider;
    public float gameTime;
    public bool stopTimer;

    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        float time = gameTime - Time.time;
        
        if (time <= 0) {
            stopTimer = true;
        }

        if (stopTimer == false) {
            timerSlider.value = time;
        }
    }
}
