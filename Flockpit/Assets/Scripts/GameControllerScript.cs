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
    int displayTimeRemaining = 0;
    //Game State
    public bool gameStarted = false;
    public bool gamePause = false;
    //UI
    public GameObject userInterface;
    // Update is called once per frame
    void Update()
    {
        //Each frame while the game is running the time elapsed is updated
        //The text displaying the score is also updated
        if ((gameStarted == true)&&(gamePause != true))
        {
            DrawUI();
            gameTimeElapsed = Time.time - initialTime;
            timeRemaining = timeLimit - gameTimeElapsed;
            displayTimeRemaining = (int)timeRemaining;
            if (timeRemaining < 0.01f)
            {
                timeRemaining = 0.0f;
                gamePause = true;
                //End Game

            }
        } else
        {
           
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
        }
    }
}
