using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerResetScript : MonoBehaviour
{
    public Transform spawnPoint;
    public PlayerMovementImmigration movementManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.collider.gameObject.tag == "Red")||(collision.collider.gameObject.tag == "Blue"))
        {
            collision.collider.transform.position = spawnPoint.position;
            collision.collider.gameObject.GetComponent<OnMouseDragScript>().passedGateLine = false;
            movementManager.passengerRandomiser(collision.collider.gameObject);
        }
     
    }
}
