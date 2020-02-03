using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Conveyor Belt Code based off code provided Mark Hogan available at popasylum.co.uk 
//http://popupasylum.co.uk/?p=253
public class ConveyorBeltScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Rigidbody rigBody = GetComponent<Rigidbody>();
        rigBody.position -= transform.forward * 2.0f * Time.deltaTime;
        rigBody.MovePosition(rigBody.position + transform.forward * 2.0f * Time.deltaTime);
    }
}
