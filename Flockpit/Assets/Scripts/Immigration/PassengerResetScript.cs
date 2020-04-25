using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script to handle reset of passenger prefabs upon collision
/// </summary>

public class PassengerResetScript : MonoBehaviour
{
    //Set movement manager class and point to respawn via inspector
    public Transform spawnPoint;
    public PlayerMovementImmigration movementManager;

    //Upon colliding, check tags for Red or Blue indicating a passenger object
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.collider.gameObject.tag == "Red")||(collision.collider.gameObject.tag == "Blue"))
        {
            //Set position of the passenger to the spawn point, set the boolean to being false as the passenger is no longer past the gate line and randomise the passenger properties
            collision.collider.transform.position = spawnPoint.position;
            collision.collider.gameObject.GetComponent<OnMouseDragScript>().passedGateLine = false;
            movementManager.passengerRandomiser(collision.collider.gameObject);
        }
     
    }
}
