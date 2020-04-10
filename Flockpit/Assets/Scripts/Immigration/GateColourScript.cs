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
        this.tag = colourTag;
    }
}
