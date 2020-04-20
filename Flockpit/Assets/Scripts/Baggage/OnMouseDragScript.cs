﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnMouseDragScript : MonoBehaviour
{
    public int playerID;
    public GameObject eventText;
    public bool passedGateLine = false;
    public Transform[] levelBarriers;
    GameControllerScript score;
    public MoodResponseScript moodScript;
    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDrag()
    {
        if ((!score.gamePause)&&(score.gameStarted))
        {
            //move the passenger along the x axis
            Vector3 newPos = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 21)).x, -1, gameObject.transform.position.z);
            if ((newPos.x > levelBarriers[0].position.x ) && (newPos.x < levelBarriers[1].position.x) &&(passedGateLine == false))
            {
                gameObject.transform.position = newPos;
            }
        }
    }
    

    void OnTriggerEnter(Collider col)
    {
        //if (col.collider.tag == "redBox")
        //{
        //    eventText.SetActive(true);
        //}
        int gameStage = GameObject.FindGameObjectWithTag("PassengerManager").GetComponent<PlayerMovementImmigration>().gameStage;
        int colliderNum = (int)char.GetNumericValue(col.gameObject.transform.parent.name.ToCharArray()[col.gameObject.transform.parent.name.ToCharArray().Length-2]);
        if ((gameStage == 0 && (colliderNum < 4)) || (gameStage == 1 && (colliderNum > 3)))
        {
            if (col.tag == "Red")
            {
                if (gameObject.tag == "Red")
                {
                    eventText.GetComponent<TextMesh>().text = "Correct";
                    //score.playerScore += 10;
                    score.IncrimentPlayerScore(10);
                    score.GetComponent<AudioManager>().PlaySound("General_Positive_Beep_01");
                    moodScript.SetState(1);
                }
                if (gameObject.tag == "Blue")
                {
                    eventText.GetComponent<TextMesh>().text = "Wrong";
                    //score.playerScore -= 5;
                    score.IncrimentPlayerScore(-5);
                    score.GetComponent<AudioManager>().PlaySound("General_Negative_Beep_01");
                    moodScript.SetState(2);
                }

            }
            else if (col.tag == "Blue")
            {
                if (gameObject.tag == "Red")
                {
                    eventText.GetComponent<TextMesh>().text = "Wrong";
                    //score.playerScore -= 5;
                    score.IncrimentPlayerScore(-5);
                    score.GetComponent<AudioManager>().PlaySound("General_Negative_Beep_01");
                    moodScript.SetState(2);
                }
                if (gameObject.tag == "Blue")
                {
                    eventText.GetComponent<TextMesh>().text = "Correct";
                    //score.playerScore += 10;
                    score.IncrimentPlayerScore(10);
                    score.GetComponent<AudioManager>().PlaySound("General_Positive_Beep_01");
                    moodScript.SetState(1);
                }
            }
        }
        eventText.SetActive(true);

    }
}
