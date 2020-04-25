using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple script to destroy game objects on collision should their tag match the desired object via the objectString parameter
public class DestructorScript : MonoBehaviour
{

    public string objectString;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == objectString)
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }

}
