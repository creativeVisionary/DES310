using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopwatchScript : MonoBehaviour
{
    [Header("Pointers To GameObjects")]
    public GameObject clockHand;
    public GameObject debugButton;
    [Header("Timing Options")]
    public float tickDownTime = 5.0f;
    public bool manualOverride = true;
    private float timer = 0.0f;
    private float timerStartPoint = 0.0f;
    private bool timing = false;
    // Start is called before the first frame update
    void Start()
    {
        timerStartPoint = Time.time;
    }

    // Update is called once per frame
   private void Update()
    {
      if (timing == true)
        {
            timer = Time.time;
            float timeDifference = timer - timerStartPoint;
            Rotate(tickDownTime, timeDifference);
            if (timeDifference > tickDownTime)//Time Reached
            {
                timer = timerStartPoint + tickDownTime;
                timing = false;
            }
        }
      
      if (manualOverride == false)
        {
            debugButton.SetActive(false);
        } else
        {
            debugButton.SetActive(true);
        }
    }

    public void StartTimer()
    {
        timerStartPoint = Time.time;
        timing = true;
    }


   private void Rotate(float maxTime, float currentTime)
    {
        float percentFactor = currentTime / maxTime;
        //angle in degrees
        float angle = percentFactor * 360.0f;
        clockHand.transform.localEulerAngles = new Vector3(0.0f, 0.0f, -angle);
    }


    public void SetTickDownTime(float maxTime)
    {
        if (manualOverride == false)
        {
            tickDownTime = maxTime;
        }
    }
}
