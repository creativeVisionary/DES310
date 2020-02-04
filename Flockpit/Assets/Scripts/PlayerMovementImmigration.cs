using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementImmigration : MonoBehaviour
{
    //init variables
    public GameObject redBox, blueBox, eventText;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.02f;
        gameObject.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        redBox.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        blueBox.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 225.0f);
        eventText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //keypress test
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    Vector3 temp = gameObject.transform.position;
        //    temp.x -= 1;
        //    gameObject.transform.position = temp;
        //}
        Vector3 temp = gameObject.transform.position;
        temp.z += speed;
        gameObject.transform.position = temp;

        
    }

    void OnMouseDrag()
    {
        //move the passenger along the x axis
        transform.position = new Vector3((Input.mousePosition.x - 564)/20, 0, gameObject.transform.position.z);
    }

    void OnTriggerEnter(Collider col)
    {
        //if (col.collider.tag == "redBox")
        //{
        //    eventText.SetActive(true);
        //}
        if (col.tag == "Red")
        {
            eventText.GetComponent<TextMesh>().text = "Correct";
        }
        else if (col.tag == "Blue")
        {
            eventText.GetComponent<TextMesh>().text = "Wrong";
        }
        eventText.SetActive(true);

    }
}
