using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to handle displaying the happy/angry sprites for the passengers
public class MoodResponseScript : MonoBehaviour
{
    //Member Variables
    //
    //Inspector Set Parameters
    //References to the happy & angry passenger sprites
    public GameObject happySprite;
    public GameObject angrySprite;
    //Variable used to track current passenger state
    private int currentState = 0;
    public enum EmotionStates
    {
        NONE = 0, HAPPY = 1, ANGRY = 2
    }
    //Upon start, clear state
    void Start()
    {
        currentState = (int)EmotionStates.NONE;
    }
    //Setter to set state to the input from the parameter provided it is within range
    public void SetState(int stateInput)
    {
        if (stateInput >= (int)EmotionStates.NONE && stateInput <= (int)EmotionStates.ANGRY)
            currentState = stateInput;
    }

    //Update function to display sprite depending on current state
    //0=None=No Sprite
    //1=Happy=Happy Sprite
    //2=Angry=Angry Sprite
    private void Update()
    {
        switch(currentState)
        {
          case (int)EmotionStates.NONE:
                {
                    happySprite.SetActive(false);
                    angrySprite.SetActive(false);
                    break;
                }
            case (int)EmotionStates.HAPPY:
                {
                    happySprite.SetActive(true);
                    angrySprite.SetActive(false);
                    break;
                }
            case (int)EmotionStates.ANGRY:
                {
                    happySprite.SetActive(false);
                    angrySprite.SetActive(true);
                    break;
                }

        }
    }
}
