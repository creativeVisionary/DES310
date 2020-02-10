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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Get current game time in seconds
        float currentTime = Time.time;
        //See if the respawn time has passed since last spawn
        if (currentTime - timer > respawnTime)
        {
            //Update timer to current game time
            timer = currentTime;
            //Spawn baggage and set velocity
            Rigidbody spawnedbaggage = Instantiate(baggage,this.transform.position,Quaternion.identity);
            spawnedbaggage.velocity = launchSpeed * this.transform.forward;;
        }
    }
}
