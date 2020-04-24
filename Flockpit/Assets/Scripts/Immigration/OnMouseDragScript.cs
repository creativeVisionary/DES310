using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to handle the movement of passengers & collision with gates
public class OnMouseDragScript : MonoBehaviour
{
    //Member Variables

    //Inspector Set Properties
    //Barrier objects to prevent player moving passengers out of bounds
    public Transform[] levelBarriers;
    //Mood Response script set within prefab to display passenger reactions upon event(i.e touching a gate)
    public MoodResponseScript moodScript;
    //Public Parameters
    public int playerID;
    //Boolean to track wether a passenger has passed the gate stop line
    //Should this be true, the passenger becomes uncontrolable
    public bool passedGateLine = false;
    //Game Controller
    GameControllerScript gameController;

    //Functions

    // Start is called before the first frame update
    void Start()
    {
        //Set reference to game controller object within scene
        gameController = FindObjectOfType<GameControllerScript>();
    }

    //Function to move passenger when dragged via mouse/touch controls
    void OnMouseDrag()
    {
        //Only runs if the game is started & unpaused
        if ((!gameController.gamePause)&&(gameController.gameStarted))
        {
            //Move the passenger along the x-axis to match the mouse X
            //Only uses new position if the position is within bounds and the passenger has not passed the gate stop line
            Vector3 newPos = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 21)).x, -0.4f, gameObject.transform.position.z) ;
            if ((newPos.x > levelBarriers[0].position.x ) && (newPos.x < levelBarriers[1].position.x) &&(passedGateLine == false))
            {
                gameObject.transform.position = newPos;
            }
        }
    }
    
    //Upon entering the collision space of a collider marked trigger via inspector
    //Used for gate collisions
    void OnTriggerEnter(Collider col)
    {
        //Game stage variable to track wether first or second set of turnstiles are in play
        int gameStage = GameObject.FindGameObjectWithTag("PassengerManager").GetComponent<PlayerMovementImmigration>().gameStage;
        //Gets the numerical value of the second last character in the string of a game objects name
        //Intended for use with the Turnstiles as their naming scheme ends with a number in (n) format thus the second last character is acquired
        int colliderNum = (int)char.GetNumericValue(col.gameObject.transform.parent.name.ToCharArray()[col.gameObject.transform.parent.name.ToCharArray().Length-2]);
        //Only evaluates the collided object should it belong to the current game stage
        //game stage 1 numbers 0->3, game stage 2 numbers 4->7
        if ((gameStage == 0 && (colliderNum < 4)) || (gameStage == 1 && (colliderNum > 3)))
        {
            //If the colour of the gate matches with the colour of the passenger, reward points,set mood state to happy and play a positive sound effect
            //If the colour of the gate does not match, decrement points,set mood state to angry and play a negative sound effect
            if (col.tag == "Red")
            {
                if (gameObject.tag == "Red")
                {
                    gameController.IncrimentPlayerScore(10);
                    gameController.GetComponent<AudioManager>().PlaySound("General_Positive_Beep_01");
                    moodScript.SetState(1);
                }
                if (gameObject.tag == "Blue")
                {
                    
                    gameController.IncrimentPlayerScore(-5);
                    gameController.GetComponent<AudioManager>().PlaySound("General_Negative_Beep_01");
                    moodScript.SetState(2);
                }

            }
            else if (col.tag == "Blue")
            {
                if (gameObject.tag == "Red")
                {
                    //score.playerScore -= 5;
                    gameController.IncrimentPlayerScore(-5);
                    gameController.GetComponent<AudioManager>().PlaySound("General_Negative_Beep_01");
                    moodScript.SetState(2);
                }
                if (gameObject.tag == "Blue")
                {
                    //score.playerScore += 10;
                    gameController.IncrimentPlayerScore(10);
                    gameController.GetComponent<AudioManager>().PlaySound("General_Positive_Beep_01");
                    moodScript.SetState(1);
                }
            }
        }

    }
}
