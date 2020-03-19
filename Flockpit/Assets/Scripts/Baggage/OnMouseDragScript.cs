﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnMouseDragScript : MonoBehaviour
{
    public GameObject eventText;
    public bool passedGateLine = false;
    public Transform[] levelBarriers;
    GameControllerScript score;
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
            Vector3 newPos = new Vector3((Input.mousePosition.x - 564) / 20, 0, gameObject.transform.position.z);
            if ((newPos.x < levelBarriers[1].position.x ) && (newPos.x > levelBarriers[0].position.x) &&(passedGateLine == false))
            {
                gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 21)).x,0,gameObject.transform.position.z);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        //if (col.collider.tag == "redBox")
        //{
        //    eventText.SetActive(true);
        //}
        if (col.tag == "Red")
        {
            if (gameObject.tag == "Red")
            {
                eventText.GetComponent<TextMesh>().text = "Correct";
                //score.playerScore += 10;
                score.IncrimentPlayerScore(10);
            }
            if (gameObject.tag == "Blue")
            {
                eventText.GetComponent<TextMesh>().text = "Wrong";
                //score.playerScore -= 5;
                score.IncrimentPlayerScore(-5);
            }

        }
        else if (col.tag == "Blue")
        {
            if (gameObject.tag == "Red")
            {
                eventText.GetComponent<TextMesh>().text = "Wrong";
                //score.playerScore -= 5;
                score.IncrimentPlayerScore(-5);
            }
            if (gameObject.tag == "Blue")
            {
                eventText.GetComponent<TextMesh>().text = "Correct";
                //score.playerScore += 10;
                score.IncrimentPlayerScore(10);
            }
        }
        eventText.SetActive(true);

    }
}
