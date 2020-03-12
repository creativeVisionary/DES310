using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class GateColourScript : MonoBehaviour
{
    public Color gateColour;
    public string colourTag;

    // Start is called before the first frame update
    void Start()
    {
        SetColour();
    }

    // Update is called once per frame
    void Update()
    {
        SetColour();
    }

    void SetColour()
    {
        Material objmat;
        int matNumber = GetComponentInChildren<Renderer>().sharedMaterials.Length;
        for (int l = 0; l < matNumber; l++)
        {
            objmat = GetComponentInChildren<Renderer>().sharedMaterials[l];
            objmat.color = gateColour;
        }
        this.tag = colourTag;
    }
}
