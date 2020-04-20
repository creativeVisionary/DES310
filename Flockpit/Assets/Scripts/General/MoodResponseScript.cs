using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodResponseScript : MonoBehaviour
{
    public GameObject happySprite;
    public GameObject angrySprite;
    private int currentState = 0;
    public enum EmotionStates
    {
        NONE = 0, HAPPY = 1, ANGRY = 2
    }
    void Start()
    {
        currentState = (int)EmotionStates.NONE;
    }

    public void SetState(int stateInput)
    {
        if (stateInput >= (int)EmotionStates.NONE && stateInput <= (int)EmotionStates.ANGRY)
            currentState = stateInput;
    }
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
