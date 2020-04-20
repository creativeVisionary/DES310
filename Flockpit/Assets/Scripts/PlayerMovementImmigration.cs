using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementImmigration : MonoBehaviour
{
    //Keep quantity of turnstiles to multiples of 4
    [Header("Turnstiles")]
    public List<GameObject> turnstiles;
    public List<GameObject> stopLines;
    public float raycastDistance;
    private int turnstileQuantity = 0;
    public float timeBetweenGateChanges = 15.0f;
    private float gateChangeTimer = 0.0f;
    private float lastGateChange = 0.0f;
    public bool currentGate;
    [Header("Passengers")]
    [Range(0.01f, 5.0f)]
    public float speed;
    public int passengerCount;
    public Material redPlayer, bluePlayer;
    private bool mouseHeldDown = false;
    [Header("Other")]
    public float stageSwitchPoint = 0;
    public int gameStage = 0;
    public StopwatchScript stopWatchObject;
    //
    //
    public List<GameObject> passengerList;
    public List<GameObject> passenger1Body;
    public List<GameObject> passenger2Body;
    public List<GameObject> passenger3Body;
    public Camera sceneCamera;
    public Vector3 secondStageCameraPos;
    public float cameraMoveSpeed;

    //include GameControllerScript
    public GameControllerScript endGame;
    //

    // Start is called before the first frame update
    void Start()
    {
        turnstileQuantity = turnstiles.Count;
        speed = 0.07f;
        
        //speed = 0.2f;//debug speed
        passengerCount = 0;
        currentGate = false;
        for (int i = 0; i < passengerList.Count; i++)
        {
            passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine = false;
            passengerList[i].GetComponent<OnMouseDragScript>().playerID = i;
            passengerRandomiser(passengerList[i]);
            RandomiseGates();
        }
        stopWatchObject.SetTickDownTime(timeBetweenGateChanges);
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
            if (endGame.timeRemaining < stageSwitchPoint && gameStage < 1 && endGame.timeRemaining != 0)
            {
                SwitchStage();
            }
            if (gameStage > 0 && sceneCamera.transform.position != secondStageCameraPos)
            {
                MoveCamera();
                for (int i = 0; i < passengerList.Count; i++)
                {
                    passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine = false;
                }
            }
            RandomiseGates();
                for (int i = 0; i < passengerList.Count; i++)
                {
                    temp = passengerList[i].transform.position;
                    temp.z += speed;
                    passengerList[i].transform.position = temp;
                }
        }
    }
    //Revamped
    public void passengerRandomiser(GameObject pass)
    {
        int colour = Random.Range(0, 2);//randomiser to choose between red and blue (0 and 1)
        Material newPassenger;//material used to texture the passengers

        if (colour == 0)//set passenger to red
        {
            newPassenger = redPlayer;
            pass.tag = "Red";
        }
        else//set passenger to blue
        {
            newPassenger = bluePlayer;
            pass.tag = "Blue";
        }

        if (pass.GetComponent<OnMouseDragScript>().playerID == 0)//passenger 1
        {
            for (int j = 0; j < passenger1Body.Count - 2; j++)//change material for every past but legs
            {
                passenger1Body[j].GetComponent<MeshRenderer>().material = newPassenger;
                
            }
            //texture legs (different mesh renderer needed)
            passenger1Body[4].GetComponent<SkinnedMeshRenderer>().material = newPassenger;
            passenger1Body[5].GetComponent<SkinnedMeshRenderer>().material = newPassenger;
            
        }
        if (pass.GetComponent<OnMouseDragScript>().playerID == 1)//passenger 2
        {
            for (int j = 0; j < passenger2Body.Count - 2; j++)//change material for every past but legs
            {
                passenger2Body[j].GetComponent<MeshRenderer>().material = newPassenger;

            }
            //texture legs (different mesh renderer needed)
            passenger2Body[4].GetComponent<SkinnedMeshRenderer>().material = newPassenger;
            passenger2Body[5].GetComponent<SkinnedMeshRenderer>().material = newPassenger;

        }
        if (pass.GetComponent<OnMouseDragScript>().playerID == 2)//passenger 3
        {
            for (int j = 0; j < passenger3Body.Count - 2; j++)//change material for every past but legs
            {
                passenger3Body[j].GetComponent<MeshRenderer>().material = newPassenger;

            }
            //texture legs (different mesh renderer needed)
            passenger3Body[4].GetComponent<SkinnedMeshRenderer>().material = newPassenger;
            passenger3Body[5].GetComponent<SkinnedMeshRenderer>().material = newPassenger;

        }
        pass.GetComponent<MoodResponseScript>().SetState(0);//reset reaction UI
    }
    
    public void MoveCamera()
    {
        Vector3 cameraMoveVector = new Vector3(0, 0, 0);
        Vector3 newCameraPos = sceneCamera.transform.position;
        cameraMoveVector = secondStageCameraPos - sceneCamera.transform.position;
        cameraMoveVector.z /= cameraMoveVector.z;
        cameraMoveVector *= cameraMoveSpeed * Time.deltaTime;
        newCameraPos += cameraMoveVector;
        if (newCameraPos.x > secondStageCameraPos.x)
        {
            newCameraPos.x = secondStageCameraPos.x;
        }
        if (newCameraPos.y > secondStageCameraPos.y)
        {
            newCameraPos.y = secondStageCameraPos.y;
        }
        if (newCameraPos.z > secondStageCameraPos.z)
        {
            newCameraPos.z = secondStageCameraPos.z;
        }
        sceneCamera.transform.position = newCameraPos;

    }

    public void SwitchStage()
    {
        gameStage++;
        //
        for (int i = 0; i < passengerList.Count; i++)
        {
            passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine = false;
        }
        //
        char[] resetBarrierName = GameObject.FindGameObjectWithTag("ResetBarrier").name.ToCharArray();
        if (resetBarrierName[resetBarrierName.Length-1] == '1')
        {
            GameObject.FindGameObjectWithTag("ResetBarrier").SetActive(false);
        }
        gateChangeTimer = 20;
        RandomiseGates();
    }

    //Revamped
   public void RandomiseGates()
    {
        int[] currentOpenGates = OpenGateCheck();
        gateChangeTimer = endGame.gameTimeElapsed - lastGateChange;
        if (lastGateChange == 0.0f)
        {
            gateChangeTimer = timeBetweenGateChanges + 1;
        }
        bool passengersBeforeLine = true;
        RESTARTGATES:
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
            for (int p = 0 + gameStage*4; p < 4 + gameStage*4; p++)
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
                        turnstiles[i].GetComponentInChildren<AnimationScript>().SetGateOpen();
                    } else if (i != openGate[0] && i!= openGate[1])
                    {
                        turnstiles[i].gameObject.tag = "Closed";
                        turnstiles[i].GetComponentInChildren<TextMesh>().text = "X";
                        turnstiles[i].GetComponentInChildren<AnimationScript>().SetGateClosed();
                    }
                }
            }
            if (OpenGateCheck().Equals(currentOpenGates))
            {
                goto RESTARTGATES;
            }
            lastGateChange = endGame.gameTimeElapsed;
            stopWatchObject.StartTimer();
        }
    }

    int[] OpenGateCheck()
    {
        List<int> opengateIndices = new List<int>();
        for (int i = 0; i < turnstileQuantity;i++)
        {
            if(turnstiles[i].tag == "Open")
            {
                opengateIndices.Add(i);
            }
        }
        if (opengateIndices.Count > 0)
        {
            return opengateIndices.ToArray();
        }
            return null;
    }
   
    //Revamped
    bool GamePause()
    {
        if (currentGate == false)
        {
            for (int i = 0; i < passengerList.Count; i++)
            {
                //if player has reached the wait line
                if ((passengerList[i].transform.position.z >= stopLines[gameStage].transform.position.z) && (passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine == false))
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
