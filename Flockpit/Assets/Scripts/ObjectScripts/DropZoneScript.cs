using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script handling the operations of objects dedicated as Drop Zone Objectives
//Code is reliant on collisions to mark when an object has arrived
public class DropZoneScript : MonoBehaviour
{
    //Game Controller Objects
    GameControllerScript gameController;
    public GameObject controllerObject;
    //Desired Object Tag
    public string objectString;
    //Colour Option List
    public List<Color> colourList;
    //Need to gain the colour list from a prefab that holds the same colour list the baggage script uses
    public GameObject prefabColourList;
    //Model Option List
    public List<string> modelList;
    //Desired Object Parameters
    public Color desiredCol;
    public string desiredModel;
    //Random Generation
    //Maximum range for which a float is generated
    //Minimum hardcoded at 0 due to the reliance on procedurally calculated 'segments' of values
    //These segments are used to determine which model is used
    public float maxRandRange = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameController = controllerObject.GetComponent<GameControllerScript>();
        colourList = prefabColourList.GetComponent<RandomisedBaggageScript>().colourList;
        for (int i = 0; i <  prefabColourList.transform.childCount; i++)
        {
            modelList.Add(prefabColourList.transform.GetChild(i).gameObject.tag);
        }
        newDesiredObject();
    }

    //When a collision has taken place and the object contains the desired tag,
    //player score is incrimented and the object is marked as expired
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == objectString)
        {
            if ((FindActiveChild(collision.gameObject).tag == desiredModel) && (FindActiveChild(collision.gameObject).GetComponentInChildren<Renderer>().material.color == desiredCol)){
                if (collision.gameObject.GetComponent<ObjectInteractCursorScript>().hasExpired != true)
                {
                    gameController.IncrimentPlayerScore(1);
                    collision.gameObject.GetComponent<ObjectInteractCursorScript>().hasExpired = true;
                    newDesiredObject();
                }
            }
        }
    }

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

    void newDesiredObject()
    {
        //Randomly select parameters
        RandCol();
        RandMesh();

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
            }
        }
    }

}
