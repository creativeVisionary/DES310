using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Inclusion of UI components for display of updated text
using UnityEngine.UI;


//Script to handle general game operations
public class GameControllerScript : MonoBehaviour
{
    //Member Variables
    //Inspector Set parameters
    //Scoring
    public int playerScore = 0;
    //Timing
    //Tracks time elapsed within game
    public float gameTimeElapsed = 0.0f;
    //Time at start of script
    float initialTime = 0.0f;
    //Time limit for the current game
    public float timeLimit = 300.0f;
    //Time remaining on the current game
    public float timeRemaining = 0.0f;
    //Track of how long the game has been paused for
    //Used for calculating correct time remaining
    public float timePausedTotal = 0.0f;
    //Used to track time paused before adding to total
    float timePaused = 0.0f;
    //The time of the last pause, used to determine the time paused
    float timeOfLastPause = 0.0f;
    //Integer to give time remaining as a whole integer for ease of display
    int displayTimeRemaining = 0;
    //Game State
    //Boolean variables used to track current state of game(start/pause/ended)
    public bool gameStarted = false;
    public bool gamePause = false;
    public bool gameEnd = false;
    //UI
    //References to UI Objects
    public GameObject userInterface;
    public GameObject pauseUI;
    public GameObject endScreenUI;
    //Keyboard key to pause game
    public KeyCode pauseKey;
    //Used to ensure pause is not rapidly paused and unpaused
    bool keyDown = false;
    //Boolean to control if game UI is necessary
    public bool enableUI = true;
    //Boolean to control if GameController is in 'hub' mode(pause functionality without need for game start or any score/timing)
    public bool hubToggle = false;

    //Functions

    void Update()
    {
       //if the game is started or hub toggle is on and the game is unpaused
        if (((gameStarted == true) || (hubToggle == true))&&(gamePause != true))
        {
            //Pause game if pause key is pressed
            //keydown ensures that game will not be unpaused while holding initial press of pause key
            if (keyDown != true)
            {
                if (Input.GetKeyDown(pauseKey))
                {
                    timeOfLastPause = gameTimeElapsed;
                    gamePause = true;
                    keyDown = true;
                    GetComponent<AudioManager>().PlaySound("Menu_Select_01");
                }
            }
            else
            {
                if (Input.GetKeyUp(pauseKey))
                {
                    keyDown = false;
                }
            }
            pauseUI.SetActive(false);
            DrawUI();
            //Timing code only executes if the game controller is set to game mode as the hub does not require timing of any sort
            if (hubToggle == false)
            {
                timePausedTotal += timePaused;
                timePaused = 0;
                gameTimeElapsed = Time.time - initialTime;
                timeRemaining = timeLimit - gameTimeElapsed + timePausedTotal;//Add time paused back as that is time the game has not been in operation
                displayTimeRemaining = (int)timeRemaining;
                if (timeRemaining < 0.01f)
                {
                    timeRemaining = 0.0f;
                    gamePause = true;
                    //End Game
                    gameEnd = true;
                    GetComponent<AudioManager>().PlaySound("General_Level_Success");
                }
            }
            //Show end game UI if game has ended
        } else if (gameEnd == true)
        {
            pauseUI.SetActive(false);
            endScreenUI.SetActive(true);
            for (int i = 0; i < endScreenUI.transform.childCount; i++)
            {
                if (endScreenUI.transform.GetChild(i).gameObject.tag == "EndScreenScore")
                {
                    endScreenUI.transform.GetChild(i).gameObject.GetComponent<Text>().text = "You scored: " + playerScore.ToString() + " points!";
                }
            }
           
        } else if (gamePause == true)//Show Pause UI if game is paused
        {
            if (hubToggle == false)
            {//Track time paused to later rectify game time calculations
                timePaused = Time.time - gameTimeElapsed - initialTime;
            }
            pauseUI.SetActive(true);
            if (keyDown != true)
            {
                if (Input.GetKeyDown(pauseKey))
                {
                    gamePause = false;
                    keyDown = true;
                    GetComponent<AudioManager>().PlaySound("Menu_Back_01");
                }
            }else
            {
                if(Input.GetKeyUp(pauseKey))
                {
                    keyDown = false;
                }
            }
        }
    }

   
    //Update text boxes with player score and time remaining as an integer
    void DrawUI()
    {
        if (enableUI == true)
        {
            List<GameObject> textBoxesList = new List<GameObject>();
            for (int i = 0; i < userInterface.transform.childCount; i++)
            {
                if (userInterface.transform.GetChild(i).gameObject.tag == "TextBox")
                {
                    textBoxesList.Add(userInterface.transform.GetChild(i).gameObject);
                }
            }
            if (textBoxesList.Count > 1)
            {
                textBoxesList[0].GetComponent<Text>().text = "Player Score:" + playerScore.ToString();
                textBoxesList[1].GetComponent<Text>().text = "Time Remaining:" + displayTimeRemaining.ToString();
            }
        }
    }


    //Starts game operations
    public void StartGame()
    {
        if (gameStarted != true)
        {
            gameStarted = true;
            initialTime = Time.time;
            if (userInterface.GetComponentsInChildren<Button>().Length > 0)
            {
                for (int i = 0; i < userInterface.GetComponentsInChildren<Button>().Length; i++)
                {
                    if (userInterface.GetComponentsInChildren<Button>()[i].tag == "StartButton")
                    {
                        userInterface.GetComponentsInChildren<Button>()[i].gameObject.SetActive(false);
                        GetComponent<AudioManager>().PlaySound("Menu_Select_01");
                        break;
                    }
                }
            }
        }
    }
    //Inverts the pause state(true-> false and vice versa)
    public void PauseUnpauseGame()
    {
        GetComponent<AudioManager>().PlaySound("Menu_Select_01");
        timeOfLastPause = gameTimeElapsed;
        gamePause = !gamePause;
    }

    //Getters
    public int GetPlayerScore()
    {
        return playerScore;
    }

    //Setters
    
    //Sets the player score to the value passed in
    public void SetPlayerScore(int newScore)
    {
        playerScore = newScore;
    }

    //Incriments player score using the value passed in. Decrementation can be done via negative integers being passed through
   public void IncrimentPlayerScore(int scoreIncrementVal)
    {
        playerScore += scoreIncrementVal;
    }
    //Sets the time limit the game will follow(Cannot be done after the game has started running)
   public void SetTimeLimit(int gameTimeLimit)
    {
        if (gameStarted != true)
        {
            timeLimit = gameTimeLimit;
        }
    }

}