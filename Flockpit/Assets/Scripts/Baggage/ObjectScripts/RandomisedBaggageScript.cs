using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to randomise visual properties of baggage objects as they are spawned
public class RandomisedBaggageScript : MonoBehaviour
{
    //Materials & Colours
    Material objmat;
    public List<Color> colourList;
    //Models
    //Models are dictated by children of a central game object within the inspector
    public List<GameObject> modelList;
    public Component[] renderList;
    //Textures
    [Header("Assign textures grouped via model number")]
    public List<Texture> bagTextures;
    //Random Generation
    //Maximum range for which a float is generated
    //Minimum hardcoded at 0 due to the reliance on procedurally calculated 'segments' of values
    //These segments are used to determine which model is used
    public float maxRandRange = 100.0f;

    // Start is called before the first frame update
    void Start()
    {

        RandMesh();
        RandCol();
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
                for (int l = 0;l < matNumber; l++)
                {
                    objmat = GetComponentInChildren<Renderer>().materials[l];
                    objmat.color = colourList[i];
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
                modelList[i].SetActive(true);
            } else
            {
                modelList[i].SetActive(false);
            }
        }
    }

    void RandTexture()
    {
        int modelNumber = -1;
        for (int i = 0; i < modelList.Count; i++)
        {
            if (modelList[i].activeSelf == true)
            {
                modelNumber = i;
                break;
            }
        }
        switch(modelNumber)
        {
            //Model 01
            case 0:
                {
                    if (UnityEngine.Random.Range(0, 101) < 50)
                    {
                        //Use first texture variant for bag
                        int matNumber = GetComponentInChildren<Renderer>().materials.Length;
                        for (int l = 0; l < matNumber; l++)
                        {
                            objmat = GetComponentInChildren<Renderer>().materials[0];
                            objmat.SetTexture("_MainTex", bagTextures[0]);
                        }
                    }

                    else{
                        //Use second texture variant for bag
                        int matNumber = GetComponentInChildren<Renderer>().materials.Length;
                        for (int l = 0; l < matNumber; l++)
                        {
                            objmat = GetComponentInChildren<Renderer>().materials[0];
                            objmat.SetTexture("_MainTex", bagTextures[1]);
                        }
                    }
                    break;
                }
            //Model 02
            case 1:
                {
                    if (UnityEngine.Random.Range(0, 101) < 50)
                    {
                        //Use first texture variant for bag
                        int matNumber = GetComponentInChildren<Renderer>().materials.Length;
                        for (int l = 0; l < matNumber; l++)
                        {
                            objmat = GetComponentInChildren<Renderer>().materials[0];
                            objmat.SetTexture("_MainTex", bagTextures[2]);
                        }
                    }

                    else
                    {
                        //Use second texture variant for bag
                        int matNumber = GetComponentInChildren<Renderer>().materials.Length;
                        for (int l = 0; l < matNumber; l++)
                        {
                            objmat = GetComponentInChildren<Renderer>().materials[0];
                            objmat.SetTexture("_MainTex", bagTextures[3]);
                        }
                    }
                    break;
                }
            //Model 03
            case 1:
                {
                    if (UnityEngine.Random.Range(0, 101) < 50)
                    {
                        //Use first texture variant for bag
                        int matNumber = GetComponentInChildren<Renderer>().materials.Length;
                        for (int l = 0; l < matNumber; l++)
                        {
                            objmat = GetComponentInChildren<Renderer>().materials[0];
                            objmat.SetTexture("_MainTex", bagTextures[4]);
                        }
                    }

                    else
                    {
                        //Use second texture variant for bag
                        int matNumber = GetComponentInChildren<Renderer>().materials.Length;
                        for (int l = 0; l < matNumber; l++)
                        {
                            objmat = GetComponentInChildren<Renderer>().materials[0];
                            objmat.SetTexture("_MainTex", bagTextures[5]);
                        }
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
    
}
