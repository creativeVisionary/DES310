using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementImmigration : MonoBehaviour
{
    //init variables
    public GameObject redBox1, redBox2, blueBox1, blueBox2, eventText;
    public GameObject redBox1Text, redBox2Text, blueBox1Text, blueBox2Text;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.02f;
        gameObject.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        redBox1.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        redBox2.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        blueBox1.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 225.0f);
        blueBox2.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 225.0f);
        blueBox2Text.GetComponent<TextMesh>().text = "O";
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

        if (gameObject.transform.position.z >= -9.0f)
        {
            temp.z = -32.0f;
            gameObject.transform.position = temp;
        }
    }

    void OnMouseDrag()
    {
        //move the passenger along the x axis
        gameObject.transform.position = new Vector3((Input.mousePosition.x - 564)/20, 0, gameObject.transform.position.z);
    }

    void OnTriggerEnter(Collider col)
    {
        //if (col.collider.tag == "redBox")
        //{
        //    eventText.SetActive(true);
        //}
        if (col.tag == "Red")
        {
            if (gameObject.tag == "Red")
            {
                eventText.GetComponent<TextMesh>().text = "Correct";
            }
            if (gameObject.tag == "Blue")
            {
                eventText.GetComponent<TextMesh>().text = "Wrong";
            }

        }
        else if (col.tag == "Blue")
        {
            if (gameObject.tag == "Red")
            {
                eventText.GetComponent<TextMesh>().text = "Wrong";
            }
            if (gameObject.tag == "Blue")
            {
                eventText.GetComponent<TextMesh>().text = "Correct";
            }
        }
        eventText.SetActive(true);

    }
}
