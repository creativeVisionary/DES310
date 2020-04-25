using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DragCameraScript : MonoBehaviour
{
    [Range(1.0f,100.0f)]
    public float maxSpeed = 100.0f;
    [Range(1.0f, 360.0f)]
    public float speedVal = 0.0f;
    bool initialCatch = false;
    public Camera cam;

    Vector3 oldPos;
    Vector3 panOrigin;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tidy up speed value
        if (speedVal > 360.0f)
        {
            speedVal = (speedVal - 360.0f);
        }
        if (speedVal < 0.0f)
        {
            speedVal = (speedVal + 360.0f);
        }

        //set initial positions of camera and mouse
        if (Input.GetMouseButtonDown(0))
        {
            initialCatch = true;
            oldPos = transform.position;
            panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);//screen vector of mouse on click
        }

        //calculate update of the camera position in relation to mouse position change
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - panOrigin;//calculate difference between old and new mouse position
            Vector3 newPos = oldPos + -pos; //add now position to old position
            speedVal += newPos.x * maxSpeed;//update speed value with new position

            //update positions
            //If these are not here, the screen will scroll endlessly in current mouse position direction
            oldPos = transform.position;
            panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        //end drag
        if (Input.GetMouseButtonUp(0))
        {
            initialCatch = false;
        }

    }

    private void Update()
    {
        //update the position of the camera
        this.gameObject.transform.eulerAngles = new Vector3(0.0f, 1.0f * speedVal, 0.0f);
    }
}
