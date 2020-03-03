using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragCameraScript : MonoBehaviour
{
    [Range(1.0f,100.0f)]
    public float maxSpeed = 20.0f;
    public Vector3 cursorPosition;
    Vector3 targetPos;
    public float currentRotateAngle;
    [Range(1.0f, 360.0f)]
    public float speedVal = 0.0f;
    bool initialCatch = false;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && initialCatch == false) {
            cursorPosition = Input.mousePosition;
            initialCatch = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            speedVal += (cursorPosition - Input.mousePosition).magnitude;
        } else if (Input.GetMouseButtonUp(0))
        {
            initialCatch = false;
        }
        //this.gameObject.transform.eulerAngles = new Vector3(0.0f, 1.0f*speedVal, 0.0f);
        //if (speedVal > 1.0f)
        //{
        //    speedVal--;
        //    if (speedVal < 1.0f)
        //    {
        //        speedVal = 0.0f;
        //    }
        //}
        //else if (speedVal < -1.0f)
        //{
        //    speedVal++;
        //    if (speedVal > -1.0f)
        //    {
        //        speedVal = 0.0f;
        //    }
        //}


    }

    private void Update()
    {
        this.gameObject.transform.eulerAngles = new Vector3(0.0f, 1.0f * speedVal, 0.0f);
    }
}
