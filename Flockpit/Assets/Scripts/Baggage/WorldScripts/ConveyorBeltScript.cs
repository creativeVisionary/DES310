using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Conveyor Belt Code based off code example provided Mark Hogan available at popasylum.co.uk 
//Available at:
//http://popupasylum.co.uk/?p=253
//Accessed on:03/02/2020

//Script to move objects along a piece of geometry in a similar fashion to a conveyor belt
public class ConveyorBeltScript : MonoBehaviour
{
    //Belt speed can be varied within inspector
    public float beltSpeed = 3.0f;
    public GameControllerScript gameController;
    public float texScrollVal = 0.25f;
    float texScrollSpeed = 0.023f;

    private void FixedUpdate()
    {
        if (gameController.gamePause == false)
        {
            //The conveyor belt current transformation and forward facing direction are found
            Transform directionTransform = transform;
            Vector3 conveyorDirection = directionTransform.right;
            Rigidbody rigBody = GetComponent<Rigidbody>();
            //The rigid body of the belt is then placed back a distance varied by the belt speed
            rigBody.position -= conveyorDirection * beltSpeed * Time.deltaTime;
            //The belt is then moved forward, colliding with the objects on the belt moving them forward
            //The belt returns to its Y position fast enough that the objects do not fall due to gravity,
            //giving the illusion of conveyor belt style movement
            rigBody.MovePosition(rigBody.position + conveyorDirection * beltSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        if (gameController.gamePause == false)
        {//Texture scrolling effect
            texScrollSpeed = texScrollVal / beltSpeed;
            Renderer renderer = GetComponent<Renderer>();
            float offset = Time.time * texScrollSpeed;
            renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
    }
}
