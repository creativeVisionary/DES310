using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameControllerScript : MonoBehaviour
{
    public int playerScore = 0;
    public float gameTimeElapsed = 0.0f;
    float initialTime = 0.0f;
    public float timeLimit = 300.0f;
    public float timeRemaining = 0.0f;
    public bool gameStarted = false;
    public GameObject userInterface;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
        if (gameStarted == true)
        {
            gameTimeElapsed = Time.time - initialTime;
            timeRemaining = timeLimit - gameTimeElapsed;
        
            userInterface.GetComponentInChildren<Text>().text = "Player Score:" + playerScore.ToString();
        } else
        {
           
        }
    }

    //Getters
   public int getPlayerScore()
    {
        return playerScore;
    }

    //Setters
    public void setPlayerScore(int newScore)
    {
        playerScore = newScore;
    }
   public void incrimentPlayerScore(int scoreIncrementVal)
    {
        playerScore += scoreIncrementVal;
    }
   public void setTimeLimit(int gameTimeLimit)
    {
        timeLimit = gameTimeLimit;
    }
   public void startGame()
    {
        if (gameStarted != true)
        {
            gameStarted = true;
            initialTime = Time.time;
        }
    }
}
