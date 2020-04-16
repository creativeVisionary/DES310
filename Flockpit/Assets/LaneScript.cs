using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.gameObject.name.Contains("Passenger") && !Input.GetMouseButtonDown(0))
    //    {
    //        collision.gameObject.transform.position = new Vector3(this.transform.position.x, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
    //    }
    //}
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.name.Contains("Passenger") && !Input.GetMouseButtonDown(0))
        {
            collision.gameObject.transform.position = new Vector3(this.transform.position.x, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
        }
    }
}
