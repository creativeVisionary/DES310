using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script for the stop watch functionality
public class StopwatchScript : MonoBehaviour
{
    //Member Variables
    //
    //References to objects
    [Header("Pointers To GameObjects")]
    //Prefab Objects
    public GameObject clockHand;
    public GameObject debugButton;
    //Game Controller
    public GameControllerScript gameController;
    //Timing Parameters
    [Header("Timing Options")]
    //Default time to tick down from
    public float tickDownTime = 5.0f;
    //Manual Override option for manual testing
    public bool manualOverride = true;
    //Timer variables
    private float timer = 0.0f;
    private float timerStartPoint = 0.0f;
    //Boolean variable to track if stopwatch is currently ticking
    private bool timing = false;


    //Functions

    // Start is called before the first frame update
    void Start()
    {
        timerStartPoint = gameController.gameTimeElapsed;
    }

    // Update is called once per frame
   private void Update()
    {
        //If the stopwatch is currently timing, tick down until the tick down time has been reached
      if (timing == true)
        {
            timer = gameController.gameTimeElapsed;
            float timeDifference = timer - timerStartPoint;
            //Rotate clock arm
            Rotate(tickDownTime, timeDifference);
            if (timeDifference > tickDownTime)//Time Reached
            {
                timer = timerStartPoint + tickDownTime;
                timing = false;
            }
        }
      //Only show debug button if manual override is enabled
      if (manualOverride == false)
        {
            debugButton.SetActive(false);
        } else
        {
            debugButton.SetActive(true);
        }
    }

    //Function to start timer and set timing to true
    public void StartTimer()
    {
        timerStartPoint = gameController.gameTimeElapsed;
        timing = true;
    }

    //Function to rotate clock arm
   private void Rotate(float maxTime, float currentTime)
    {
        //Find percent progress of the timing
        float percentFactor = currentTime / maxTime;
        //Calculate angle in degrees by getting a percentage of 360
        float angle = percentFactor * 360.0f;
        //Apply angle clockwise
        clockHand.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -angle);
    }

    //Setter to set Tick Down Time if manual override is disabled
    public void SetTickDownTime(float maxTime)
    {
        if (manualOverride == false)
        {
            tickDownTime = maxTime;
        }
    }
}
