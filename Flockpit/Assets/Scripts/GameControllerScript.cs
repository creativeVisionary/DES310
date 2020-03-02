using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Inclusion of UI components for display of updated text
using UnityEngine.UI;


//Script to handle general game operations
public class GameControllerScript : MonoBehaviour
{
    //Scoring
    public int playerScore = 0;
    //Time
    public float gameTimeElapsed = 0.0f;
    float initialTime = 0.0f;
    public float timeLimit = 300.0f;
    public float timeRemaining = 0.0f;
    public float timePausedTotal = 0.0f;
    float timePaused = 0.0f;
    float timeOfLastPause = 0.0f;
    int displayTimeRemaining = 0;
    //Game State
    public bool gameStarted = false;
    public bool gamePause = false;
    public bool gameEnd = false;
    //UI
    public GameObject userInterface;
    public GameObject pauseUI;
    public GameObject endScreenUI;
    public KeyCode pauseKey;
    bool keyDown = false;
    // Update is called once per frame
    void Update()
    {
        //Each frame while the game is running the time elapsed is updated
        //The text displaying the score is also updated
        if ((gameStarted == true)&&(gamePause != true))
        {
            if (keyDown != true)
            {
                if (Input.GetKeyDown(pauseKey))
                {
                    timeOfLastPause = gameTimeElapsed;
                    gamePause = true;
                    keyDown = true;
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
            timePausedTotal += timePaused;
            timePaused = 0;
            gameTimeElapsed = Time.time - initialTime;
            timeRemaining = timeLimit - gameTimeElapsed + timePausedTotal;
            displayTimeRemaining = (int)timeRemaining;
            if (timeRemaining < 0.01f)
            {
                timeRemaining = 0.0f;
                gamePause = true;
                //End Game
                gameEnd = true;
            }
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
           
        } else if (gamePause == true)
        {
            timePaused = Time.time - gameTimeElapsed - initialTime;
            pauseUI.SetActive(true);
            if (keyDown != true)
            {
                if (Input.GetKeyDown(pauseKey))
                {
                    gamePause = false;
                    keyDown = true;
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


    void DrawUI()
    {
        List<GameObject> textBoxesList = new List<GameObject>();
        for (int i = 0; i < userInterface.transform.childCount;i++)
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
    //Starts game operations
   public void StartGame()
    {
        if (gameStarted != true)
        {
            gameStarted = true;
            initialTime = Time.time;
            if (userInterface.GetComponentsInChildren<Button>().Length > 0) {
                for (int i = 0; i < userInterface.GetComponentsInChildren<Button>().Length;i++)
                {
                    if(userInterface.GetComponentsInChildren<Button>()[i].tag == "StartButton")
                    {
                        userInterface.GetComponentsInChildren<Button>()[i].gameObject.SetActive(false);
                        break;
                    }
                }
            }
        }
    }
}