using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://docs.unity3d.com/2018.3/Documentation/ScriptReference/Physics.Raycast.html
//

public class ObjectInteractCursorScript : MonoBehaviour
{

    Rigidbody rigBody;
    // Start is called before the first frame update
    void Start()
    {
        Physics.queriesHitTriggers = true;
        rigBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        //HoldObject();
    }

    private void OnMouseDrag()
    {
        HoldObject();
    }

    void HoldObject()
    {
        Vector3 mouseRay = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rigBody.velocity = new Vector3(0,0,0);
        Vector3 oldPos = rigBody.position;
        oldPos.y = 4.0f;
        oldPos.x = mouseRay.x;
        oldPos.z = mouseRay.z;
        rigBody.position = oldPos;
    }
}
