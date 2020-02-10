using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomisedBaggageScript : MonoBehaviour
{
    Material objmat;
    public float maxRandRange = 100.0f;
    //Reliant on minimum being 0.0f
    float minRandRange = 0.0f;
    public List<Color> colourList;
    // Start is called before the first frame update
    void Start()
    {
        int numOfVariations = colourList.Count;
        objmat = GetComponent<Renderer>().material;
        objmat.color = Color.white;
        float randValue = Random.Range(minRandRange, maxRandRange);
        float rangeSegment = maxRandRange / numOfVariations;
        for (int i = 0; i < numOfVariations; i++)
        {
            float lowVal = rangeSegment * i;
            float highVal = rangeSegment * (i + 1);
            if ((randValue <= highVal)&&(randValue > lowVal))
            {
                objmat.color = colourList[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
