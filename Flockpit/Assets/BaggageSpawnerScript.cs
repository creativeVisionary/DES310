using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BaggageSpawnerScript : MonoBehaviour
{
    public float respawnTime = 2.0f;
    public GameObject baggage;
    float timer = 0.0f;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        if (currentTime - timer > respawnTime)
        {
            string objName = "Baggage " + counter.ToString();
             baggage = new GameObject(objName);
            //Spawn baggage
            Instantiate(baggage,this.transform.position,Quaternion.identity,this.transform);
            timer = currentTime;
            counter++;
        }
    }
}
