using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Script to handle the game logic of the Immigration game
public class PlayerMovementImmigration : MonoBehaviour
{
    //Member Variables
    //
    //Turnstile Properties 
    [Header("Turnstiles")]
    //List of Turnstile Game Objects
    public List<GameObject> turnstiles;
    //List to contain the history of the last turnstile combinations via strings
    private List<string> turnstileTagHistory = new List<string>();
    //List of gate lines for which the passengers stop before proceeding
    public List<GameObject> stopLines;
    //Distance of the raycast line sent from the passenger
    //Used to determine if an open gate is ahead
    public float raycastDistance;
    //Variable to track quantity of turnstiles within scene
    private int turnstileQuantity = 0;
    //Time between changing of gate order
    public float timeBetweenGateChanges = 15.0f;
    //Variable to act as a timer for the gate change functionality
    private float gateChangeTimer = 0.0f;
    //Time of the last gate change, used to compute wether a new gate change should take place
    private float lastGateChange = 0.0f;
    //Passenger Properties
    [Header("Passengers")]
    [Range(0.01f, 5.0f)]
    //Speed at which the passengers travel
    public float speed;
    //Material objects for the red and blue passengers
    public Material redPlayer, bluePlayer;
    //Boolean used to track current mouse button status
    //Required for stop line functionality
    private bool mouseHeldDown = false;
    //Lists of passenger game objects
    public List<GameObject> passengerList;
    public List<GameObject> passenger1Body;
    public List<GameObject> passenger2Body;
    public List<GameObject> passenger3Body;
    //Misc Properties
    [Header("Other")]
    //Time at which the game stage switches from stage 1 to stage 2
    public float stageSwitchPoint = 0;
    //Variable storing the current game stage
    public int gameStage = 0;
    //Reference to the gate change stop watch object
    public StopwatchScript stopWatchObject;
    //Reference to Scene Camera
    //Used for move camera functionality when switching game states
    public Camera sceneCamera;
    //Position for which the camera will travel to while switching game states
    public Vector3 secondStageCameraPos;
    //Speed at which the camera will move
    public float cameraMoveSpeed;
    //Game Controller
    public GameControllerScript gameController;

    //Functions

    // Start is called before the first frame update
    void Start()
    {
        //Initialise quantity and speed
        //Speed can be changed via inspector during runtime
        turnstileQuantity = turnstiles.Count;
        speed = 0.07f;
        //Initialise Passengers
        for (int i = 0; i < passengerList.Count; i++)
        {
            passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine = false;
            passengerList[i].GetComponent<OnMouseDragScript>().playerID = i;
            passengerRandomiser(passengerList[i]);
        }
        //Set time for stop watch to tick down from
        stopWatchObject.SetTickDownTime(timeBetweenGateChanges);
    }

    // Update is called once per frame
    void Update()
    {
        //New position for passenger movement
        Vector3 newPos;
        //Get status of mouse/touch screen
        mouseHeldDown = Input.GetMouseButton(0);
        //Pause game if the game is either paused or not started
        if ((gameController.gamePause) || (!gameController.gameStarted))
        {

        }
        //Pause game if closest passenger is not lined up with a gate
        else if (GamePause())
        {
        }
        else if (!GamePause())
        {
            //Switch to the second game stage if the required amount of time has passed
            if (gameController.timeRemaining < stageSwitchPoint && gameStage < 1 && gameController.timeRemaining != 0)
            {
                SwitchStage();
            }
            //Move the camera to its second stage position if the second stage is active and the camera has not arrived
            if (gameStage > 0 && sceneCamera.transform.position != secondStageCameraPos)
            {
                MoveCamera();
                //Keep passengers set as not passed gate line as to ensure they move to the second stage seamlessly 
                for (int i = 0; i < passengerList.Count; i++)
                {
                    passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine = false;
                }
            }
            //Randomise the open/close order of the gates
            RandomiseGates();
            //Move all the passengers forward
                for (int i = 0; i < passengerList.Count; i++)
                {
                    newPos = passengerList[i].transform.position;
                    newPos.z += speed;
                    passengerList[i].transform.position = newPos;
                }
        }
    }

    //Check if the game should be paused due to the passengers positions in relation to the stop lines
    bool GamePause()
    {
        //Loop through all the passengers in the game
        for (int i = 0; i < passengerList.Count; i++)
        {
            //If player has reached the wait line for the current game stage
            if ((passengerList[i].transform.position.z >= stopLines[gameStage].transform.position.z) && (passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine == false))
            {
                //Raycast from that passenger to check if an open gate is ahead
                Ray passengerRaycast = new Ray();
                passengerRaycast.origin = passengerList[i].transform.position;
                passengerRaycast.direction = new Vector3(0.0f, 0.0f, 1.0f);
                RaycastHit raycastInfo = new RaycastHit();
                Physics.Raycast(passengerRaycast, out raycastInfo, raycastDistance);
                if (raycastInfo.collider != null)
                {
                    //If an open gate is ahead and the player is not holding down the mouse, keep the game unpaused
                    if (raycastInfo.collider.gameObject.transform.parent.tag == "Open" && mouseHeldDown == false)
                    {
                        passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine = true;
                        return false;
                    }
                    else//If not infront of an available gate or the mouse is held down, keep game paused
                    { return true; }
                }
                else //If no gate is ahead, keep game paused
                {
                    return true;
                }
            }
        }
        //None of the passengers have reached the wait line so the continue to move forward until they do
        return false;
    }

