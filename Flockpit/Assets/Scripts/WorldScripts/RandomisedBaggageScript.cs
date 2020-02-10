using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomisedBaggageScript : MonoBehaviour
{
    Material objmat;
    Mesh objMesh;
    public float maxRandRange = 100.0f;
    //Reliant on minimum being 0.0f
    float minRandRange = 0.0f;
    public List<Color> colourList;
    public Component[] renderList;
    //Set in inspector using children of parent game object
    public List<GameObject> modelList;
    // Start is called before the first frame update
    void Start()
    {
        RandMesh();
        RandCol();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandCol()
    {
        int numOfVariations = colourList.Count;
        float randValue = Random.Range(minRandRange, maxRandRange);
        float rangeSegment = maxRandRange / numOfVariations;
        for (int i = 0; i < numOfVariations; i++)
        {
            float lowVal = rangeSegment * i;
            float highVal = rangeSegment * (i + 1);
            if ((randValue <= highVal) && (randValue > lowVal))
            {
                int matNumber = GetComponentInChildren<Renderer>().materials.Length;
                for (int l = 0;l < matNumber; l++)
                {
                    objmat = GetComponentInChildren<Renderer>().materials[l];
                    objmat.color = colourList[i];
                }
            }
        }
    }

    void RandMesh()
    {
        int numOfVariations = modelList.Count;
        float randValue = Random.Range(minRandRange, maxRandRange);
        float rangeSegment = maxRandRange / numOfVariations;
        for (int i = 0; i < numOfVariations; i++)
        {
            float lowVal = rangeSegment * i;
            float highVal = rangeSegment * (i + 1);
            if ((randValue <= highVal) && (randValue > lowVal))
            {
                modelList[i].SetActive(true);
            } else
            {
                modelList[i].SetActive(false);
            }
        }
    }
    
}
