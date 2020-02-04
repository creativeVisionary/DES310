using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementImmigration : MonoBehaviour
{
    //init variables
    public GameObject redBox1, redBox2, blueBox1, blueBox2, eventText;
    public GameObject redBox1Text, redBox2Text, blueBox1Text, blueBox2Text;
    public GameObject passanger1, passanger2, passanger3;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.02f;
        passanger1.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        passanger2.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        passanger3.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
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
        Vector3 temp = passanger1.transform.position;
        temp.z += speed;
        passanger1.transform.position = temp;
        if (passanger1.transform.position.z >= -9.0f)
        {
            temp.z = -35.0f;
            passanger1.transform.position = temp;
            PassangerRandomiser(passanger1);
        }

        temp = passanger2.transform.position;
        temp.z += speed;
        passanger2.transform.position = temp;

        if (passanger2.transform.position.z >= -9.0f)
        {
            temp.z = -35.0f;
            passanger2.transform.position = temp;
            PassangerRandomiser(passanger2);
        }

        temp = passanger3.transform.position;
        temp.z += speed;
        passanger3.transform.position = temp;

        if (passanger3.transform.position.z >= -9.0f)
        {
            temp.z = -35.0f;
            passanger3.transform.position = temp;
            PassangerRandomiser(passanger3);
        }
    }

    private void PassangerRandomiser(GameObject pass)
    {
        int colour = Random.Range(0, 2);
        if (colour == 0)
        {
            pass.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
            pass.tag = "Red";
        }
        else if (colour == 1)
        {
            pass.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 225.0f);
            pass.tag = "Blue";
        }
        else //error
        {
            pass.GetComponent<Renderer>().material.color = new Color(0.0f, 225.0f, 0.0f);
            pass.tag = "Default";
        }
    }

    //void OnTriggerEnter(Collider col)
    //{
    //    //if (col.collider.tag == "redBox")
    //    //{
    //    //    eventText.SetActive(true);
    //    //}
    //    if (col.tag == "Red")
    //    {
    //        if (gameObject.tag == "Red")
    //        {
    //            eventText.GetComponent<TextMesh>().text = "Correct";
    //        }
    //        if (gameObject.tag == "Blue")
    //        {
    //            eventText.GetComponent<TextMesh>().text = "Wrong";
    //        }

    //    }
    //    else if (col.tag == "Blue")
    //    {
    //        if (gameObject.tag == "Red")
    //        {
    //            eventText.GetComponent<TextMesh>().text = "Wrong";
    //        }
    //        if (gameObject.tag == "Blue")
    //        {
    //            eventText.GetComponent<TextMesh>().text = "Correct";
    //        }
    //    }
    //    eventText.SetActive(true);

    //}
}
