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
    private bool currentGate, newGate;
    public Camera sceneCamera;
    private float speed;
    public int passengerCount;
    public Texture redPlayer, bluePlayer;
    public float timeBetweenGateChanges = 15.0f;
    private float gateChangeTimer = 0.0f;
    private float lastGateChange = 0.0f;
    private bool mouseHeldDown = false;

    //include GameControllerScript
    public GameControllerScript endGame;

    // Start is called before the first frame update
    void Start()
    {
        turnstileQuantity = turnstiles.Count;
        speed = 0.07f;
        
        //speed = 0.2f;//debug speed
        passengerCount = 0;
        currentGate = false;
        newGate = false;
        for (int i = 0; i < passengerList.Count; i++)
        {
            passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine = false;
            passengerRandomiser(passengerList[i]);
            RandomiseGates();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp;
        mouseHeldDown = Input.GetMouseButton(0);
        if ((endGame.gamePause) || (!endGame.gameStarted))
        {

        }
        //pause game if closest passenger is not lined up with a gate
        else if (GamePause())
        {
            //here
            RandomiseGates();
        }
        else if (!GamePause())
        {
            RandomiseGates();
            if (newGate == false)
            {
                for (int i = 0; i < passengerList.Count; i++)
                {
                    temp = passengerList[i].transform.position;
                    temp.z += speed;
                    passengerList[i].transform.position = temp;
                    if (passengerList[i].transform.position.z >= -9.0f && passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine == false && currentGate == false)
                    {
                        passengerCount++;
                        if (passengerCount < 8)
                        {
                            temp.z = -35.0f;

                        }
                        else
                        {
                            passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine = true;
                            temp.z = -1000;
                        }
                        passengerList[i].transform.position = temp;
                        passengerRandomiser(passengerList[i]);
                    }
                    else if (passengerList[i].transform.position.z >= 19.0f && passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine == false && currentGate == true && newGate == false)
                    {
                        passengerCount++;
                        if (passengerCount < 8)
                        {
                            temp.z = -9.0f;

                        }
                        else
                        {
                            passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine = true;
                            temp.z = -10000;
                        }
                        passengerList[i].transform.position = temp;
                        passengerRandomiser(passengerList[i]);
                    }
                }
            }
            else if (newGate == true)
            {
                for (int i = 0; i < passengerList.Count; i++)
                {
                    temp = passengerList[i].transform.position;
                    temp.z += speed;
                    passengerList[i].transform.position = temp;
                    if (passengerList[i].transform.position.z < -9.0f)
                    {
                        temp.z = -9.0f;
                        passengerList[i].transform.position = temp;
                        passengerRandomiser(passengerList[i]);
                    }
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
    //Revamped
    public void passengerRandomiser(GameObject pass)
    {
        int colour = Random.Range(0, 2);//randomiser to choose between red and blue (0 and 1)

        if (colour == 0)//set player to red
        {
            Renderer rend = pass.GetComponent<Renderer>();
            Material tempMat = pass.GetComponent<MeshRenderer>().material;
            tempMat.SetTexture("_MainTex", redPlayer);
            rend.material = tempMat;
            pass.tag = "Red";
        }
        if (colour == 1)//set player to blue
        {
            Renderer rend = pass.GetComponent<Renderer>();
            Material tempMat = pass.GetComponent<MeshRenderer>().material;
            tempMat.SetTexture("_MainTex", bluePlayer);
            rend.material = tempMat;
            pass.tag = "Blue";
        }
        
    }
    
    //Revamped
    private void RandomiseGates()
    {
        gateChangeTimer = endGame.gameTimeElapsed - lastGateChange;
        if (lastGateChange == 0.0f)
        {
            gateChangeTimer = timeBetweenGateChanges + 1;
        }
        bool passengersBeforeLine = true;
        for (int i = 0; i < passengerList.Count; i++)
        {
            if (passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine == true)
            {
                passengersBeforeLine = false;
            }
        }
        if (turnstileQuantity >= 4 && gateChangeTimer > timeBetweenGateChanges && passengersBeforeLine == true)
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
            lastGateChange = endGame.gameTimeElapsed;
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
            for (int i = 0; i < passengerList.Count; i++)
            {
                passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine = false;
            }
        }
        sceneCamera.transform.position = temp;//update camera with new position
    }
   
    //Revamped
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
                        if (raycastInfo.collider.gameObject.transform.parent.tag == "Open" && mouseHeldDown == false)
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
