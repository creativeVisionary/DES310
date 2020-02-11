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
    public Camera sceneCamera;
    private float speed;
    public int passangerCount;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.02f;
        passangerCount = 0;
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
            passangerCount++;
            temp.z = -35.0f;
            passanger1.transform.position = temp;
            PassangerRandomiser(passanger1);
        }

        temp = passanger2.transform.position;
        temp.z += speed;
        passanger2.transform.position = temp;

        if (passanger2.transform.position.z >= -9.0f)
        {
            passangerCount++;
            temp.z = -35.0f;
            passanger2.transform.position = temp;
            PassangerRandomiser(passanger2);
        }

        temp = passanger3.transform.position;
        temp.z += speed;
        passanger3.transform.position = temp;

        if (passanger3.transform.position.z >= -9.0f)
        {
            passangerCount++;
            temp.z = -35.0f;
            passanger3.transform.position = temp;
            PassangerRandomiser(passanger3);
        }

        if (sceneCamera.transform.position.z <= 9 && passangerCount >= 10) 
        {
            CameraMove();
        }
    }

    private void PassangerRandomiser(GameObject pass)
    {
        int colour = Random.Range(0, 2);//randomiser to choose between red and blue (0 and 1)

        if (colour == 0)//set player to red
        {
            pass.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
            pass.tag = "Red";
        }
        else if (colour == 1)//set player to blue
        {
            pass.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 225.0f);
            pass.tag = "Blue";
        }
        else //error catch
        {
            pass.GetComponent<Renderer>().material.color = new Color(0.0f, 225.0f, 0.0f);
            pass.tag = "Default";
        }

        //randomise gates
        int redGate = Random.Range(0, 2);
        if (redGate == 0)//first gate is open
        {
            redBox1Text.GetComponent<TextMesh>().text = "O";
            redBox2Text.GetComponent<TextMesh>().text = "X";
        }
        else if (redGate == 1)//second gate is open
        {
            redBox1Text.GetComponent<TextMesh>().text = "X";
            redBox2Text.GetComponent<TextMesh>().text = "O";
        }
        else//error catch
        {
            redBox1Text.GetComponent<TextMesh>().text = "-";
            redBox2Text.GetComponent<TextMesh>().text = "-";
        }

        int blueGate = Random.Range(0, 2);
        if (blueGate == 0)//first gate is open
        {
            blueBox1Text.GetComponent<TextMesh>().text = "O";
            blueBox2Text.GetComponent<TextMesh>().text = "X";
        }
        else if (blueGate == 1)//second gate is open
        {
            blueBox1Text.GetComponent<TextMesh>().text = "X";
            blueBox2Text.GetComponent<TextMesh>().text = "O";
        }
        else//error catch
        {
            blueBox1Text.GetComponent<TextMesh>().text = "-";
            blueBox2Text.GetComponent<TextMesh>().text = "-";
        }
    }

    void CameraMove()
    {
        //pan camera to gate 2
        Vector3 temp = sceneCamera.transform.position;
        temp.z += 0.2f;
        if (temp.z >9)
        {
            temp.z = 9;
        }
        sceneCamera.transform.position = temp;
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
