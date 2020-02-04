using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Conveyor Belt Code based off code provided Mark Hogan available at popasylum.co.uk 
//http://popupasylum.co.uk/?p=253
public class ConveyorBeltScript : MonoBehaviour
{
    public float beltSpeed = 0.5f;
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
        Transform directionTransform = transform;
        Vector3 conveyorDirection = directionTransform.forward;
        Rigidbody rigBody = GetComponent<Rigidbody>();
        //The conveyor belt is first moved backwards before then moving forwards once again
        //This causes a collision with the objects on the conveyor belt moving them forward
        rigBody.position -= conveyorDirection * beltSpeed * Time.deltaTime;
        rigBody.MovePosition(rigBody.position + conveyorDirection * beltSpeed * Time.deltaTime);
    }
}
