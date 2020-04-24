using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GateStatus { CLOSED = -1, OPEN = 1 }

//Script to handle animation of turnstile gates
public class AnimationScript : MonoBehaviour
{
    //Member Variables
    //
    //Inspector Set properties
    //List of animator components to handle the animation of gates
    public List<Animator> gateAnimators;
    //Boolean Members
    //Boolean variables to determine if gate is to be opened or closed
    public bool openGate = false;
    public bool closeGate = true;
    //Variable to check the current status of the gate
    private int gateStatus = (int)GateStatus.CLOSED;
    //Functions

    // Update is called once per frame
    void Update()
    {
        //If the gate is not currently open and it is to be opened, play the open animation
        if (gateStatus != (int)GateStatus.OPEN)
        {
            for (int i = 0; i < gateAnimators.Count; i++)
            {
                if (openGate == true)
                {
                    gateAnimators[i].Play("GateOpen");
                    gateStatus = (int)GateStatus.OPEN;
                }
            }
        }
        //If the gate is currently open and it is to be closed, play the close animation
        if (gateStatus != (int) GateStatus.CLOSED)
            {
            for (int i = 0; i < gateAnimators.Count; i++)
            {
                if (closeGate == true)
                {
                    gateAnimators[i].Play("GateClose");
                    gateStatus = (int)GateStatus.CLOSED;
                }
            }
        }

    }
    //Setters

    //Setters to set gates either open or closed
    public void SetGateOpen()
    {
        openGate = true;
        closeGate = false;
    }
    public void SetGateClosed()
    {
        openGate = false;
        closeGate = true;
    }

}
