using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
