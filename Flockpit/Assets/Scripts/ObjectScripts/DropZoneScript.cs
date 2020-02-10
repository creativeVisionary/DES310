using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script handling the operations of objects dedicated as Drop Zone Objectives
//Code is reliant on collisions to mark when an object has arrived
public class DropZoneScript : MonoBehaviour
{
    //Game Controller Objects
    public GameControllerScript gameController;
    public GameObject controllerObject;
    //Desired Object Tag
    public string objectString;
    // Start is called before the first frame update
    void Start()
    {
        gameController = controllerObject.GetComponent<GameControllerScript>();
    }

    //When a collision has taken place and the object contains the desired tag,
    //player score is incrimented and the object is marked as expired
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == objectString)
        {
            if (collision.gameObject.GetComponent<ObjectInteractCursorScript>().hasExpired != true)
            {
                gameController.IncrimentPlayerScore(1);
                collision.gameObject.GetComponent<ObjectInteractCursorScript>().hasExpired = true;
            }
        }
    }
}
