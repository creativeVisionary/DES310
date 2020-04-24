using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
//Script to place collided passengers in the center of the lane
public class LaneScript : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.name.Contains("Passenger") && !Input.GetMouseButtonDown(0))
        {
            collision.gameObject.transform.position = new Vector3(this.transform.position.x, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
        }
    }
}