    //Function to randomise passenger properties
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
    
    //Function to move the camera towards the second stage point
    public void MoveCamera()
    {
        //Calculate the vector for which the camera must move to reach the destination
        //Apply the speed to that vector along with delta time
        //If new position is beyond the second stage position, set it to the second stage position
        //Apply the newly calculated position to the scene camera
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

    //Function to handover elements to second stage
    public void SwitchStage()
    {
        //Incriment game stage variable
        gameStage++;
        //Reset passed gate line variable on all passengers to ensure movement through 1st stage gates
        for (int i = 0; i < passengerList.Count; i++)
        {
            passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine = false;
        }
        //Deactivate the first reset barrier in the scene
        char[] resetBarrierName = GameObject.FindGameObjectWithTag("ResetBarrier").name.ToCharArray();
        if (resetBarrierName[resetBarrierName.Length-1] == '1')
        {
            GameObject.FindGameObjectWithTag("ResetBarrier").SetActive(false);
        }
        //Set gate change timer & randomise the gates
        gateChangeTimer = timeBetweenGateChanges;
        RandomiseGates();
    }

    //Function to randomise gate order
   public void RandomiseGates()
    {
        //Get an array of the indices of all open gates
        int[] currentOpenGates = OpenGateCheck();
        //Set last gate change if it is currently equal to 0(due to start of the game)
        gateChangeTimer = gameController.gameTimeElapsed - lastGateChange;
        if (lastGateChange == 0.0f)
        {
            gateChangeTimer = timeBetweenGateChanges + 1;
        }
        bool passengersBeforeLine = true;
        RESTARTGATES:
        //If there are any passengers that have passed the gate line set passengers before line to false
        //This is to avoid passengers having gates changed on them as they travel through
        for (int i = 0; i < passengerList.Count; i++)
        {
            if (passengerList[i].GetComponent<OnMouseDragScript>().passedGateLine == true)
            {
                passengersBeforeLine = false;
            }
        }
        //If there are enough turnstiles, the time between gate changes has been met, passengers are all before the gate line and the game has been active for any amount of time(due to bugs with timing)
        if (turnstileQuantity >= 4 && gateChangeTimer > timeBetweenGateChanges && passengersBeforeLine == true && gameController.gameTimeElapsed > 0)
        {
            //Clear the tag history and get the current state of the gates
            turnstileTagHistory.Clear();
            for (int i = 0; i < turnstiles.Count; i++)
            {
                turnstileTagHistory.Add(turnstiles[i].gameObject.tag);
            }
            //Array to hold the indices of the open gates
            int[] openGate = new int[2];
            //Two Colour index lists, one for red and one for blue
            List<int> firstColIndex = new List<int>();
            List<int> secondColIndex = new List<int>();
            //Array of colour tags
            string[] colourTags = new string[2] {"Red","Blue" };
            //Loop through each turnstile in the current game stage and find their gate
            //If the gate is red add their index to the first col index, if not add to the second
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
            //Choose a random index from each colIndex list to act as the new open gate for each colour
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
            //Set the new gates to open and the rest of the gates to closed
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
            //If no change in the gate order has occured, go back to RESTARTGATES and repeat the process, thus ensuring a unqiue combination after some time
            if (OpenGateCheck().Equals(currentOpenGates))
            {
                goto RESTARTGATES;
            }
            //Play Animations once Final Gate Selection Has Been Found
            for (int i = 0; i < turnstiles.Count; i++)
            {
                //Only play animation if turnstile state has changed
                if (turnstiles[i].gameObject.tag != turnstileTagHistory[i])
                {
                    if (turnstiles[i].gameObject.tag == "Closed")
                    {
                        turnstiles[i].GetComponentInChildren<AnimationScript>().SetGateClosed();
                    }
                    else if (turnstiles[i].gameObject.tag == "Open")
                    {
                        turnstiles[i].GetComponentInChildren<AnimationScript>().SetGateOpen();
                    }
                }
            }
            //Set last gate change to current time elapsed and start stopwatch
                lastGateChange = gameController.gameTimeElapsed;
            stopWatchObject.StartTimer();
        }
    }

    //Return the indices of the currently open gates
    int[] OpenGateCheck()
    {
        //Loop through all  the gates in the scene and add the indices of those tagged as open to a list
        List<int> opengateIndices = new List<int>();
        for (int i = 0; i < turnstileQuantity;i++)
        {
            if(turnstiles[i].tag == "Open")
            {
                opengateIndices.Add(i);
            }
        }
        //If the list has at least one element, return it as an array
        //If the list is empty, return null
        if (opengateIndices.Count > 0)
        {
            return opengateIndices.ToArray();
        }
            return null;
    }
 
 
}
