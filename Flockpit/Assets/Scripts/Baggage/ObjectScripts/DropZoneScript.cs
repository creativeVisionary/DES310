﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script handling the operations of objects dedicated as Drop Zone Objectives
//Code is reliant on collisions to mark when an object has arrived
public class DropZoneScript : MonoBehaviour
{
    //Game Controller Objects
    GameControllerScript gameController;
    //Bag History Object
    BaggageHistoryScript bagHistory;
    //Current Desired Bag properties
    BaggageHistoryScript.BaggageItem currentBag;
    //Game Controller gameobject
    public GameObject controllerObject;
    //Mood Response Object
    public MoodResponseScript moodScript;
    //Desired Object Tag
    public string objectString;
    //Bag Variation Lists
    //Colour Option List
    public List<Color> colourList;
    //Texture List
    public List<Texture> texList;
    //Need to gain the colour list from a prefab that holds the same colour list the baggage script uses
    public GameObject prefabColourList;
    //Model Option List
    public List<string> modelList;
    //Desired Object Parameters
    public Color desiredCol;
    public string desiredModel;
    public Texture desiredTex;
    private int desiredModelIndex;
    //Random Generation
    //Maximum range for which a float is generated
    //Minimum hardcoded at 0 due to the reliance on procedurally calculated 'segments' of values
    //These segments are used to determine which model is used
    public float maxRandRange = 100.0f;
    //boolean to check if drop zone has recieved a bag
    private bool recievedBag = false;
    //boolean to check if drop zone has entered the scene
    private bool sceneEntered = false;
    //Drop Zone speed parameter
    [Range(0.0f,5.0f)]
    public float passengerSpeed = 1.0f;
    //List of display objects for the desired bag display
    private List<GameObject> displayObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        gameController = controllerObject.GetComponent<GameControllerScript>();
        bagHistory = controllerObject.GetComponent<BaggageHistoryScript>();
        colourList = prefabColourList.GetComponent<RandomisedBaggageScript>().colourList;
        texList = prefabColourList.GetComponent<RandomisedBaggageScript>().bagTextures;
        for (int i = 0; i <  prefabColourList.transform.childCount; i++)
        {
            modelList.Add(prefabColourList.transform.GetChild(i).gameObject.tag);
        }
        currentBag = new BaggageHistoryScript.BaggageItem();
        newDesiredObject();
    }

    private void Update()
    {
        //If the game is in operation, add all objects marked DisplayObj to the displayObjects list
        if (gameController.gamePause == false && gameController.gameStarted == true && gameController.gameEnd == false)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).gameObject.tag == "DisplayObj")
                {
                    displayObjects.Add(this.transform.GetChild(i).gameObject);
                }
            }
            //Display desired bag once scene has been entered,
            //once any bag has been acquired, hide the desired bag display and leave the scene
            if (sceneEntered == true)
            {
                if (recievedBag == false)
                {
                    DisplayDesiredObject();
                }
                else
                {
                    HideDesiredObject();
                    LeaveScene();
                }
            }
            else
            {//If a bag has been recieved hide the display, 
                //if no bag has been reciecved display the desired object display
                if (recievedBag == true)
                {
                    HideDesiredObject();
                }
                else
                {
                    DisplayDesiredObject();
                }
                //Enter the scene
                EnterScene();
            }
        }
    }

    //When a collision has taken place and the object contains the desired tag,
    //player score is incrimented and the object is marked as expired
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == objectString && recievedBag == false)
        {
            if ((FindActiveChild(collision.gameObject).tag == desiredModel) && (FindActiveChild(collision.gameObject).GetComponentInChildren<Renderer>().material.color == desiredCol) && (FindActiveChild(collision.gameObject).GetComponentInChildren<Renderer>().material.GetTexture("_MainTex") == desiredTex))
            {
                if (collision.gameObject.GetComponent<ObjectInteractCursorScript>().hasExpired != true)
                {
                    gameController.IncrimentPlayerScore(10);
                    gameController.GetComponent<AudioManager>().PlaySound("General_Positive_Beep_01");
                    collision.gameObject.GetComponent<ObjectInteractCursorScript>().hasExpired = true;
                    //Set mood to Happy
                    moodScript.SetState(1);
                }
            }
            else {
                gameController.IncrimentPlayerScore(-5);
                gameController.GetComponent<AudioManager>().PlaySound("General_Negative_Beep_01");
                collision.gameObject.GetComponent<ObjectInteractCursorScript>().hasExpired = true;
                //Set mood to Angry
                moodScript.SetState(2);
            }
            recievedBag = true;
        } else if (collision.gameObject.tag == "ResetBarrier")
        {
            Reinitialise();
        }
    }
    //Function to find the first child that is active within a game objects children]
    //Used to find the active child within a collided bag for checking the model
    GameObject FindActiveChild(GameObject parent)
    {
        int numChildren = parent.transform.childCount;
        for (int i = 0; i < numChildren; i++)
        {
            if (parent.transform.GetChild(i).gameObject.activeSelf == true)
            {
                return parent.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

    //Generate a series of properties for a new desired object
    void newDesiredObject()
    {
        OBJECTSTART:
        //Randomly select parameters
        RandCol();
        RandMesh();
        RandTexture();
        currentBag.bagCol = desiredCol;
        currentBag.bagModel = desiredModel;
        currentBag.bagTex = desiredTex;
        //If the bag has been requested recently, repeat the process until a unique bag combination has been found
        if (!bagHistory.IsInHistory(currentBag))
        {
            DisplayDesiredObject();
            bagHistory.AddToHistory(currentBag);
        } else
        {
            goto OBJECTSTART;
        }
    }

    //Calculation of a random colour from a list
    //Colours can be set within the constructor
    void RandCol()
    {
        int numOfVariations = colourList.Count;
        float randValue = Random.Range(0.0f, maxRandRange);
        //Calculate the magnitude of each segment of values within the range
        //For example, with 4 variations and a maximum of 100 the values between 0 and 25 would correspond to the first colour in the list
        float rangeSegment = maxRandRange / numOfVariations;
        //Check the value against each segment and if it should fit within the current segment,
        //Loop through each renderer the object has within its children objects and set the colour on each
        for (int i = 0; i < numOfVariations; i++)
        {
            float lowVal = rangeSegment * i;
            float highVal = rangeSegment * (i + 1);
            if ((randValue <= highVal) && (randValue > lowVal))
            {
                int matNumber = GetComponentInChildren<Renderer>().materials.Length;
                for (int l = 0; l < matNumber; l++)
                {
                    desiredCol = colourList[i];
                }
            }
        }
    }

    //Calculation of a random model from the children models within the object
    //Models are dictated by children of a central game object within the inspector
    void RandMesh()
    {
        int numOfVariations = modelList.Count;
        float randValue = Random.Range(0.0f, maxRandRange);
        //Calculate the magnitude of each segment of values within the range
        //For example, with 4 variations and a maximum of 100 the values between 0 and 25 would correspond to the first model in the heirarchy
        float rangeSegment = maxRandRange / numOfVariations;
        //Check the value against each segment and if it should fit within the current segment,
        //activate the model for that segment
        //Should the segment not match, deactivate the model to ensure only the correct model is ever activated
        for (int i = 0; i < numOfVariations; i++)
        {
            float lowVal = rangeSegment * i;
            float highVal = rangeSegment * (i + 1);
            if ((randValue <= highVal) && (randValue > lowVal))
            {
               desiredModel = modelList[i];
                desiredModelIndex = i;
            }
        }
    }
    //Selects a random texture from two choices
    //Case required as different models have different variations
    void RandTexture()
    {
        int modelNumber = desiredModelIndex;
        switch (modelNumber)
        {
            //Model 01
            case 0:
                {
                    if (UnityEngine.Random.Range(0, 101) < 50)
                    {
                        desiredTex = texList[0];
                    }

                    else
                    {
                        desiredTex = texList[3];
                    }
                    break;
                }
            //Model 02
            case 1:
                {
                    if (UnityEngine.Random.Range(0, 101) < 50)
                    {
                        desiredTex = texList[1];
                    }

                    else
                    {
                        desiredTex = texList[4];
                    }
                    break;
                }
            //Model 03
            case 2:
                {
                    if (UnityEngine.Random.Range(0, 101) < 50)
                    {
                        desiredTex = texList[2];
                    }

                    else
                    {
                        desiredTex = texList[5];
                    }
                    break;
                }
            //Model Number Not Found
            default:
                {
                    Debug.LogError("Model Number Not Found");
                    break;
                }


        }
    }

    //Function to display the desired object as a gameobject
    void DisplayDesiredObject()
    {
        for (int i = 0; i < displayObjects.Count; i++)
        {
            displayObjects[i].SetActive(true);
        }
        for (int i = 0; i < modelList.Count; i++) {
            if (modelList[i] == desiredModel) {
                prefabColourList.transform.GetChild(i).gameObject.SetActive(true);
           } else
            {
                prefabColourList.transform.GetChild(i).gameObject.SetActive(false);
            }
          }

        int matNumber = prefabColourList.GetComponentInChildren<Renderer>().materials.Length;
        for (int l = 0; l < matNumber; l++)
        {
            prefabColourList.GetComponentInChildren<Renderer>().materials[l].color = desiredCol;
        }
        prefabColourList.GetComponentInChildren<Renderer>().materials[0].SetTexture("_MainTex",desiredTex);
    }

    //Hide the display objects and as such hide the desired object display
    void HideDesiredObject()
    {
        for (int i = 0; i < displayObjects.Count; i++)
        {
            displayObjects[i].SetActive(false);
        }
    }

    //Function to move the drop zone towards an allocated stop point within the scene
    void EnterScene()
    {
        Vector3 goalStopPoint = GameObject.FindGameObjectWithTag("StopPoint").transform.position;
        if (this.transform.position.z > goalStopPoint.z -3)
        {
         Vector3 sceneEnterVector = new Vector3(0, 0, this.transform.position.z - goalStopPoint.z);
            sceneEnterVector.z /= sceneEnterVector.z*-1;
            Vector3 newPos = this.transform.position;
            newPos += sceneEnterVector * Time.deltaTime * passengerSpeed;
            this.transform.position = newPos;
        } else
        {
            sceneEntered = true;
        }
    }
    //Function to move the drop zone away from its initial spawn point, thus eventually leaving the scene view
    void LeaveScene()
    {
        Vector3 sceneLeaveVector = new Vector3(0, 0, GameObject.FindGameObjectWithTag("SpawnPoint").transform.position.z - this.transform.position.z);
        sceneLeaveVector.z /= sceneLeaveVector.z*-1;
            Vector3 newPos = this.transform.position;
            newPos += sceneLeaveVector * Time.deltaTime * passengerSpeed;
            this.transform.position = newPos;
    }
    //Function to reinitialise drop zones after a reset
    void Reinitialise()
    {
        newDesiredObject();
        Vector3 spawnPos = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
        spawnPos.y = 3.061016f;
        spawnPos.x = this.transform.position.x;
        this.transform.position = spawnPos;
        sceneEntered = false;
        recievedBag = false;
        moodScript.SetState(0);
    }
}
