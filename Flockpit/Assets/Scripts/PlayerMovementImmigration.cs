using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementImmigration : MonoBehaviour
{
    //init variables
    [Header("Gate 1")]
    public GameObject redBox1;
    public GameObject redBox2;
    public GameObject blueBox1;
    public GameObject blueBox2;
    public GameObject eventText;

    public GameObject redBox1Text;
    public GameObject redBox2Text;
    public GameObject blueBox1Text;
    public GameObject blueBox2Text;

    public GameObject redBox1Status;
    public GameObject redBox2Status;
    public GameObject blueBox1Status;
    public GameObject blueBox2Status;

    [Header("Gate 2")]
    public GameObject redBox12;
    public GameObject redBox22;
    public GameObject blueBox12;
    public GameObject blueBox22;
    public GameObject eventText2;

    public GameObject redBox12Text;
    public GameObject redBox22Text;
    public GameObject blueBox12Text;
    public GameObject blueBox22Text;

    public GameObject redBox12Status, redBox22Status, blueBox12Status, blueBox22Status;

    [Header("Misc.")]
    public GameObject passanger1;
    public GameObject passanger2;
    public GameObject passanger3;
    private bool passanger1Lock, passanger2Lock, passanger3Lock, currentGate, newGate;
    public Camera sceneCamera;
    private float speed;
    public int passangerCount;

    //include GameControllerScript
    GameControllerScript endGame;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.07f;
        //speed = 0.2f;//debug speed
        passangerCount = 0;
        currentGate = false;
        newGate = false;
        endGame = FindObjectOfType<GameControllerScript>();

        //init passangers
        passanger1Lock = false;
        passanger2Lock = false;
        passanger3Lock = false;
        passanger1.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        passanger2.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        passanger3.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 225.0f);
        //passanger3.GetComponent<Renderer>().material.SetTexture("RedTex", SM_Immigration_Turnstile_01_BC);

        //init gate 1
        //redBox1.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        //redBox2.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        //blueBox1.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 225.0f);
        //blueBox2.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 225.0f);

        //init gate 2
        //redBox12.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        //redBox22.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        //blueBox12.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 225.0f);
        //blueBox22.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 225.0f);

        RandomiseGates1();
        eventText.SetActive(false);
        eventText2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp;

        //pause game if closest passenger is not lined up with a gate
        if (GamePause())
        {
            //here
        }
        else if (!GamePause())
        {



            if (newGate == false)
            {
                temp = passanger1.transform.position;
                temp.z += speed;
                passanger1.transform.position = temp;
                if (passanger1.transform.position.z >= -9.0f && passanger1Lock == false && currentGate == false)
                {
                    passangerCount++;
                    if (passangerCount < 8)
                    {
                        temp.z = -35.0f;

                    }
                    else
                    {
                        passanger1Lock = true;
                        temp.z = -1000;
                    }
                    passanger1.transform.position = temp;
                    PassangerRandomiser(passanger1);
                }
                else if (passanger1.transform.position.z >= 19.0f && passanger1Lock == false && currentGate == true && newGate == false)
                {
                    passangerCount++;
                    if (passangerCount < 8)
                    {
                        temp.z = -9.0f;

                    }
                    else
                    {
                        passanger1Lock = true;
                        temp.z = -10000;
                    }
                    passanger1.transform.position = temp;
                    PassangerRandomiser(passanger1);
                }


                temp = passanger2.transform.position;
                temp.z += speed;
                passanger2.transform.position = temp;

                if (passanger2.transform.position.z >= -9.0f && passanger2Lock == false && currentGate == false)
                {
                    passangerCount++;
                    if (passangerCount < 8)
                    {
                        temp.z = -35.0f;

                    }
                    else
                    {
                        passanger2Lock = true;
                        temp.z = -1000;
                    }
                    passanger2.transform.position = temp;
                    PassangerRandomiser(passanger2);
                }
                else if (passanger2.transform.position.z >= 19.0f && passanger2Lock == false && currentGate == true && newGate == false)
                {
                    passangerCount++;
                    if (passangerCount < 8)
                    {
                        temp.z = -9.0f;

                    }
                    else
                    {
                        passanger2Lock = true;
                        temp.z = -10000;
                    }
                    passanger2.transform.position = temp;
                    PassangerRandomiser(passanger2);
                }

                temp = passanger3.transform.position;
                temp.z += speed;
                passanger3.transform.position = temp;

                if (passanger3.transform.position.z >= -9.0f && passanger3Lock == false && currentGate == false)
                {
                    passangerCount++;
                    if (passangerCount < 8)
                    {
                        temp.z = -35.0f;

                    }
                    else
                    {
                        passanger3Lock = true;
                        temp.z = -1000;
                    }
                    passanger3.transform.position = temp;
                    PassangerRandomiser(passanger3);
                }
                else if (passanger3.transform.position.z >= 19.0f && passanger3Lock == false && currentGate == true && newGate == false)
                {
                    passangerCount++;
                    if (passangerCount < 8)
                    {
                        temp.z = -9.0f;

                    }
                    else
                    {
                        passanger3Lock = true;
                        temp.z = -10000;
                    }
                    passanger3.transform.position = temp;
                    PassangerRandomiser(passanger3);
                }
            }
            else if (newGate == true)
            {
                temp = passanger1.transform.position;
                temp.z += speed;
                passanger1.transform.position = temp;
                if (passanger1.transform.position.z < -9.0f)
                {
                    temp.z = -9.0f;
                    passanger1.transform.position = temp;
                    PassangerRandomiser(passanger1);
                }
                temp = passanger2.transform.position;
                temp.z += speed;
                passanger2.transform.position = temp;
                if (passanger1.transform.position.z >= -1 && passanger2.transform.position.z < -9.0f)
                {
                    temp.z = -9.0f;
                    passanger2.transform.position = temp;
                }
                temp = passanger3.transform.position;
                temp.z += speed;
                passanger3.transform.position = temp;
                if (passanger2.transform.position.z >= -1 && passanger3.transform.position.z < -9.0f)
                {
                    temp.z = -9.0f;
                    passanger3.transform.position = temp;
                    newGate = false;
                }
            }

            if (sceneCamera.transform.position.z <= 9 && passangerCount >= 10 && currentGate == false)
            {
                CameraMove();
            }

            //end game
            if (passangerCount>= 10 && currentGate == true)
            {
                //call end game from GameControllerScript
                //endGame.
            }
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
        if (colour == 1)//set player to blue
        {
            pass.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 225.0f);
            pass.tag = "Blue";
        }
        //else //error catch
        //{
        //    pass.GetComponent<Renderer>().material.color = new Color(0.0f, 225.0f, 0.0f);
        //    pass.tag = "Default";
        //}

        if (currentGate == false) { RandomiseGates1(); }
        else if (currentGate == true) { RandomiseGates2(); }
        
    }
    
    private void RandomiseGates1()
    {
        //randomise gates
        int redGate = Random.Range(0, 2);
        if (redGate == 0)//first gate is open
        {
            redBox1Text.GetComponent<TextMesh>().text = "O";
            redBox1Status.tag = "Open";
            redBox2Text.GetComponent<TextMesh>().text = "X";
            redBox2Status.tag = "Closed";
        }
        if (redGate == 1)//second gate is open
        {
            redBox1Text.GetComponent<TextMesh>().text = "X";
            redBox1Status.tag = "Closed";
            redBox2Text.GetComponent<TextMesh>().text = "O";
            redBox2Status.tag = "Open";
        }
        //else//error catch
        //{
        //    redBox1Text.GetComponent<TextMesh>().text = "-";
        //    redBox2Text.GetComponent<TextMesh>().text = "-";
        //}

        int blueGate = Random.Range(0, 2);
        if (blueGate == 0)//first gate is open
        {
            blueBox1Text.GetComponent<TextMesh>().text = "O";
            blueBox1Status.tag = "Open";
            blueBox2Text.GetComponent<TextMesh>().text = "X";
            blueBox2Status.tag = "Closed";
        }
        if (blueGate == 1)//second gate is open
        {
            blueBox1Text.GetComponent<TextMesh>().text = "X";
            blueBox1Status.tag = "Closed";
            blueBox2Text.GetComponent<TextMesh>().text = "O";
            blueBox2Status.tag = "Open";
        }
        //else//error catch
        //{
        //    blueBox1Text.GetComponent<TextMesh>().text = "-";
        //    blueBox2Text.GetComponent<TextMesh>().text = "-";
        //}
    }

    private void RandomiseGates2()
    {
        //randomise gates
        int redGate = Random.Range(0, 2);
        if (redGate == 0)//first gate is open
        {
            redBox12Text.GetComponent<TextMesh>().text = "O";
            redBox12Status.tag = "Open";
            redBox22Text.GetComponent<TextMesh>().text = "X";
            redBox22Status.tag = "Closed";
        }
        else if (redGate == 1)//second gate is open
        {
            redBox12Text.GetComponent<TextMesh>().text = "X";
            redBox12Status.tag = "Closed";
            redBox22Text.GetComponent<TextMesh>().text = "O";
            redBox22Status.tag = "Open";
        }
        else//error catch
        {
            redBox12Text.GetComponent<TextMesh>().text = "-";
            redBox12Status.tag = "Closed";
            redBox22Text.GetComponent<TextMesh>().text = "-";
            redBox22Status.tag = "Closed";
        }

        int blueGate = Random.Range(0, 2);
        if (blueGate == 0)//first gate is open
        {
            blueBox12Text.GetComponent<TextMesh>().text = "O";
            blueBox12Status.tag = "Open";
            blueBox22Text.GetComponent<TextMesh>().text = "X";
            blueBox22Status.tag = "Closed";
        }
        else if (blueGate == 1)//second gate is open
        {
            blueBox12Text.GetComponent<TextMesh>().text = "X";
            blueBox12Status.tag = "Closed";
            blueBox22Text.GetComponent<TextMesh>().text = "O";
            blueBox22Status.tag = "Open";
        }
        else//error catch
        {
            blueBox12Text.GetComponent<TextMesh>().text = "-";
            blueBox12Status.tag = "Closed";
            blueBox22Text.GetComponent<TextMesh>().text = "-";
            blueBox22Status.tag = "Closed";
        }
    }

    void CameraMove()//camera pan
    {
        //pan camera to gate 2
        Vector3 temp = sceneCamera.transform.position;
        temp.z += 0.2f;
        if (temp.z >9)//set z to 9 once pan is done
        {
            temp.z = 9;
            passangerCount = 0;
            newGate = true;
            currentGate = true;
            passanger1Lock = false;
            passanger2Lock = false;
            passanger3Lock = false;
        }
        sceneCamera.transform.position = temp;//update camera with new position
    }
   
    bool GamePause()
    {
        if (currentGate == false)
        {
            //if player has reached the wait line
            if (passanger1.transform.position.z >= -16)//player 1
            {
                //check to see if passanger 1 is infront of an open gate
                if ((passanger1.transform.position.x > 1.5f && passanger1.transform.position.x < 2.5f) && blueBox1Status.tag == "Open")//blue gate 1
                {
                    return false;
                }
                else if ((passanger1.transform.position.x > 6.0f && passanger1.transform.position.x < 7.0f) && blueBox2Status.tag == "Open")//blue gate 2
                {
                    return false;
                }
                else if ((passanger1.transform.position.x > -7.5f && passanger1.transform.position.x < -6.5f) && redBox1Status.tag == "Open")//red gate 1
                {
                    return false;
                }
                else if ((passanger1.transform.position.x > -3.0f && passanger1.transform.position.x < -2.0f) && redBox2Status.tag == "Open")//red gate 2
                {
                    return false;
                }
                else//if not infront of an available gate, forward movement is paused
                { return true; }
            }
            if (passanger2.transform.position.z >= -16)//passanger 2
            {
                //check to see if passanger 2 is infront of an open gate
                if ((passanger2.transform.position.x > 1.5f && passanger2.transform.position.x < 2.5f) && blueBox1Status.tag == "Open")//blue gate 1
                {
                    return false;
                }
                else if ((passanger2.transform.position.x > 6.0f && passanger2.transform.position.x < 7.0f) && blueBox2Status.tag == "Open")//blue gate 2
                {
                    return false;
                }
                else if ((passanger2.transform.position.x > -7.5f && passanger2.transform.position.x < -6.5f) && redBox1Status.tag == "Open")//red gate 1
                {
                    return false;
                }
                else if ((passanger2.transform.position.x > -3.0f && passanger2.transform.position.x < -2.0f) && redBox2Status.tag == "Open")//red gate 2
                {
                    return false;
                }
                else//if not infront of an available gate, forward movement is paused
                { return true; }
            }
            if (passanger3.transform.position.z >= -16)//passanger 3
            {
                //check to see if passanger 3 is infront of an open gate
                if ((passanger3.transform.position.x > 1.5f && passanger3.transform.position.x < 2.5f) && blueBox1Status.tag == "Open")//blue gate 1
                {
                    return false;
                }
                else if ((passanger3.transform.position.x > 6.0f && passanger3.transform.position.x < 7.0f) && blueBox2Status.tag == "Open")//blue gate 2
                {
                    return false;
                }
                else if ((passanger3.transform.position.x > -7.5f && passanger3.transform.position.x < -6.5f) && redBox1Status.tag == "Open")//red gate 1
                {
                    return false;
                }
                else if ((passanger3.transform.position.x > -3.0f && passanger3.transform.position.x < -2.0f) && redBox2Status.tag == "Open")//red gate 2
                {
                    return false;
                }
                else//if not infront of an available gate, forward movement is paused
                { return true; }
            }
        }
        else if (currentGate == true)
        {
            //if player has reached the wait line
            if (passanger1.transform.position.z >= 12)//player 1
            {
                //check to see if passanger 1 is infront of an open gate
                if ((passanger1.transform.position.x > 1.5f && passanger1.transform.position.x < 2.5f) && blueBox12Status.tag == "Open")//blue gate 1
                {
                    return false;
                }
                else if ((passanger1.transform.position.x > 6.0f && passanger1.transform.position.x < 7.0f) && blueBox22Status.tag == "Open")//blue gate 2
                {
                    return false;
                }
                else if ((passanger1.transform.position.x > -7.5f && passanger1.transform.position.x < -6.5f) && redBox12Status.tag == "Open")//red gate 1
                {
                    return false;
                }
                else if ((passanger1.transform.position.x > -3.0f && passanger1.transform.position.x < -2.0f) && redBox22Status.tag == "Open")//red gate 2
                {
                    return false;
                }
                else//if not infront of an available gate, forward movement is paused
                { return true; }
            }
            if (passanger2.transform.position.z >= 12)//passanger 2
            {
                //check to see if passanger 2 is infront of an open gate
                if ((passanger2.transform.position.x > 1.5f && passanger2.transform.position.x < 2.5f) && blueBox12Status.tag == "Open")//blue gate 1
                {
                    return false;
                }
                else if ((passanger2.transform.position.x > 6.0f && passanger2.transform.position.x < 7.0f) && blueBox22Status.tag == "Open")//blue gate 2
                {
                    return false;
                }
                else if ((passanger2.transform.position.x > -7.5f && passanger2.transform.position.x < -6.5f) && redBox12Status.tag == "Open")//red gate 1
                {
                    return false;
                }
                else if ((passanger2.transform.position.x > -3.0f && passanger2.transform.position.x < -2.0f) && redBox22Status.tag == "Open")//red gate 2
                {
                    return false;
                }
                else//if not infront of an available gate, forward movement is paused
                { return true; }
            }
            if (passanger3.transform.position.z >= 12)//passanger 3
            {
                //check to see if passanger 3 is infront of an open gate
                if ((passanger3.transform.position.x > 1.5f && passanger3.transform.position.x < 2.5f) && blueBox12Status.tag == "Open")//blue gate 1
                {
                    return false;
                }
                else if ((passanger3.transform.position.x > 6.0f && passanger3.transform.position.x < 7.0f) && blueBox22Status.tag == "Open")//blue gate 2
                {
                    return false;
                }
                else if ((passanger3.transform.position.x > -7.5f && passanger3.transform.position.x < -6.5f) && redBox12Status.tag == "Open")//red gate 1
                {
                    return false;
                }
                else if ((passanger3.transform.position.x > -3.0f && passanger3.transform.position.x < -2.0f) && redBox22Status.tag == "Open")//red gate 2
                {
                    return false;
                }
                else//if not infront of an available gate, forward movement is paused
                { return true; }
            }
        }
        //none of the passangers have reached the wait line so the continue to move forward until they do
        return false;
    }
}
