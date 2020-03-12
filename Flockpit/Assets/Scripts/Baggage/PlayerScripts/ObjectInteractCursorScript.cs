using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code designed with the aid of following documentation
//https://docs.unity3d.com/ScriptReference/Camera.html


//Script to handle interactions with game world objects.
public class ObjectInteractCursorScript : MonoBehaviour
{

    Rigidbody rigBody;
    //World Position of Mouse Cursor
    public Vector3 mousePos;
    Vector3 mousePosZ;
    //Viewport location of Mouse Cursor
    public Vector3 screenMouse;
    //Viewport location of selected object
    public Vector3 screenObj;
    //Difference in terms of viewport space between the locations of the object and cursor
    public Vector3 difference;
    //Used later to set what distance the world point should be calculated at
    public float camDistance;
    //Position on the Y Axis for which the object is held
    public float holdHeight = 4.0f;
    //Used to track if the object has been marked as expired
    //Utilised mainly within the functionality for drop zones
    public bool hasExpired = false;
    //Get Game Controller
    GameObject[] gameController;

    // Start is called before the first frame update
    void Start()
    {
        Physics.queriesHitTriggers = true;
        rigBody = GetComponent<Rigidbody>();
        gameController = GameObject.FindGameObjectsWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        //Find the difference over the Y Axis between the camera and the body, thus giving the position for which the mouse position should be calculated
        camDistance = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - rigBody.position.y;
        Vector3 inputPos = Input.mousePosition;
        inputPos.z = camDistance;
        mousePos = Camera.main.ScreenToWorldPoint(inputPos);
        screenObj = rigBody.position;
        //Find position of mouse cursor and selected object in viewport space
        screenMouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //
        screenMouse = mousePosZ;
        if (hasExpired == true)
        {
            Destroy(this.gameObject);
        }
    }

    //Object is only dragged while it is tagged as active. Once expired the user cannot interact with the object
    private void OnMouseDrag()
    {
        if ((hasExpired == false)&&(gameController[0].GetComponent<GameControllerScript>().gamePause != true) && (gameController[0].GetComponent<GameControllerScript>().gameEnd != true))
        {
            HoldObjectTest();
        }
    }

    void HoldObject()
    {
        //Calculate difference of screen positions between object and mouse cursor
        difference = screenObj - mousePos;
        //Clear velocity
        //Velocity must be cleared otherwise object will not be held correctly
        rigBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        //Set the position of the object's rigid body component to the mouse position
        //Hold height is used in place of the mousePos Z component as a constant hold height is desired
        //Additionally the mousePos Y component is used in the newPos Z component as the view has been rotated to look down the Y axis thus the Y and Z Axes are switched
        Vector3 newPos = rigBody.position;
        newPos.x = mousePos.x;
        newPos.y = holdHeight;
        newPos.z = mousePos.z;
        rigBody.position = newPos;
    }
    void HoldObjectTest()
    {
        //Calculate difference of screen positions between object and mouse cursor
        difference = screenObj - mousePos;
        //Clear velocity
        //Velocity must be cleared otherwise object will not be held correctly
        rigBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        //Set the position of the object's rigid body component to the mouse position
        //Hold height is used in place of the mousePos Z component as a constant hold height is desired
        //Additionally the mousePos Y component is used in the newPos Z component as the view has been rotated to look down the Y axis thus the Y and Z Axes are switched
        Vector3 newPos = rigBody.position;
        newPos.x = mousePos.x;
        newPos.y = holdHeight;
        newPos.z = mousePos.z;
        rigBody.position = newPos;
    }
}
