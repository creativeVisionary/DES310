using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://docs.unity3d.com/2018.3/Documentation/ScriptReference/Physics.Raycast.html
//

public class ObjectInteractCursorScript : MonoBehaviour
{

    Rigidbody rigBody;
    public Vector3 mousePos;
<<<<<<< Updated upstream
    public Vector3 screenMouse;
    public Vector3 screenObj;
    public Vector3 difference;
    public float camDistance;

   
=======
    public Vector3 objRay;
>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        Physics.queriesHitTriggers = true;
        rigBody = GetComponent<Rigidbody>();
        camDistance = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        camDistance = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - rigBody.position.y;
        Vector3 inputPos = Input.mousePosition;
        inputPos.z = camDistance;
        mousePos = Camera.main.ScreenToWorldPoint(inputPos);
        //
        screenMouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        screenObj = Camera.main.WorldToViewportPoint(rigBody.position);
=======
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //mousePos = Input.mousePosition;
>>>>>>> Stashed changes
    }

    private void OnMouseDown()
    {

    }

    private void OnMouseDrag()
    {
        HoldObject();
    }

    void HoldObject()
    {
<<<<<<< Updated upstream
        //
        difference = screenObj - screenMouse;
        Vector3 inputPos = Input.mousePosition;
        inputPos.z = camDistance;
        Vector3 finalPos = Camera.main.ScreenToWorldPoint(inputPos);
        //
        rigBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 newPos = rigBody.position;
        newPos.x = mousePos.x;
        newPos.y = 4.0f;
        newPos.z = mousePos.z;
        rigBody.position = newPos;
=======
        Vector3 mouseRay = Camera.main.ScreenToViewportPoint(Input.mousePosition);
         objRay = Camera.main.WorldToViewportPoint(rigBody.position);
        Vector3 moveVec = objRay - mousePos;
        moveVec = Camera.main.ViewportToWorldPoint(moveVec);
        rigBody.velocity = new Vector3(0,0,0);
        Vector3 oldPos = rigBody.position;
        oldPos.y = 4.0f;
        oldPos.x = mouseRay.x;
        //oldPos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
       // oldPos.z = mouseRay.z;
        rigBody.position = oldPos;
>>>>>>> Stashed changes
    }
}
