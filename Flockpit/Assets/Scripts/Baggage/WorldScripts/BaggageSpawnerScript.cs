using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code designed with the aid of the following documentation
//https://docs.unity3d.com/Manual/InstantiatingPrefabs.html


public class BaggageSpawnerScript : MonoBehaviour
{
    //Range to allow for controlled use within inspector
    [Range(0.0f,30.0f)]
    //Respawn time is measured in seconds
    public float respawnTime = 2.0f;
    [Range(0.0f,20.0f)]
    public float launchSpeed = 5.0f;
    //Keep null, this object is set within the inspector
    public Rigidbody baggage;
    //Holds the time at the last point respawn time condition was met(or 0 at start)
    float timer = 0.0f;
    //Game Controller
    public GameControllerScript gameController;
    //Reference to a drop zone to ensure its desired bag is spawned after some point
    public DropZoneScript dropZone;
    //Time between forcing the drop of the drop zones desired bag
    public float timeBetweenAssuredDrop = 10.0f;
    //Time from the last forced drop
    public float timeFromLastDrop = 0.0f;

    // Update is called once per frame
    void Update()
    {
        //If game is in operation
        if ((gameController.gamePause == false) && (gameController.gameStarted == true))
        {
            //Get current game time in seconds
            float currentTime = Time.time;
            //See if the respawn time has passed since last spawn
            if (currentTime - timer > respawnTime)
            {
                //Update timer to current game time
                timer = currentTime;
                //Spawn baggage and set velocity
                Rigidbody spawnedbaggage = Instantiate(baggage, this.transform.position, Quaternion.identity);
                spawnedbaggage.velocity = launchSpeed * this.transform.forward;
                if (Time.time - timeFromLastDrop > timeBetweenAssuredDrop)
                {
                    //Spawn an assured desired bag
                    SetBaggageProperties(spawnedbaggage.gameObject, dropZone.desiredModel, dropZone.desiredCol, dropZone.desiredTex);
                    //Set time from last drop to be current time
                    timeFromLastDrop = Time.time;
                }
            }
        }
    }

    void SetBaggageProperties(GameObject bag,string modelName,Color modelColour,Texture modelTexture)
    {
        //Set Model
        for (int i = 0; i < bag.transform.childCount; i++)
        {
            if (modelName == bag.transform.GetChild(i).gameObject.tag)
            {
                bag.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                bag.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        //Set Colour
        int matNumber = bag.GetComponentInChildren<Renderer>().materials.Length;
        for (int l = 0; l < matNumber; l++)
        {
            bag.GetComponentInChildren<Renderer>().materials[l].color = modelColour;
        }
        //Set Texture
        bag.GetComponentInChildren<Renderer>().materials[0].SetTexture("_MainTex", modelTexture);
    }
}