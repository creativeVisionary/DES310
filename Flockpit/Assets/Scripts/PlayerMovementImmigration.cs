using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementImmigration : MonoBehaviour
{
    //Keep quantity of turnstiles to multiples of 4
    public List<GameObject> turnstiles;
    public float raycastDistance;
    private int turnstileQuantity = 0;

    [Header("Misc.")]
    public List<GameObject> passengerList;
    public GameObject passenger1;
    public GameObject passenger2;
    public GameObject passenger3;
    private bool passenger1Lock, passenger2Lock, passenger3Lock, currentGate, newGate;
    public Camera sceneCamera;
    private float speed;
    public int passengerCount;
    public Texture redPlayer, bluePlayer;

    //include GameControllerScript
    GameControllerScript endGame;

    // Start is called before the first frame update
    void Start()
    {
        turnstileQuantity = turnstiles.Count;
        speed = 0.07f;
        //speed = 0.2f;//debug speed
        passengerCount = 0;
        currentGate = false;
        newGate = false;
        endGame = FindObjectOfType<GameControllerScript>();

        //Initialise Passengers
        passenger1Lock = false;
        passenger2Lock = false;
        passenger3Lock = false;
        //
        passenger1.GetComponent<Renderer>().material.mainTexture = redPlayer;
        passenger2.GetComponent<Renderer>().material.mainTexture = redPlayer;
        passenger3.GetComponent<Renderer>().material.mainTexture = bluePlayer;
        //
        RandomiseGates();
       // eventText.SetActive(false);
       // eventText2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp;

        if ((endGame.gamePause) || (!endGame.gameStarted))
        {

        }
        //pause game if closest passenger is not lined up with a gate
        else if (GamePause())
        {
            //here
        }
        else if (!GamePause())
        {
            if (newGate == false)
            {
                temp = passenger1.transform.position;
                temp.z += speed;
                passenger1.transform.position = temp;
                if (passenger1.transform.position.z >= -9.0f && passenger1Lock == false && currentGate == false)
                {
                    passengerCount++;
                    if (passengerCount < 8)
                    {
                        temp.z = -35.0f;

                    }
                    else
                    {
                        passenger1Lock = true;
                        temp.z = -1000;
                    }
                    passenger1.transform.position = temp;
                    passengerRandomiser(passenger1);
                }
                else if (passenger1.transform.position.z >= 19.0f && passenger1Lock == false && currentGate == true && newGate == false)
                {
                    passengerCount++;
                    if (passengerCount < 8)
                    {
                        temp.z = -9.0f;

                    }
                    else
                    {
                        passenger1Lock = true;
                        temp.z = -10000;
                    }
                    passenger1.transform.position = temp;
                    passengerRandomiser(passenger1);
                }


                temp = passenger2.transform.position;
                temp.z += speed;
                passenger2.transform.position = temp;

                if (passenger2.transform.position.z >= -9.0f && passenger2Lock == false && currentGate == false)
                {
                    passengerCount++;
                    if (passengerCount < 8)
                    {
                        temp.z = -35.0f;

                    }
                    else
                    {
                        passenger2Lock = true;
                        temp.z = -1000;
                    }
                    passenger2.transform.position = temp;
                    passengerRandomiser(passenger2);
                }
                else if (passenger2.transform.position.z >= 19.0f && passenger2Lock == false && currentGate == true && newGate == false)
                {
                    passengerCount++;
                    if (passengerCount < 8)
                    {
                        temp.z = -9.0f;

                    }
                    else
                    {
                        passenger2Lock = true;
                        temp.z = -10000;
                    }
                    passenger2.transform.position = temp;
                    passengerRandomiser(passenger2);
                }

                temp = passenger3.transform.position;
                temp.z += speed;
                passenger3.transform.position = temp;

                if (passenger3.transform.position.z >= -9.0f && passenger3Lock == false && currentGate == false)
                {
                    passengerCount++;
                    if (passengerCount < 8)
                    {
                        temp.z = -35.0f;

                    }
                    else
                    {
                        passenger3Lock = true;
                        temp.z = -1000;
                    }
                    passenger3.transform.position = temp;
                    passengerRandomiser(passenger3);
                }
                else if (passenger3.transform.position.z >= 19.0f && passenger3Lock == false && currentGate == true && newGate == false)
                {
                    passengerCount++;
                    if (passengerCount < 8)
                    {
                        temp.z = -9.0f;

                    }
                    else
                    {
                        passenger3Lock = true;
                        temp.z = -10000;
                    }
                    passenger3.transform.position = temp;
                    passengerRandomiser(passenger3);
                }
            }
            else if (newGate == true)
            {
                temp = passenger1.transform.position;
                temp.z += speed;
                passenger1.transform.position = temp;
                if (passenger1.transform.position.z < -9.0f)
                {
                    temp.z = -9.0f;
                    passenger1.transform.position = temp;
                    passengerRandomiser(passenger1);
                }
                temp = passenger2.transform.position;
                temp.z += speed;
                passenger2.transform.position = temp;
                if (passenger1.transform.position.z >= -1 && passenger2.transform.position.z < -9.0f)
                {
                    temp.z = -9.0f;
                    passenger2.transform.position = temp;
                }
                temp = passenger3.transform.position;
                temp.z += speed;
                passenger3.transform.position = temp;
                if (passenger2.transform.position.z >= -1 && passenger3.transform.position.z < -9.0f)
                {
                    temp.z = -9.0f;
                    passenger3.transform.position = temp;
                    newGate = false;
                }
            }

            if (sceneCamera.transform.position.z <= 9 && passengerCount >= 10 && currentGate == false)
            {
               // CameraMove();
            }

            //end game
            if (passengerCount>= 10 && currentGate == true)
            {
                //call end game from GameControllerScript
                //endGame.
            }
        }
    }

    private void passengerRandomiser(GameObject pass)
    {
        //int colour = Random.Range(0, 2);//randomiser to choose between red and blue (0 and 1)

        //if (colour == 0)//set player to red
        //{
        //    //pass.GetComponent<Renderer>().material.color = new Color(225.0f, 0.0f, 0.0f);
        //    pass.GetComponent<Renderer>().material.mainTexture = redPlayer;
        //    pass.tag = "Red";
        //}
        //if (colour == 1)//set player to blue
        //{
        //    //pass.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 225.0f);
        //    pass.GetComponent<Renderer>().material.mainTexture = bluePlayer;
        //    pass.tag = "Blue";
        //}
        RandomiseGates();
        
    }
    
    private void RandomiseGates()
    {
        if (turnstileQuantity >= 4)
        {

            int[] openGate = new int[2];
            List<int> firstColIndex = new List<int>();
            List<int> secondColIndex = new List<int>();
            string[] colourTags = new string[2] {"Red","Blue" };
            for (int p = 0; p < 4; p++)
            {
                if (turnstiles[p].transform.Find("Gate").gameObject.tag == colourTags[0])
                {
                    firstColIndex.Add(p);
                } else
                {
                    secondColIndex.Add(p);
                }
            }
            if (Random.Range(0,2) != 0)
            {
                openGate[0] = firstColIndex[0];
            } else
            {
                openGate[0] = firstColIndex[1];
            }
            if (Random.Range(0, 2) != 0)
            {
                openGate[1] = secondColIndex[0];
            }
            else
            {
                openGate[1] = secondColIndex[1];
            }
            for (int i = 0; i < turnstileQuantity; i++)
            {
                for (int l = 0; l < openGate.Length; l++)
                {
                    if (openGate[l] == i)
                    {
                        turnstiles[i].gameObject.tag = "Open";
                        turnstiles[i].GetComponentInChildren<TextMesh>().text = "O";
                    } else if (i != openGate[0] && i!= openGate[1])
                    {
                        turnstiles[i].gameObject.tag = "Closed";
                        turnstiles[i].GetComponentInChildren<TextMesh>().text = "X";
                    }
                }
            }
            
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
            passengerCount = 0;
            newGate = true;
            currentGate = true;
            passenger1Lock = false;
            passenger2Lock = false;
            passenger3Lock = false;
        }
        sceneCamera.transform.position = temp;//update camera with new position
    }
   
    bool GamePause()
    {
        if (currentGate == false)
        {
            for (int i = 0; i < passengerList.Count; i++)
            {
                //if player has reached the wait line
                if ((passengerList[i].transform.position.z >= -16) && (passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine == false))
                {
                    Ray passengerRaycast = new Ray();
                    passengerRaycast.origin = passengerList[i].transform.position;
                    passengerRaycast.direction = new Vector3(0.0f, 0.0f, 1.0f);
                    RaycastHit raycastInfo = new RaycastHit();
                    Physics.Raycast(passengerRaycast, out raycastInfo, raycastDistance);
                    if (raycastInfo.collider != null)
                    {
                        if (raycastInfo.collider.gameObject.transform.parent.tag == "Open")
                        {
                            passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine = true;
                            return false;
                        }
                        else//if not infront of an available gate
                        { return true; }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        //none of the passengers have reached the wait line so the continue to move forward until they do
        return false;
    }
}
